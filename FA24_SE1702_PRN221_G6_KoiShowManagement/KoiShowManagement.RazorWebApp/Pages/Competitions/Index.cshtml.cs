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

namespace KoiShowManagement.RazorWebApp.Pages.Competitions
{
    public class IndexModel : PageModel
    {
        private readonly CompetitionService _competitionService;

        public IndexModel(CompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        public IList<Competition> Competition { get; set; } = default!;

        public async Task OnGetAsync()
        {

            Competition = (await _competitionService.GetAll()).Data as IList<Competition>;

        }
    }
}
