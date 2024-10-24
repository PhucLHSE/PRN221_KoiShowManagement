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
    public class DetailsModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly CompetitionService _competitionService;
        public DetailsModel(CompetitionService competitionService)
        {
            //_context = context;
            _competitionService = competitionService;
        }

        public Competition Competition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _competitionService.GetById(id.Value);
            if (competition == null)
            {
                return NotFound();
            }
            else
            {
                Competition = competition.Data as Competition;
            }
            return Page();
        }
    }
}
