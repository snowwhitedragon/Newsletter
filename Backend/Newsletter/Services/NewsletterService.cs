using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter.Services {
    public class NewsletterService : INewsletterService {
        private readonly AppDbContext _context;
        public NewsletterService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<Entities.Newsletter>> GetByIdAsync(Guid id) {
            var response = new Response<Entities.Newsletter>();
            try {
                var news = await this._context.Newsletters.Include(n => n.Articles).FirstOrDefaultAsync(r => r.Id == id);
                if (news == null) {
                    response.AddError("Newsletter wurde nicht gefunden");
                    return response;
                }

                response.Result = news;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<Entities.Newsletter>>> SearchAsync(SearchRequestBase searchRequest) {
            var response = new Response<IEnumerable<Entities.Newsletter>>();
            try {
                IQueryable<Entities.Newsletter> query = this._context.Newsletters.Include(n => n.Articles);

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

        private IQueryable<Entities.Newsletter> OrderBy(IQueryable<Entities.Newsletter> query, SearchRequestBase searchRequest) {
            switch (searchRequest.OrderBy) {
                case "Title":
                    return searchRequest.Descending ? query.OrderByDescending(r => r.Title) : query.OrderBy(r => r.Title);
                default:
                    return query.OrderBy(r => r.Title);
            }
        }
    }
}
