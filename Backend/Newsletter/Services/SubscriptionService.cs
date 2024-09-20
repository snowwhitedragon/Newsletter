using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter {
    public class SubscriptionService : ISubscriptionService {
        private readonly AppDbContext _context;

        public SubscriptionService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<bool>> SubscribeAsync(Guid contactId, Guid newsId) {
            var response = new Response<bool>();
            try {
                var contact = await this._context.Contacts.FirstOrDefaultAsync(a => a.Id == contactId);
                if (contact == null) {
                    response.AddError("Kontakt wurde nicht gefunden.");
                    return response;
                }

                var news = await this._context.Newsletters.FirstOrDefaultAsync(n => n.Id == newsId);
                if (news == null) {
                    response.AddError("Newsletter wurde nicht gefunden.");
                    return response;
                }

                contact.Newsletters.Add(news);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> UnsubscribeAsync(Guid contactId, Guid newsId) {
            var response = new Response<bool>();
            try {
                var entry = await this._context.Contacts.FirstOrDefaultAsync(a => a.Id == contactId);
                if (entry == null) {
                    response.AddError("Kontakt wurde nicht gefunden.");
                    return response;
                }

                var news = entry.Newsletters.FirstOrDefault(n => n.Id == newsId);
                if (news == null) {
                    response.AddError("Subscription wurde nicht gefunden.");
                    return response;
                }

                entry.Newsletters.Remove(news);
                await _context.SaveChangesAsync();
                response.Result = true;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }
    }
}
