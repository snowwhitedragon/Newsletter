using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter {
    public class ContactService : IContactService {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<Contact>> CreateAsync(Contact newEntry) {
            var response = new Response<Contact>();
            try {
                this._context.Contacts.Add(newEntry);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(Guid id) {
            var response = new Response<bool>();
            try {
                var entry = await this._context.Contacts.FirstOrDefaultAsync(a => a.Id == id);

                if (entry == null) {
                    response.AddError("Kontakt wurde nicht gefunden.");
                    return response;
                }

                this._context.Contacts.Remove(entry);
                await _context.SaveChangesAsync();
                response.Result = true;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<Contact>> GetByIdAsync(Guid id) {
            var response = new Response<Contact>();
            try {
                var entry = await this._context.Contacts
                    .Include(c => c.Customers)
                    .Include(c => c.Subcontractors)
                    .Include(c => c.Suppliers)
                    .Include(c => c.Language)
                    .Include(c => c.State).FirstOrDefaultAsync(x => x.Id == id);

                if (entry == null) {
                    response.AddError("Kontakt wurde nicht gefunden.");
                    return response;
                }

                response.Result = entry;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<Contact>>> SearchAsync(SearchRequestBase searchRequest) {
            var response = new Response<IEnumerable<Contact>>();
            try {
                var query = this._context.Contacts
                    .Include(c => c.Customers)
                    .Include(c => c.Subcontractors)
                    .Include(c => c.Suppliers)
                    .Include(c => c.Language)
                    .Include(c => c.State)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchRequest.SearchTerm)) {
                    query = query.Where(c =>
                    c.FirstName.Contains(searchRequest.SearchTerm)
                    || c.LastName.Contains(searchRequest.SearchTerm)
                    || c.Email.Contains(searchRequest.SearchTerm)
                    || c.Language.Language.Contains(searchRequest.SearchTerm)
                    || c.State.Title.Contains(searchRequest.SearchTerm));
                }

                response.Result = await query.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<Contact>> UpdateAsync(Contact updatedEntry) {
            var response = new Response<Contact>();
            try {
                var entry = await this._context.Contacts.FirstOrDefaultAsync(x => x.Id == updatedEntry.Id);

                if (entry == null) {
                    response.AddError("Kontakt wurde nicht gefunden.");
                    return response;
                }

                entry.Salutation = updatedEntry.Salutation;
                entry.FirstName = updatedEntry.FirstName;
                entry.LastName = updatedEntry.LastName;
                entry.Email = updatedEntry.Email;
                entry.LanguageId = updatedEntry.LanguageId;
                entry.StateId = updatedEntry.StateId;
                response.Result = entry;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }
    }
}
