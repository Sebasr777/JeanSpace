using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_de_Herramientas
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public override string ToString()
        {
            return NombreRol;
        }
    }

}
