using MassTransit;
using Mensajes;
using Modelos.Conn;
using Procesos.Errores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Procesos
{
    public class Consumidor : IConsumer<Correo>
    {
        public readonly SMTPConfiguracionModelo _smtp;
        public Consumidor(SMTPConfiguracionModelo smtp)
        {
            _smtp = smtp;
        }

        public Task Consume(ConsumeContext<Correo> context)
        {
            var correo = context.Message;
            clsCorreo email = new clsCorreo(_smtp);

            Console.WriteLine("entro al consumidor");

            if (context.GetRetryCount() < 2) {
                Console.WriteLine("Reintento " + context.GetRetryCount().ToString());
                throw new ErrorControlado();

            }
                  

            Thread.Sleep(10000);
            email.Enviar(correo);

            return Task.CompletedTask;
        }
    }
}
