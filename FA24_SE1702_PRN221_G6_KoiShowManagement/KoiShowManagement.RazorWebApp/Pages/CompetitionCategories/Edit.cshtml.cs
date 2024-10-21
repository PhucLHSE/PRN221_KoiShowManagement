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
using KoiShowManagement.Service;
using KoiShowManagement.Common;

namespace KoiShowManagement.RazorWebApp.Pages.CompetitionCategories
{
    public class EditModel : PageModel
    {
        private readonly CompetitionCategoryService _competitionCategoryService;
        private readonly CompetitionService _competitionService;
        private readonly AnimalVarietyService _animalVarietyService;
        public EditModel(CompetitionCategoryService competitionCategoryService)
        {
            _competitionCategoryService ??= new CompetitionCategoryService();
            _competitionService ??= new CompetitionService();
            _animalVarietyService ??= new AnimalVarietyService();
        }

        [BindProperty]
        public CompetitionCategory CompetitionCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitioncategoryResult = _competitionCategoryService.GetById((int)id);
            if (competitioncategoryResult.Result.Status == Const.SUCCESS_READ_CODE)
            {
                CompetitionCategory = competitioncategoryResult.Result.Data as CompetitionCategory;
            }

            var animalVariety = _animalVarietyService.GetAll().Result.Data as List<AnimalVariety>;
            ViewData["VarietyId"] = new SelectList(animalVariety, "VarietyId", "VarietyName");
            var competitions = _competitionService.GetCompetitionList().Result.Data as List<Competition>;
            ViewData["CompetitionId"] = new SelectList(competitions, "CompetitionId", "CompetitionName");
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
            await _competitionCategoryService.Save(CompetitionCategory);
            return RedirectToPage("./Index");
        }

        private bool CompetitionCategoryExists(int id)
        {
            return _competitionCategoryService.GetById(id) != null;
        }
    }
}
