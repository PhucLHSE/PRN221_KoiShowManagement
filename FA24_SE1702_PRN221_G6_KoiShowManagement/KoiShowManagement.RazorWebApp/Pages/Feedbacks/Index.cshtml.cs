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
    public class IndexModel : PageModel
    {
        // private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly FeedbackService _feedbackService;

         public IndexModel(FeedbackService feedbackService)=> _feedbackService = feedbackService;

        public IList<Feedback> Feedback { get;set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string? SearchT { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? SearchTT { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? SearchTTT { get; set; } = string.Empty;
        public async Task OnGetAsync()
        {
          
           //Feedback = (await _feedbackService.GetAll()).Data as IList<Feedback>;

            Feedback = (await _feedbackService.SearchString(SearchT, SearchTT, SearchTTT)).Data as IList<Feedback>;

        }
    }
}
