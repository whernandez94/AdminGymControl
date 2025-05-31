using System.ComponentModel.DataAnnotations;

namespace AdminGymControl.Models
{
    public class MembershipPlan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser al menos 1 día")]
        public int DurationInDays { get; set; }

    }

}
