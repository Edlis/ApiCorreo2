using Mensajes;
using Modelos.Conn;
using System;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace Procesos
{
    public class clsCorreo
    {
        public SMTPConfiguracionModelo _smtp;

        public clsCorreo(SMTPConfiguracionModelo smtp)
        {
            this._smtp = smtp;
        }


        public string Enviar(Correo correo)
        {

            try
            {                

                File.WriteAllText("Archivo.txt", correo.Mensaje);
                
                MailMessage message = new MailMessage(_smtp.Email, correo.Destinatario);
                message.Subject = correo.Titulo;
                message.Body = correo.Mensaje;
                SmtpClient client = new SmtpClient(_smtp.Host, _smtp.Port);
                //se le quito seguridad al correo
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(_smtp.Email, _smtp.Password);

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocurrio un error al enviar el correo: {0}",
                        ex.ToString());
                    throw ex;
                }
                return "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                throw ex;
            }

        }
    }
}
