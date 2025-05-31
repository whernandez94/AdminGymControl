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

        public async Task<List<Member>> GetAllAsync() => await _context.Members.ToListAsync();

        public async Task<Member> GetByIdAsync(int id) => await _context.Members.FindAsync(id);

        public async Task AddAsync(Member member)
        {
            try
            {
                member.JoinDate = DateTime.Now; // Set current date if not set
                _context.Members.Add(member);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log error
                throw new ApplicationException("Error saving member", ex);
            }
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                try
                {
                    _context.Members.Remove(member);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log del error si es necesario
                    throw new Exception("Error deleting member: " + ex.Message);
                }
            }
        }
    }

}
