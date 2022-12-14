using Spire.Email;
using Spire.Email.Smtp;
using Spire.Email.IMap;
using HtmlAgilityPack;
using TallerTECService.Models;

namespace TallerTECService.Coms
{

    //Esta clase hace uso de las librerias HtmlAgilityPack (Para modificar el archivo HTML que se envia por correo a los clientes, personalizando con sus datos)
    //y la libreria Spire.Email para hacer el envio del correo usando protocolo smtp
    public static class EmailSender
    {
        public static void SendCreationEmail(Cliente customer)
        {
            
            MailAddress sender = "tallertec.noreply@gmail.com";
            MailAddress recipient = customer.correo;
            MailMessage message = new MailMessage(sender,recipient);
            var password = customer.contrasena;

            HtmlDocument emailDoc = new HtmlDocument();
            emailDoc.Load(@"Coms/email.html");
            var passwordSpace = emailDoc.GetElementbyId("password");
            passwordSpace.InnerHtml = customer.contrasena;

            message.Subject = customer.nombre + ", Bienvenido a TallerTEC!";
            message.BodyHtml = emailDoc.DocumentNode.OuterHtml;
            message.Date = DateTime.Now;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.ConnectionProtocols = ConnectionProtocols.Ssl;
            smtp.Username = sender.Address;
            smtp.Password = "wtqdcbfuzwwbhuxd";
            smtp.Port = 587;
            smtp.SendOne(message);


            
        }
    }
}