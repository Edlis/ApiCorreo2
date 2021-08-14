using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mensajes;
using Microsoft.AspNetCore.Mvc;
using Modelos.Conn;
using Procesos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCorreo2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreoController : ControllerBase
    {
        private SMTPConfiguracionModelo _smtp;

        public CorreoController(SMTPConfiguracionModelo smtp)
        {
            this._smtp = smtp;
        }

        // POST api/<Correo>
        [HttpPost("enviarCorreo")]
        public ActionResult Post(Correo c)
        {
            try
            {
                clsCorreo send = new clsCorreo(_smtp);
                send.Enviar(c);
                return Ok("ok");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
