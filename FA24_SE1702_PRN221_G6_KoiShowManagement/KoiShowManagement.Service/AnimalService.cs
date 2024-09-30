using KoiShowManagement.Common;
using KoiShowManagement.Data;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagement.Service
{
    public interface IAnimalService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int AnimalId);
        Task<ServiceResult> Save(Animal animal);
        Task<ServiceResult> DeleteById(int AnimalId);
        //Task<ServiceResult> GetAnimalVarieties();
        Task<ServiceResult> GetAllAsync();
    }
    public class AnimalService : IAnimalService
    {
        private readonly UnitOfWork _unitOfWork;

        public AnimalService() => _unitOfWork ??= new UnitOfWork();

        //public AnimalService() 
        //{        
        //    _unitOfWork ??= new UnitOfWork();
        //    if (_unitOfWork == null)
        //    {
        //
        //    }
        //}

        public async Task<ServiceResult> GetAll()
        {
            #region Business Rule

            #endregion Business Rule

            var animals = await _unitOfWork.AnimalRepository.GetAllAsync();

            if (animals == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, animals);
            }
        }

        public async Task<ServiceResult> GetById(int AnimalId)
        {
            #region Business Rule

            #endregion Business Rule

            var animal = await _unitOfWork.AnimalRepository.GetByIdAsync(AnimalId);
            if (animal == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, animal);
            }
        }

        public async Task<ServiceResult> Save(Animal animal)
        {
            try
            {
                #region Business Rule

                #endregion Business Rule

                int result = -1;

                var animalTmp = _unitOfWork.AnimalRepository.GetById(animal.AnimalId);
                //var animalTmp = this.GetById(animal.AnimalId);

                if (animalTmp != null)
                //if (animalTmp.Result.Status == Const.SUCCESS_READ_CODE)
                {
                    result = await _unitOfWork.AnimalRepository.UpdateAsync(animal);

                    if (result > 0) 
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, animal);
                    }
                    else 
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.AnimalRepository.CreateAsync(animal);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, animal);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, animal);
                    }
                }
            }
            catch (Exception ex) 
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<ServiceResult> DeleteById(int AnimalId) 
        {
            try
            {
                var result = false;
                var animalResult = this.GetById(AnimalId);

                if (animalResult != null && animalResult.Result.Status == Const.SUCCESS_READ_CODE)
                {
                    result = await _unitOfWork.AnimalRepository.RemoveAsync((Animal)animalResult.Result.Data);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, animalResult.Result.Data);
                    }
                }
                else
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }                
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        //public async Task<ServiceResult> GetAnimalVarieties()
        //{
        //    #region Business Rule
        //    #endregion Business Rule

        //    var varieties = await _unitOfWork.AnimalVarietyRepository.GetAllAsync();

        //    if (varieties == null)
        //    {
        //        return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<AnimalVariety>());
        //    }
        //    else
        //    {
        //        return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, varieties);
        //    }
        //}

        public async Task<ServiceResult> GetAllAsync()
        {
            try
            {
                var animals = await _unitOfWork.AnimalRepository.GetAlLAnimalsAsync();
                Console.WriteLine($"Number of users retrieved: {animals?.Count}");
                if (animals != null && animals.Count > 0)
                {
                    return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, animals);
                }
                else
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, null);
                }
                //return new Result<List<User>>(users, Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in GetAllUserAsync: {ex.Message}");
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message, null);
                //return new Result<List<User>>(null, Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
