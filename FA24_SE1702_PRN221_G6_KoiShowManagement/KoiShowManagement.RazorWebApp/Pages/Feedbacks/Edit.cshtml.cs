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

namespace KoiShowManagement.RazorWebApp.Pages.Feedbacks
{
    public class EditModel : PageModel
    {
        private readonly FeedbackService _feedbackService;

        public EditModel(FeedbackService feedbackService) => _feedbackService = feedbackService;

       
        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackResult = _feedbackService.GetById(id);
            if (feedbackResult.Result.Status == Const.SUCCESS_READ_CODE)
            {
               Feedback = feedbackResult.Result.Data as Feedback;
            }

            var competions = _feedbackService.GetCompetitionsList().Result.Data as List<Competition>;
            ViewData["CompetitionId"] = new SelectList(competions, "CompetitionId", "CompetitionName");

            var users = _feedbackService.GetUsersList().Result.Data as List<User>;
            ViewData["UserId"] = new SelectList(users, "UserId", "Email");

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

            await _feedbackService.Save(Feedback);
           
       

            return RedirectToPage("./Index");
        }

        private bool FeedbackExists(int id)
        {
            return _feedbackService.GetById(id) != null;
        }
    }
}
