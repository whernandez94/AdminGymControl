using AdminGymControl.Models;
using AdminGymControl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminGymControl.Controllers
{
    public class MembershipPlansController : Controller
    {
        private readonly IMembershipPlanService _planService;
        private readonly IMemberService _memberService;

        public MembershipPlansController(
            IMembershipPlanService planService,
            IMemberService memberService)
        {
            _planService = planService;
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _planService.GetAllAsync();
            return View(plans);
        }

        public IActionResult Create()
        {
            return View(new MembershipPlan());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembershipPlan plan)
        {
            if (ModelState.IsValid)
            {
                await _planService.AddAsync(plan);
                TempData["SuccessMessage"] = "Plan creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MembershipPlan plan)
        {
            if (id != plan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _planService.UpdateAsync(plan);
                    TempData["SuccessMessage"] = "Plan actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error al actualizar el plan");
                }
            }
            return View(plan);
        }

        public async Task<IActionResult> Details(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            var members = await _memberService.GetMembersByPlanIdAsync(id);
            ViewBag.Members = members;

            return View(plan);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

   
            var hasMembers = await _memberService.HasMembersWithPlanAsync(id);
            ViewBag.CanDelete = !hasMembers;

            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
              
                var hasMembers = await _memberService.HasMembersWithPlanAsync(id);
                if (hasMembers)
                {
                    return Json(new
                    {
                        success = false,
                        message = "No se puede eliminar el plan porque tiene miembros asociados"
                    });
                }

                await _planService.DeleteAsync(id);
                return Json(new
                {
                    success = true,
                    message = "Plan eliminado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Error al eliminar: {ex.Message}"
                });
            }
        }
    }
}
