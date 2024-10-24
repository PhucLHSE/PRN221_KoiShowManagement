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

namespace KoiShowManagement.RazorWebApp.Pages.Competitions
{
    public class EditModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly CompetitionService _competitionService;
        public EditModel(CompetitionService competitionService)
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
            Competition = competition.Data as Competition;
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

            try
            {
                var result = await _competitionService.Save(Competition);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CompetitionExists(Competition.CompetitionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> CompetitionExists(int id)
        {
            var result = await _competitionService.GetById(id);
            return result.Data != null;
        }
    }
}
