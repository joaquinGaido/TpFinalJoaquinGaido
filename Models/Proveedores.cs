using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpFinalJoaquinGaido.Models
{
    public class Proveedores
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Telefono { get; set; }

        public string Domicilio { get; set; }

        public string Localidad { get; set; }

        public string Provincia { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
