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

namespace KoiShowManagement.RazorWebApp.Pages.Feedbacks
{
    public class DeleteModel : PageModel
    {
       
        private readonly FeedbackService _feedbackService;

        public DeleteModel(FeedbackService feedbackService) => _feedbackService = feedbackService;

        [BindProperty]
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
           
            Feedback = feedback.Data as Feedback;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _feedbackService.GetById((int)id);
           if(feedback.Status != Const.SUCCESS_READ_CODE || feedback.Data == null)
            {
                return NotFound();
            }

            await _feedbackService.DeleteById((int)id);

            return RedirectToPage("./Index");
        }
    }
}
