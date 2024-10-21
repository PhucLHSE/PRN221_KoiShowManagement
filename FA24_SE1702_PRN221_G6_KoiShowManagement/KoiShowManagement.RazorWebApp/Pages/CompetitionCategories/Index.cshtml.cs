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
    public class IndexModel : PageModel
    {
        private readonly CompetitionCategoryService _competitionCategoryService;

        public IndexModel(CompetitionCategoryService competitionCategoryService)
        {
            _competitionCategoryService = competitionCategoryService;
        }

        public IList<CompetitionCategory> CompetitionCategory { get; set; } = default!;

        public async Task OnGetAsync()
        {

            CompetitionCategory = (await _competitionCategoryService.GetAll()).Data as IList<CompetitionCategory>;
        }
    }
}
