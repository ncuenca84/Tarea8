using System;

namespace usuarios.ViewModels
{
    public class ProyectoCreateVM
    {
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
        public decimal Presupuesto { get; set; }
        public int ClienteId { get; set; }
        public int CategoriaId { get; set; }
    }
}
