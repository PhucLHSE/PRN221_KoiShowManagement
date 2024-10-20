using KoiShowManagement.Common;
using KoiShowManagement.Data.Models;
using KoiShowManagement.Data;
using KoiShowManagement.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagement.Service
{
    public interface IRegistrationService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> Save(Registration registration);
        Task<ServiceResult> DeleteById(int id);

        Task<ServiceResult> GetAnimalsList();
        Task<ServiceResult> GetCompetitionsList();
        Task<ServiceResult> GetUsersList();


    }
    public class RegistrationService : IRegistrationService
    {
        private readonly UnitOfWork _unitOfWork;
        public RegistrationService() => _unitOfWork = new UnitOfWork();
    
        public async Task<ServiceResult> GetAll()
        {
            var registrations = await _unitOfWork.RegistrationsRepository.GetAllWithDetailsAsync();

            if (registrations == null || !registrations.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, registrations);
        }



        public async Task<ServiceResult> GetAnimalsList()
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

        public async Task<ServiceResult> GetCompetitionsList()
        {
            #region Business Rule

            #endregion Business Rule

            var competitions = await _unitOfWork.CompetitionsRepository.GetAllAsync();
            if (competitions == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, competitions);
            }
        }
        public async Task<ServiceResult> GetUsersList()
        {
            #region Business Rule

            #endregion Business Rule

            var users = await _unitOfWork.UsersRepository.GetAllAsync();
            if (users == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users);
            }
        }




        public async Task<ServiceResult> GetById(int id)
        {
            #region Business Rule

            #endregion Business Rule
            var registration = await _unitOfWork.RegistrationsRepository.GetByIdWithDetailsAsync(id);
            if (registration == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, registration);
            }
        }
        public async Task<ServiceResult> Save(Registration registration)
        {
            try
            {
                #region Business Rule

                #endregion Business Rule
                int result = -1;
                var registrationTmp = _unitOfWork.RegistrationsRepository.GetById(registration.RegistrationId);


                if (registrationTmp != null)
                {
                    result = await _unitOfWork.RegistrationsRepository.UpdateAsync(registration);
                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, registration);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.RegistrationsRepository.CreateAsync(registration);
                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, registration);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }

            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }

        }

        public async Task<ServiceResult> DeleteById(int id)
        {
            try
            {
                var result = false;
                var registrationResult = _unitOfWork.RegistrationsRepository.GetById(id);

                if (registrationResult != null)
                {
                    result = await _unitOfWork.RegistrationsRepository.RemoveAsync(registrationResult);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, registrationResult);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, registrationResult);
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
    }
}
