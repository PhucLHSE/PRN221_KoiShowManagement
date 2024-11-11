using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Common;
using KoiShowManagement.Service;

namespace KoiShowManagement.RazorWebApp.Pages.Registrations
{
    public class EditModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly RegistrationService _registrationService;

        public EditModel(RegistrationService registrationService) => _registrationService = registrationService;


        [BindProperty]
        public Registration Registration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var registrationResult = _registrationService.GetById(id);
            if (registrationResult.Result.Status == Const.SUCCESS_READ_CODE)
            {
                Registration = registrationResult.Result.Data as Registration;
            }

            var animals = _registrationService.GetAnimalsList().Result.Data as List<Animal>;
            ViewData["AnimalId"] = new SelectList(animals, "AnimalId", "AnimalName");

            var competions = _registrationService.GetCompetitionsList().Result.Data as List<Competition>;
            ViewData["CompetitionId"] = new SelectList(competions, "CompetitionId", "CompetitionName");

            var users = _registrationService.GetUsersList().Result.Data as List<User>;
            ViewData["UserId"] = new SelectList(users, "UserId", "Email");


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Registration.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Registration.ImageFile.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                {
                    await Registration.ImageFile.CopyToAsync(fileStream);
                }

                Registration.Image = "/images/" + fileName;
            }
          


            await _registrationService.Save(Registration);
            return RedirectToPage("./Index");


        }

        private bool RegistrationExists(int id)
        {
            return _registrationService.GetById(id) != null;

        }
    }
}
