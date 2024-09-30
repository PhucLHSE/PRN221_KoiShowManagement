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
using KoiShowManagement.Common.ENUM;

namespace KoiShowManagement.RazorWebApp.Pages.AnimalPage
{
    public class EditModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly AnimalService _animalService;
        private readonly AnimalVarietyService _animalVarietyService;

        public EditModel(AnimalService animalService, AnimalVarietyService animalVarietyService) => _ = (_animalService = animalService, _animalVarietyService = animalVarietyService);

        //public EditModel(AnimalService animalService, AnimalVarietyService animalVarietyService)
        //{
        //    //_context = context;
        //    _animalService = animalService;
        //    _animalVarietyService = animalVarietyService;
        //}

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    // Use the service to get the Animal by id
        //    var animalResult = _animalService.GetById(id.Value);
        //    Animal = animalResult.Result.Data as Animal;
        //    var animals = _animalService.GetAll().Result.Data as List<AnimalVariety>;
        //    ViewData["VarietyId"] = new SelectList(animals, "VarietyId", "VarietyName");
        //    ViewData["GenderList"] = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
        //    return Page();
        //}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use the service to get the Animal by id
            var animal = await _animalService.GetById(id.Value);
            // Kiểm tra xem kết quả có thành công không
            if (animal.Status != Const.SUCCESS_READ_CODE || animal.Data == null)
            {
                return NotFound();
            }

            // Trích xuất đối tượng Animal từ ServiceResult
            Animal = animal.Data as Animal;
            // Lấy danh sách giống loài động vật và danh sách giới tính để hiển thị trong dropdown
            var animalVarieties = await _animalVarietyService.GetAll();

            if (animalVarieties.Status == Const.SUCCESS_READ_CODE && animalVarieties.Data != null)
            {
                var varieties = animalVarieties.Data as IEnumerable<AnimalVariety>;
                ViewData["VarietyId"] = new SelectList(varieties, "VarietyId", "VarietyName");
            }
            else
            {
                ViewData["VarietyId"] = new SelectList(Enumerable.Empty<AnimalVariety>(), "VarietyId", "VarietyName");
            }

            ViewData["GenderList"] = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
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

            await _animalService.Save(Animal);
            return RedirectToPage("./Index");
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // Reload animal varieties and gender lists if there's a validation error
        //        var animalVarieties = await _animalVarietyService.GetAll();
        //        if (animalVarieties.Status == Const.SUCCESS_READ_CODE && animalVarieties.Data != null)
        //        {
        //            var varieties = animalVarieties.Data as IEnumerable<AnimalVariety>;
        //            ViewData["VarietyId"] = new SelectList(varieties, "VarietyId", "VarietyName");
        //        }
        //        else
        //        {
        //            ViewData["VarietyId"] = new SelectList(Enumerable.Empty<AnimalVariety>(), "VarietyId", "VarietyName");
        //        }

        //        ViewData["GenderList"] = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
        //        return Page();
        //    }

        //    try
        //    {
        //        await _animalService.Save(Animal);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await AnimalExists(Animal.AnimalId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("./Index");
        //}

        private async Task<bool> AnimalExists(int id)
        {
            var animalResult = await _animalService.GetById(id);
            return animalResult.Status == Const.SUCCESS_READ_CODE && animalResult.Data != null;
        }
    }
}
