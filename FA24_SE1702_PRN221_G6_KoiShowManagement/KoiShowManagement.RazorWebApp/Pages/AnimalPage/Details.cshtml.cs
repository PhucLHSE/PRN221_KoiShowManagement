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

namespace KoiShowManagement.RazorWebApp.Pages.AnimalPage
{
    public class DetailsModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly AnimalService _animalService;

        public DetailsModel(AnimalService animalService)
        {
            //_context = context;
            _animalService = animalService;
        }

        public Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }
            if (id == null)
            {
                return NotFound();
            }

            //    var animal = await _context.Animals.FirstOrDefaultAsync(m => m.AnimalId == id);
            var animal = await _animalService.GetById(id.Value);
            // Ensure the returned data is of type Animal
            if (animal.Data is Animal animalResult)
            {
                Animal = animalResult; // Assign to Animal
            }
            else
            {
                return NotFound(); // Handle unexpected type
            }

            //    if (animal == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        Animal = animal;
            //    }

            return Page();
        }
    }
}
