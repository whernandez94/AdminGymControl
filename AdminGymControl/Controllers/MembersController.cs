using AdminGymControl.Models;
using AdminGymControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminGymControl.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _service;

        public MembersController(IMemberService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

        public IActionResult Create()
        {
            var model = new Member
            {
                JoinDate = DateTime.Today // Valor por defecto
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var member = await _service.GetByIdAsync(id);
            return member == null ? NotFound() : View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Json(new { success = true, message = "Miembro eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}
