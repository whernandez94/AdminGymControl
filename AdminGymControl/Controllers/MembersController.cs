using AdminGymControl.Models;
using AdminGymControl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminGymControl.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMembershipPlanService _planService;

        public MembersController(
            IMemberService memberService,
            IMembershipPlanService planService)
        {
            _memberService = memberService;
            _planService = planService;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _memberService.GetAllWithPlansAsync();
            return View(members);
        }

        public async Task<IActionResult> Create()
        {
            await LoadPlansViewData();
            return View(new Member
            {
                JoinDate = DateTime.Today,
                MembershipPlanId = null
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _memberService.AddAsync(member);
                    TempData["SuccessMessage"] = "Miembro creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al crear miembro: {ex.Message}");
                }
            }

            await LoadPlansViewData();
            return View(member);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var member = await _memberService.GetByIdWithPlanAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            await LoadPlansViewData();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _memberService.UpdateAsync(member);
                    TempData["SuccessMessage"] = "Miembro actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al actualizar miembro: {ex.Message}");
                }
            }

            await LoadPlansViewData();
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _memberService.ForceDeleteAsync(id);
                return Json(new
                {
                    success = true,
                    message = "Miembro eliminado exitosamente, incluyendo su asociación a planes"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Error al eliminar miembro: {ex.Message}"
                });
            }
        }

        private async Task LoadPlansViewData()
        {
            var plans = await _planService.GetAllAsync();
            ViewBag.MembershipPlans = new SelectList(plans, "Id", "Name");
        }
    }

}
