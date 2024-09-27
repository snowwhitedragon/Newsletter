using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter.Services {
    public class OrganizationService : IOrganizationService {
        private readonly AppDbContext _context;
        public OrganizationService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<Organization>> GetByIdAsync(Guid id) {
            var response = new Response<Organization>();
            try {
                var organization = await this._context.Organizations.FirstOrDefaultAsync(r => r.Id == id);
                if (organization == null) {
                    response.AddError("Organisation wurde nicht gefunden");
                    return response;
                }

                response.Result = organization;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<Organization>>> SearchAsync(SearchRequestBase searchRequest) {
            var response = new Response<IEnumerable<Organization>>();
            try {
                IQueryable<Organization> query = this._context.Organizations;

                if (!string.IsNullOrWhiteSpace(searchRequest.SearchTerm)) {
                    query = query.Where(r => r.Title.Contains(searchRequest.SearchTerm));
                }

                query = this.OrderBy(query, searchRequest);

                response.Result = await query.Skip(searchRequest.Skip).Take(searchRequest.Take).ToListAsync();
                if (!response.Result.Any()) {
                    response.AddError("Die Suche ergab keine Ergebnisse");
                }
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        private IQueryable<Organization> OrderBy(IQueryable<Organization> query, SearchRequestBase searchRequest) {
            switch (searchRequest.OrderBy) {
                case "Title":
                    return searchRequest.Descending ? query.OrderByDescending(r => r.Title) : query.OrderBy(r => r.Title);
                default:
                    return query.OrderBy(r => r.Title);
            }
        }
    }
}
