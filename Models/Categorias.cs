using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpFinalJoaquinGaido.Models
{
    public class Categorias
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
