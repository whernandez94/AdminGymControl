using System.ComponentModel.DataAnnotations;

namespace AdminGymControl.Models
{
    public class MembershipPlan
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }

        public ICollection<Member> Members { get; set; }
    }

}
