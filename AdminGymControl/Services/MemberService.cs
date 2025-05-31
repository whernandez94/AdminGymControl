using AdminGymControl.Data;
using AdminGymControl.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminGymControl.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> GetAllAsync() =>
            await _context.Members.ToListAsync();

        public async Task<List<Member>> GetAllWithPlansAsync() =>
            await _context.Members
                .Include(m => m.MembershipPlan)
                .OrderBy(m => m.FullName)
                .ToListAsync();

        public async Task<Member> GetByIdAsync(int id) =>
            await _context.Members.FindAsync(id);

        public async Task<Member> GetByIdWithPlanAsync(int id) =>
            await _context.Members
                .Include(m => m.MembershipPlan)
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(Member member)
        {
            try
            {
                
                if (member == null) throw new ArgumentNullException(nameof(member));

               
                if (member.MembershipPlanId.HasValue)
                {
                    var planExists = await _context.MembershipPlans
                        .AnyAsync(p => p.Id == member.MembershipPlanId.Value);

                    if (!planExists)
                    {
                        throw new ArgumentException("El plan especificado no existe");
                    }
                }

                
                member.JoinDate = member.JoinDate == default ? DateTime.Today : member.JoinDate;

                _context.Members.Add(member);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al guardar el miembro en la base de datos", ex);
            }
        }

        public async Task UpdateAsync(Member member)
        {
            try
            {
                
                var existingMember = await _context.Members
                    .FirstOrDefaultAsync(m => m.Id == member.Id);

                if (existingMember == null)
                {
                    throw new KeyNotFoundException("Miembro no encontrado");
                }

                
                if (member.MembershipPlanId.HasValue)
                {
                    var planExists = await _context.MembershipPlans
                        .AnyAsync(p => p.Id == member.MembershipPlanId.Value);

                    if (!planExists)
                    {
                        throw new ArgumentException("El plan especificado no existe");
                    }
                }

                
                existingMember.FullName = member.FullName;
                existingMember.Email = member.Email;
                existingMember.JoinDate = member.JoinDate;
                existingMember.MembershipPlanId = member.MembershipPlanId;

                _context.Members.Update(existingMember);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al actualizar el miembro en la base de datos", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                
                if (member.MembershipPlanId.HasValue)
                {
                    throw new InvalidOperationException("El miembro tiene un plan asociado");
                }

                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Miembro no encontrado");
            }
        }

        public async Task<List<Member>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Members
                .Where(m => ids.Contains(m.Id))
                .ToListAsync();
        }

        public async Task<List<Member>> GetMembersByPlanIdAsync(int planId)
        {
            return await _context.Members
                .Where(m => m.MembershipPlanId == planId)
                .ToListAsync();
        }

        public async Task<bool> HasMembersWithPlanAsync(int planId)
        {
            return await _context.Members
                .AnyAsync(m => m.MembershipPlanId == planId);
        }

        public async Task ForceDeleteAsync(int id)
        {
            var member = await _context.Members
                .Include(m => m.MembershipPlan)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                throw new KeyNotFoundException("Miembro no encontrado");
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
    }

}
