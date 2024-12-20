﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Service;

namespace KoiShowManagement.RazorWebApp.Pages.Registrations
{
    public class IndexModel : PageModel
    {
        private readonly RegistrationService _registrationService;

        public IndexModel(RegistrationService registrationService) => _registrationService = registrationService;
        public IList<Registration> Registration { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchT { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? SearchTT { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? SearchTTT { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {

            //Registration = (await _registrationService.GetAll()).Data as IList<Registration>;

            Registration = (await _registrationService.searchString(SearchT, SearchTT, SearchTTT)).Data as IList<Registration>;


        }
    }
}
