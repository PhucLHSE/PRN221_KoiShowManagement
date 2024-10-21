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

namespace KoiShowManagement.RazorWebApp.Pages.CompetitionCategories
{
    public class CreateModel : PageModel
    {
        private readonly CompetitionCategoryService _competitionCategoryService;
        private readonly CompetitionService _competitionService;
        private readonly AnimalVarietyService _animalVarietyService;

        public CreateModel(CompetitionCategoryService competitionCategoryService)
        {
            _competitionCategoryService ??= new CompetitionCategoryService();
            _competitionService ??= new CompetitionService();
            _animalVarietyService ??= new AnimalVarietyService();
        }

        public IActionResult OnGet()
        {
            var competitions = _competitionService.GetCompetitionList().Result.Data as List<Competition>;
            ViewData["CompetitionId"] = new SelectList(competitions, "CompetitionId", "CompetitionName");

            var animalVariety = _animalVarietyService.GetAll().Result.Data as List<AnimalVariety>;
            ViewData["VarietyId"] = new SelectList(animalVariety, "VarietyId", "VarietyName");
            return Page();
        }

        [BindProperty]
        public CompetitionCategory CompetitionCategory { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _competitionCategoryService.Save(CompetitionCategory);

            return RedirectToPage("./Index");
        }
    }
}
