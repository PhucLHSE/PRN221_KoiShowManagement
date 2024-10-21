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

namespace KoiShowManagement.RazorWebApp.Pages.CompetitionCategories
{
    public class DetailsModel : PageModel
    {
        private readonly CompetitionCategoryService _competitionCategoryService;

        public DetailsModel(CompetitionCategoryService competitionCategoryService)
        {
            _competitionCategoryService = competitionCategoryService;
        }

        public CompetitionCategory CompetitionCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionCategory = await _competitionCategoryService.GetById(id.Value);
            if (competitionCategory == null)
            {
                return NotFound();
            }
            else
            {
                CompetitionCategory = competitionCategory.Data as CompetitionCategory;
            }
            return Page();
        }
    }
}
