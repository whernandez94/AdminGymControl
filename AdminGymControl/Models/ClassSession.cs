using System.ComponentModel.DataAnnotations;

namespace AdminGymControl.Models
{
    public class ClassSession
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public ICollection<MemberClass> MemberClasses { get; set; }
    }

}
