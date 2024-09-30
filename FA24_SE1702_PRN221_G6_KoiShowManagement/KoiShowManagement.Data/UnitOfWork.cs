using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagement.Data
{
    public class UnitOfWork
    {
        private FA24_SE1702_PRN221_G6_KoiShowManagementContext _unitOfWorkContext;
        private AnimalRepository _animalRepository;
        private AnimalVarietyRepository _animalVarietyRepository;
        public UnitOfWork() 
        {
            _unitOfWorkContext ??= new FA24_SE1702_PRN221_G6_KoiShowManagementContext();
        }

        public AnimalRepository AnimalRepository
        {
            get
            {
                return _animalRepository ??= new Repository.AnimalRepository(_unitOfWorkContext);
            }
            
        }

        public AnimalVarietyRepository AnimalVarietyRepository
        {
            get
            {
                return _animalVarietyRepository ??= new Repository.AnimalVarietyRepository(_unitOfWorkContext);
            }
        }

    }
}
