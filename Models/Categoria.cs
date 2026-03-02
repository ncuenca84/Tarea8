using Microsoft.CodeAnalysis;

namespace usuarios.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<Proyecto> Proyectos { get; set; }
    }
}
