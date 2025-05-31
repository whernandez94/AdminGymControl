using System.ComponentModel.DataAnnotations;

namespace AdminGymControl.Models
{
  
    public class Member
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre completo es requerido")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo invalido")]
        public string Email { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; } = DateTime.Today;

        public int MembershipPlanId { get; set; }
        public MembershipPlan MembershipPlan { get; set; }

        public ICollection<MemberClass> MemberClasses { get; set; }
    }


}
