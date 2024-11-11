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

namespace KoiShowManagement.RazorWebApp.Pages.Feedbacks
{
    public class DetailsModel : PageModel
    {
        private readonly FeedbackService _feedbackService;
        public DetailsModel(FeedbackService feedbackService) => _feedbackService = feedbackService;
     

        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _feedbackService.GetById((int)id);
            if (feedback == null)
            {
                return NotFound();
            }
            else
            {
                Feedback = feedback.Data as Feedback;
            }
            return Page();
        }
    }
}
