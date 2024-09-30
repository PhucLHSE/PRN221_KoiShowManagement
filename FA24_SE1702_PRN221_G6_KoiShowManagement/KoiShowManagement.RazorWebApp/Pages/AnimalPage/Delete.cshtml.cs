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

namespace KoiShowManagement.RazorWebApp.Pages.AnimalPage
{
    public class DeleteModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly AnimalService _animalService;

        public DeleteModel(AnimalService animalService)
        {
            //_context = context;
            _animalService = animalService;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var animal = await _context.Animals.FirstOrDefaultAsync(m => m.AnimalId == id);
            var animal = await _animalService.GetById(id.Value);


            if (animal.Status != Const.SUCCESS_READ_CODE)
            {
                return NotFound();
            }

            Animal = (Animal)animal.Data;// Casting Data to Animal as per ServiceResult structure
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _animalService.DeleteById(id.Value);
            if (animal.Status != Const.SUCCESS_DELETE_CODE)
            {
                //Animal = animal;
                //_context.Animals.Remove(Animal);
                //await _context.SaveChangesAsync();
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
