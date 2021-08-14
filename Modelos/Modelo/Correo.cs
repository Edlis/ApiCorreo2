using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Mensajes
{
    public class Correo
    {
        [DataMember(IsRequired = true)]
        public string Titulo { get; set; }
        [DataMember(IsRequired = true)]
        public string Mensaje { get; set; }
        [DataMember(IsRequired = true)]
        public string Destinatario { get; set; }
    }
}
