using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using RelayController.Domain.Common;

namespace RelayController.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly EmailSettings _settings;
    
    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
        _smtpClient = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = _settings.EnableSsl
        };
    }


    private string DefaultBodyMessage(string message, string endpoint ,string btnLabel,string token)
    {
        return 
            $"""
                 <html>
                   <body>
                    <center style='margin-top: 20vh;'>
                      <p>{message}:</p>
                      <br/>
                      <a 
                          href='{_settings.Url}/{endpoint}?token={token}'  
                          target='_blank'
                          rel='noopener noreferrer'
                          style='
                          color: #ffffff;
                          background-color: #2d63c8;
                          border: 1px solid #2d63c8;
                          border-radius: 12px;
                          font-size: 20px;
                          text-decoration: none;
                          padding: 15px 50px;
                          cursor: pointer;'
                      >{btnLabel}</a>
                    </center>
                   </body>
                 </html>
             """;
    }

    public async Task SendEmailConfirmationAsync(string email, string emailToken, CancellationToken cancellationToken)
    {
        var body =  DefaultBodyMessage("Por favor confirme o Email clicando no botão abaixo", "confirm-email" ,"Confirmar Email",emailToken);
        var message = new MailMessage(_settings.From, email, "Confirmação de Email", body);
        message.IsBodyHtml = true;
        await _smtpClient.SendMailAsync(message, cancellationToken);
    }

    public async Task SendPasswordRecoveryEmailAsync(string email, string recoveryToken, CancellationToken cancellationToken)
    {
        var body =  DefaultBodyMessage("Para recuperar a senha, clique no botão abaixo", "reset-password" ,"Recuperar Senha",recoveryToken);
        var message = new MailMessage(_settings.From, email, "Password Recovery", body);
        message.IsBodyHtml = true;
        await _smtpClient.SendMailAsync(message, cancellationToken);
    }
}