namespace usuarios.Models
{
    public class Tarea
    {
        public int TareaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Estado { get; set; }

        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
    }
}
