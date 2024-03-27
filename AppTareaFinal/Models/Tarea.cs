using System.ComponentModel.DataAnnotations;

namespace AppTareaFinal.Models
{
    public class Tarea
    {
        [Key]
        public int IdTarea { get; set; }
        public string NombreTarea { get; set; }
    }
}
