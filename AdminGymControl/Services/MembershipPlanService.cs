using AdminGymControl.Data;
using AdminGymControl.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminGymControl.Services
{
    public class MembershipPlanService : IMembershipPlanService
    {
        private readonly ApplicationDbContext _context;

        public MembershipPlanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MembershipPlan>> GetAllAsync() =>
            await _context.MembershipPlans.ToListAsync();

        public async Task<MembershipPlan> GetByIdAsync(int id) =>
            await _context.MembershipPlans.FindAsync(id);

        public async Task AddAsync(MembershipPlan plan)
        {
            try
            {
               
                if (plan == null) throw new ArgumentNullException(nameof(plan));

                
                Console.WriteLine($"Guardando plan - Nombre: {plan.Name}, Precio: {plan.Price}");


                await _context.MembershipPlans.AddAsync(plan);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Plan guardado con ID: {plan.Id}");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Error de BD: {dbEx.InnerException?.Message}");
                throw new ApplicationException("Error al guardar en la base de datos. Verifique los datos e intente nuevamente.", dbEx);
            }
        }

        public async Task UpdateAsync(MembershipPlan plan)
        {
            _context.MembershipPlans.Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.MembershipPlans.FindAsync(id);
            if (plan != null)
            {
                _context.MembershipPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }

    }
}
