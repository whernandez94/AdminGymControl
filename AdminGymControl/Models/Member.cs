using System.ComponentModel.DataAnnotations;

namespace AdminGymControl.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
    }

}
