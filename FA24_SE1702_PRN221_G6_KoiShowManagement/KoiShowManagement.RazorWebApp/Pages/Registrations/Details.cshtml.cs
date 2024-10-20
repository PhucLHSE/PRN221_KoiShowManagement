using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Service;

namespace KoiShowManagement.RazorWebApp.Pages.Registrations
{
    public class DetailsModel : PageModel
    {
        private readonly RegistrationService _registrationService;
        public DetailsModel(RegistrationService registrationService)
        {
            //_context = context;
            _registrationService = registrationService;
        }

        public Registration Registration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var registration = await _registrationService.GetById((int)id);
            if (registration == null)
            {
                return NotFound();
            }
            else
            {
                Registration = registration.Data as Registration;
            }
            return Page();
        }
    }
}
