using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter {
    public class ArticleService : IArticleService {
        private readonly AppDbContext _context;
        private readonly Guid _userId;

        public ArticleService(AppDbContext context, IHttpContextAccessor httpContext) {
            this._context = context;
            Guid.TryParse(httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out this._userId);
        }

        public async Task<Response<ArticleData>> CreateAsync(ArticleData newEntry) {
            var response = new Response<ArticleData>();
            try {
                if (newEntry.NewPicture == null) {
                    response.AddError("Ein Bild muss mit angegeben werden.");
                    return response;
                }

                var article = new Article {
                    Id = Guid.NewGuid(),
                    Title = newEntry.Title,
                    Summary = newEntry.Summary,
                    Description = newEntry.Description,
                    Picture = newEntry.NewPicture,
                    NewsletterId = newEntry.NewsletterId,
                    OrganizationId = newEntry.OrganizationId,
                    CreatedAt = DateTime.Now,
                    CreatedById = this._userId,
                    UpdatedAt = DateTime.Now,
                    UpdatedById = this._userId,
                    Published = false,
                    PublishedAt = null,
                    PublishedById = null
                };

                article = this._context.Articles.Add(article).Entity;
                await _context.SaveChangesAsync();
                response = await this.GetByIdAsync(article.Id);
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(Guid id) {
            var response = new Response<bool>();
            try {
                var entry = await this._context.Articles.FirstOrDefaultAsync(a => a.Id == id);

                if (entry == null) {
                    response.AddError("Der Beitrag wurde nicht gefunden.");
                    return response;
                }

                if (entry.Published) {
                    response.AddError("Der Beitrag ist bereits veröffentlicht.");
                    return response;
                }

                this._context.Articles.Remove(entry);
                await _context.SaveChangesAsync();
                response.Result = true;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<ArticleData>> GetByIdAsync(Guid id) {
            var response = new Response<ArticleData>();
            try {
                var article = await this._context.Articles
                    .Include(x => x.PublishedBy)
                    .Include(x => x.UpdatedBy)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (article == null) {
                    response.AddError("Der Beitrag wurde nicht gefunden.");
                    return response;
                }

                response.Result = ArticleData.Map(article);
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<Article>> GetArticleWithSubscribersAsync(Guid id) {
            var response = new Response<Article>();
            try {
                var article = await this._context.Articles
                    .Include(x => x.PublishedBy)
                    .Include(x => x.UpdatedBy)
                    .Include(x => x.Newsletter.Contacts)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (article == null) {
                    response.AddError("Der Beitrag wurde nicht gefunden.");
                    return response;
                }

                response.Result = article;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<ArticleData>> PublishAsync(Guid id) {
            var response = new Response<ArticleData>();
            try {
                var original = await this._context.Articles.FindAsync(id);
                if (original == null) {
                    response.AddError("Der Beitrag wurde nicht gefunden.");
                    return response;
                }

                original.Published = true;
                original.PublishedAt = DateTime.Now;
                original.PublishedById = this._userId;
                await this._context.SaveChangesAsync();
                response = await this.GetByIdAsync(original.Id);
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<ArticleData>>> SearchAsync(ArticleSearchRequest searchRequest) {
            var response = new Response<IEnumerable<ArticleData>>();
            try {
                var query = this._context.Articles.AsQueryable();
                if (!string.IsNullOrWhiteSpace(searchRequest.SearchTerm)) {
                    query = query.Where(x => 
                    x.Title.Contains(searchRequest.SearchTerm)
                    || x.Description.Contains(searchRequest.SearchTerm));
                }

                if (searchRequest.OrganizationId.HasValue) {
                    query = query.Include(x => x.Newsletter.Organizations).Where(x => x.Newsletter.Organizations.Any(o => o.Id == searchRequest.OrganizationId));
                }

                if (searchRequest.NewsletterId.HasValue) {
                    query = query.Where(x => x.NewsletterId == searchRequest.NewsletterId);
                }

                if (searchRequest.From.HasValue && searchRequest.To.HasValue) {
                    query = query.Where(x => x.CreatedAt >= searchRequest.From.Value && x.CreatedAt <= searchRequest.To.Value);
                }

                if (searchRequest.CreatedById.HasValue) {
                    query = query.Where(x => x.CreatedById == searchRequest.CreatedById.Value);
                }

                if (searchRequest.Published.HasValue) {
                    query = query.Where(x => x.Published == searchRequest.Published.Value);
                }

                response.Result = await query.OrderByDescending(x => x.CreatedAt).Select(a => ArticleData.Map(a)).ToListAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<ArticleData>> UpdateAsync(ArticleData updatedEntry) {
            var response = new Response<ArticleData>();
            try {
                var article = await this._context.Articles.FindAsync(updatedEntry.Id);
                if (article == null) {
                    response.AddError("Der Beitrag konnte nicht gefunden werden.");
                    return response;
                }

                article.Title = updatedEntry.Title;
                article.Summary = updatedEntry.Summary;
                article.Description = updatedEntry.Description;
                article.UpdatedAt = DateTime.Now;
                article.UpdatedById = this._userId;

                if (updatedEntry.NewPicture != null) {
                    article.Picture = updatedEntry.NewPicture;
                }

                this._context.Articles.Update(article);
                await _context.SaveChangesAsync();
                response = await this.GetByIdAsync(article.Id);
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }
    }
}
