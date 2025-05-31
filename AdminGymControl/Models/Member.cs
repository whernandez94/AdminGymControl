using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminGymControl.Models
{

    public class Member
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre completo es requerido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime JoinDate { get; set; } = DateTime.Today;

        [Display(Name = "Plan de Membresía")]
        public int? MembershipPlanId { get; set; } 

        [ForeignKey("MembershipPlanId")]
        public virtual MembershipPlan? MembershipPlan { get; set; } 
    }

}
