

using System.Net.Mail;
using System.Net;
using Aplicacion.Interface;

namespace Infraestructura.Correos
{
    
    public class Correos : ICorreos
    {
        private readonly string _remitente;
        private readonly string _password = "weurxannfzeqkuuw";
        private readonly string _host = "smtp.gmail.com";
        private readonly int _port = 587;
        private readonly bool _enableSsl = true;

        public Correos(string remitente)
        {
            _remitente = remitente;
        }

        public void EnviarCorreo(string destinatario, string asunto, string cuerpo, bool esHtml = false)
        {
            using var mensaje = new MailMessage
            {
                From = new MailAddress(_remitente, "Mi Aplicación"),
                Subject = asunto,
                Body = cuerpo,
                IsBodyHtml = esHtml
            };
            mensaje.To.Add(destinatario);

            using var smtp = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_remitente, _password),
                EnableSsl = _enableSsl
            };

            smtp.Send(mensaje);
        }
    }
}
