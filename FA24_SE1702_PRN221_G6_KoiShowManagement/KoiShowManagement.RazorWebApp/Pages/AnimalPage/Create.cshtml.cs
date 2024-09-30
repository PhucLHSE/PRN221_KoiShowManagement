using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Service;
using KoiShowManagement.Common;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Common.ENUM;

namespace KoiShowManagement.RazorWebApp.Pages.AnimalPage
{
    public class CreateModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly AnimalService _animalService;
        private readonly AnimalVarietyService _animalVarietyService;

        public CreateModel(AnimalService animalService, AnimalVarietyService animalVarietyService)
        {
            _animalService = animalService;
            _animalVarietyService = animalVarietyService;
            //_animalService ??= new AnimalService();
        }

        public IActionResult OnGetAsync()
        {
            //ViewData["VarietyId"] = new SelectList(_context.AnimalVarieties, "VarietyId", "VarietyId");
            //ViewData["VarietyId"] = new SelectList(_animalService.GetAnimalVarieties(), "VarietyId", "VarietyName");

            var animalVarieties = _animalVarietyService.GetAll().Result.Data as List<AnimalVariety>;
            ViewData["VarietyId"] = new SelectList(animalVarieties, "VarietyId", "VarietyName");
            ViewData["GenderList"] = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            return Page();

            //var animalVarieties= await _animalService.GetAnimalVarieties();
            //// Check if the result is successful
            //if (animalVarieties.Status == Const.SUCCESS_READ_CODE && animalVarieties.Data != null)
            //{
            //    // Extract the data and pass it to the SelectList
            //    var varieties = animalVarieties.Data as IEnumerable<AnimalVariety>;
            //    ViewData["VarietyId"] = new SelectList(varieties, "VarietyId", "VarietyName");
            //}
            //else
            //{
            //    // Handle the case where no data is found or an error occurs
            //    ViewData["VarietyId"] = new SelectList(Enumerable.Empty<AnimalVariety>(), "VarietyId", "VarietyName");
            //}
            //// Create a SelectList for gender
            //ViewData["GenderList"] = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            return Page();
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Animals.Add(Animal);
            //await _context.SaveChangesAsync();

            await _animalService.Save(Animal);

            return RedirectToPage("./Index");
        }
    }
}
