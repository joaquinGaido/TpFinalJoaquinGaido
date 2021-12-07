using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TpFinalJoaquinGaido.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Precio { get; set; }

        public string Descripcion { get; set; }

        public string Imagen { get; set; }

        public bool Favorito { get; set; }

        [ForeignKey("Marca")]
        public int Marcaid { get; set; }
        public Marcas Marca { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public Categorias Categoria { get; set; }


        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        public Proveedores Proveedor { get; set; }

    }
}
