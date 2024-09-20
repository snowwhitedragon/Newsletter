using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Newsletter.Data;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter {
    public class MailService : IMailService {
        private readonly IArticleService _articleService;
        private readonly SmtpSettings _smptSettings;

        public MailService(IArticleService articleService, IOptions<SmtpSettings> smptSettings) {
            this._articleService = articleService;
            this._smptSettings = smptSettings.Value;
        }

        public async Task<Response<bool>> SendAsync(IEnumerable<string> recipients, string subject, string body) {
            var response = new Response<bool>();
            try {
                if (!recipients.Any()) {
                    response.AddError("Es wurden keine Empfänger angegeben");
                    return response;
                }

                if (!string.IsNullOrWhiteSpace(body) || !string.IsNullOrWhiteSpace(subject)) {
                    response.AddError("Es wurden keine Empfänger angegeben");
                    return response;
                }

                var mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("Nentindo Games", this._smptSettings.UserName));
                mail.Bcc.AddRange(recipients.Select(r => MailboxAddress.Parse(r)));

                var builder = new BodyBuilder() {
                    HtmlBody = body
                };

                mail.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient()) {
                    await smtp.ConnectAsync(this._smptSettings.Host, this._smptSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(this._smptSettings.UserName, this._smptSettings.Password);
                    await smtp.SendAsync(mail);
                    await smtp.DisconnectAsync(true);
                }
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> SendNewsletterToSubscibersAsync(Guid articleId) {
            var response = new Response<bool>();

            try {
                Response<Article> articleResponse = await this._articleService.GetByIdAsync(articleId);
                if (!articleResponse.IsSuccess || articleResponse.Result == null) {
                    response.AddError("Artikel wurde nicht gefunden und konnte somit nicht an alle versendet werden");
                    return response;
                }

                IEnumerable<string> recipients = articleResponse.Result.Newsletter.Contacts.Select(x => x.Email);
                string subject = $"{articleResponse.Result.Newsletter.Title} - Neuer Beitrag";
                string body = $"";
                await this.SendAsync(recipients, subject, body);
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }
    }
}
