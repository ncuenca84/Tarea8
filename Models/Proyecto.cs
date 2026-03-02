namespace usuarios.Models
{
    public class Proyecto
    {
        public int ProyectoId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
        public decimal Presupuesto { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<Tarea> Tareas { get; set; }
    }
}
