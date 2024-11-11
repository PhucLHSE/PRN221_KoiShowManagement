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

        [BindProperty(SupportsGet = true)]
        public string CompetitionName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Description { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }

        public IList<Competition> Competition { get; set; } = default!;

        public async Task OnGetAsync()
        {

            var result = await _competitionService.GetAll();

            if (result != null)
            {
                var competitions = result.Data as IList<Competition>;

                // Apply filters if search parameters are provided
                if (!string.IsNullOrEmpty(CompetitionName))
                {
                    competitions = competitions.Where(c => c.CompetitionName.Contains(CompetitionName, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(Description))
                {
                    competitions = competitions.Where(c => c.Description.Contains(Description, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(Title))
                {
                    competitions = competitions.Where(c => c.Title.Contains(Title, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                Competition = competitions;
            }
            else
            {
                Competition = new List<Competition>();
                // Optionally handle the error message from the result
            }

        }
    }
}
