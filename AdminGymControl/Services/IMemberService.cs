using AdminGymControl.Models;

namespace AdminGymControl.Services
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(int id);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int id);
    }

}
