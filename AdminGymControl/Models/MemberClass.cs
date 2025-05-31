namespace AdminGymControl.Models
{
    public class MemberClass
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int ClassSessionId { get; set; }
        public ClassSession ClassSession { get; set; }
    }

}
