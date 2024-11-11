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

namespace KoiShowManagement.RazorWebApp.Pages.Feedbacks
{
    public class CreateModel : PageModel
    {
        private readonly FeedbackService _feedbackService;

        public CreateModel(FeedbackService feedbackService) => _feedbackService = feedbackService;
        

        public IActionResult OnGet()
        {
            var competions = _feedbackService.GetCompetitionsList().Result.Data as List<Competition>;
            ViewData["CompetitionId"] = new SelectList(competions, "CompetitionId", "CompetitionName");

            var users = _feedbackService.GetUsersList().Result.Data as List<User>;
            ViewData["UserId"] = new SelectList(users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
          

            await _feedbackService.Save(Feedback);

            return RedirectToPage("./Index");
        }
    }
}
