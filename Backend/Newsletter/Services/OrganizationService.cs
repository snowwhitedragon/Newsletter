using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;
using Newsletter.Data.Enums;

namespace Newsletter.Services {
    public class OrganizationService : IOrganizationService {
        private readonly AppDbContext _context;
        private readonly Guid _userId;

        public OrganizationService(AppDbContext context, IHttpContextAccessor httpContext) {
            this._context = context;
            Guid.TryParse(httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out this._userId);
        }

        public async Task<Response<OrganizationData>> GetByIdAsync(Guid id) {
            var response = new Response<OrganizationData>();
            try {
                var organization = await this._context.Organizations.Include(o => o.Newsletters).FirstOrDefaultAsync(r => r.Id == id);
                if (organization == null) {
                    response.AddError("Organisation wurde nicht gefunden");
                    return response;
                }

                response.Result = new OrganizationData(organization);
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<OrganizationData>>> SearchAsync(OrganizationSearchRequest searchRequest) {
            var response = new Response<IEnumerable<OrganizationData>>();
            try {
                IQueryable<Organization> query = this._context.Organizations.Include(o => o.Newsletters);

                if (searchRequest.OnlyMine == true) {
                    var user = await this._context.Users.Include(o => o.Organization).FirstAsync(o => o.Id == this._userId);
                    if (user.Organization == null) {
                        response.AddError("Benutzer ist keiner Organisation zugeteilt");
                        return response;
                    }

                    if ((ResponsibilityTypes)user.Organization.ResponsibilityType != ResponsibilityTypes.Customers) {
                        query.Where(o => o.Id == user.OrganizationId);
                    }
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.SearchTerm)) {
                    query = query.Where(r => r.Title.Contains(searchRequest.SearchTerm));
                }

                query = this.OrderBy(query, searchRequest);

                response.Result = await query.Skip(searchRequest.Skip).Take(searchRequest.Take).Select(o => new OrganizationData(o)).ToListAsync();
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
