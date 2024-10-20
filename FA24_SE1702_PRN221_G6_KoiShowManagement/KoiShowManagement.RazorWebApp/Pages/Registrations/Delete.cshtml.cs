using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Common;
using KoiShowManagement.Service;

namespace KoiShowManagement.RazorWebApp.Pages.Registrations
{
    public class DeleteModel : PageModel
    {
        private readonly RegistrationService _registrationService;

        public DeleteModel(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var registration = await _context.Registrations.FirstOrDefaultAsync(m => m.RegistrationId == id);
            var registration = await _registrationService.GetById((int)id);

            if (registration == null)
            {
                return NotFound();
            }

            Registration = registration.Data as Registration; // Lấy dữ liệu từ ServiceResult
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var registration = await _registrationService.GetById((int)id);


            if (registration.Status != Const.SUCCESS_READ_CODE || registration.Data == null)
            {
                return NotFound();
            }

            await _registrationService.DeleteById((int)id);

            return RedirectToPage("./Index");
        }
    }
}
