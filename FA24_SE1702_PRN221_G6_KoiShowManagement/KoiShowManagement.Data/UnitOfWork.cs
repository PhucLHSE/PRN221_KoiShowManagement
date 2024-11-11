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
        private RegistrationsRepository _registrationsRepository;
        private CompetitionsRepository _competitionsRepository;
        private UsersRepository _usersRepository;
        private FeedbackRepository _feedbackRepository;
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
        public RegistrationsRepository RegistrationsRepository
        {
            get
            {
                return _registrationsRepository ??= new Repository.RegistrationsRepository(_unitOfWorkContext);

            }
        }
        public FeedbackRepository FeedbackRepository
        {
            get
            {
                return _feedbackRepository ??= new Repository.FeedbackRepository(_unitOfWorkContext);

            }
        }
        public CompetitionsRepository CompetitionsRepository
        {
            get
            {
                return _competitionsRepository ??= new Repository.CompetitionsRepository(_unitOfWorkContext);

            }
        }
        public UsersRepository UsersRepository
        {
            get
            {
                return _usersRepository ??= new Repository.UsersRepository(_unitOfWorkContext);

            }
        }
    }
}
