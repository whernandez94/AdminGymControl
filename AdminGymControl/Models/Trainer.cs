using System.ComponentModel.DataAnnotations;

namespace AdminGymControl.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }

        public ICollection<ClassSession> ClassSessions { get; set; }
    }

}
