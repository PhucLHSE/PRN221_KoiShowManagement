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
using KoiShowManagement.Common;

namespace KoiShowManagement.RazorWebApp.Pages.CompetitionCategories
{
    public class DeleteModel : PageModel
    {
        private readonly CompetitionCategoryService _competitionCategoryService;

        public DeleteModel(CompetitionCategoryService competitionCategoryService)
        {
            _competitionCategoryService = competitionCategoryService;
        }

        [BindProperty]
        public CompetitionCategory CompetitionCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionCategory = await _competitionCategoryService.GetById((int)id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionCategory = await _competitionCategoryService.GetById((int)id);
            if (competitionCategory.Status != Const.SUCCESS_READ_CODE || competitionCategory.Data == null)
            {
                return NotFound();

            }
            await _competitionCategoryService.DeleteById((int)id);
            return RedirectToPage("./Index");
        }
    }
}
