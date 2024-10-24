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
    public class DeleteModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;

        private readonly CompetitionService _competitionService;

        public DeleteModel(CompetitionService competitionService)
        {
            //_context = context;
            _competitionService = competitionService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _competitionService.GetById(id.Value);
            if (competition != null)
            {
                Competition = competition.Data as Competition;
                _competitionService.DeleteById(id.Value);
                await _competitionService.Save(Competition);
            }

            return RedirectToPage("./Index");
        }
    }
}
