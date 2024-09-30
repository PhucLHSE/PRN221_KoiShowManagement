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
    public class IndexModel : PageModel
    {
        //private readonly KoiShowManagement.Data.DBContext.FA24_SE1702_PRN221_G6_KoiShowManagementContext _context;
        private readonly AnimalService _animalService;

        //Dependency injection
        public IndexModel(AnimalService animalService)
        {
            //_context = context;
            _animalService = animalService;
        }

        //Singleton
        //public IndexModel()
        //{
        //    //_context = context;
        //    _animalService ??= new AnimalService();
        //}


        public IList<Animal> Animal { get;set; } = default!;

        public async Task OnGetAsync()
        {

            //Animal = (IList<Animal>)(await _animalService.GetAll()).Data;
            Animal = (await _animalService.GetAllAsync()).Data as IList<Animal>;
        }

        //public async Task OnGetAsync()
        //{
        //    //Animal = await _context.Animals
        //    //    .Include(a => a.Variety).ToListAsync();

        //    //var result = await _animalService.GetAll();
        //    //Animal = (List<Animal>)result.Data;

        //    //Animal = (IList<Animal>)(await _animalService.GetAll()).Data;
        //    Animal = (await _animalService.GetAll()).Data as IList<Animal>;

        //    //if (result.Status > 0)
        //    //{
        //    //    Animal = (List<Animal>)result.Data;
        //    //}
        //}
    }
}
