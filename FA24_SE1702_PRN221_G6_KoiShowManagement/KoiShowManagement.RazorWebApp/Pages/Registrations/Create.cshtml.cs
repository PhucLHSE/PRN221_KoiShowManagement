using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Service;

namespace KoiShowManagement.RazorWebApp.Pages.Registrations
{
    public class CreateModel : PageModel
    {
        //  private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;

        private readonly RegistrationService _registrationService;

        public CreateModel(RegistrationService registrationService)
        {
            // _context = context;
            _registrationService = registrationService;
        }

        public IActionResult OnGet()
        {
            var animals = _registrationService.GetAnimalsList().Result.Data as List<Animal>;
            ViewData["AnimalId"] = new SelectList(animals, "AnimalId", "AnimalName");

            var competions = _registrationService.GetCompetitionsList().Result.Data as List<Competition>;
            ViewData["CompetitionId"] = new SelectList(competions, "CompetitionId", "CompetitionName");

            var users = _registrationService.GetUsersList().Result.Data as List<User>;
            ViewData["UserId"] = new SelectList(users, "UserId", "Email");

            return Page();
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;




        // For more information, see https://aka.ms/RazorPagesCRUD.

        ////////////////////////////////////////////////////////////////////////////////////////
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Registrations.Add(Registration);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Sử dụng RegistrationService để lưu bản ghi
            await _registrationService.Save(Registration);
            return RedirectToPage("./Index");
        }
    }
}
