using AdminGymControl.Models;

namespace AdminGymControl.Services
{
    public interface IMemberService
    {
        Task<List<Member>> GetByIdsAsync(List<int> ids);
        Task<List<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(int id);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int id);
        Task<List<Member>> GetAllWithPlansAsync();
        Task<Member> GetByIdWithPlanAsync(int id);

        Task<List<Member>> GetMembersByPlanIdAsync(int planId);
        Task<bool> HasMembersWithPlanAsync(int planId);

        Task ForceDeleteAsync(int id);


    }

}
