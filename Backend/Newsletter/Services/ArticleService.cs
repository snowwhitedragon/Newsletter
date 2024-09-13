﻿using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter {
    public class ArticleService : IArticleService {
        private readonly AppDbContext _context;

        public ArticleService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<Article>> CreateAsync(Article newEntry) {
            var response = new Response<Article>();
            try {
                newEntry.CreatedAt = DateTime.Now;
                newEntry.CreatedById = 0; // TODO Userscope
                this._context.Articles.Add(newEntry);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(int id) {
            var response = new Response<bool>();
            try {
                var entry = await this._context.Articles.FirstOrDefaultAsync(a => a.Id == id);

                if (entry == null) {
                    response.AddError("Artikel wurde nicht gefunden.");
                    return response;
                }

                if (entry.Published) {
                    response.AddError("Der Artikel ist bereits veröffentlicht.");
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

        public async Task<Response<Article>> GetByIdAsync(int id) {
            var response = new Response<Article>();
            try {
                var article = await this._context.Articles.FirstOrDefaultAsync(x => x.Id == id);

                if (article == null) {
                    response.AddError("Artikel wurde nicht gefunden.");
                    return response;
                }

                response.Result = article;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<Article>> PublishAsync(int id) {
            var response = new Response<Article>();
            try {
                var original = await this._context.Articles.FirstOrDefaultAsync(y => y.Id == id);
                if (original == null) {
                    response.AddError("Artikel wurde nicht gefunden.");
                    return response;
                }

                original.Published = true;
                original.PublishedAt = DateTime.Now;
                original.PublishedById = 0; // TODO Userscope
                await this._context.SaveChangesAsync();
                response.Result = original;
                // TODO SEND MAILS
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<Article>>> SearchAsync(ArticleSearchRequest searchRequest) {
            var response = new Response<IEnumerable<Article>>();
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

                response.Result = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public Task<Response<Article>> UpdateAsync(Article updatedEntry) {
            throw new NotImplementedException();
        }
    }
}