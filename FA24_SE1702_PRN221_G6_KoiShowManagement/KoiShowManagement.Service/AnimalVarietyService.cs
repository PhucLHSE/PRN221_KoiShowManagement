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
    public interface IAnimalVarietyService
    {
        Task<ServiceResult> GetAll();
    }
    public class AnimalVarietyService : IAnimalVarietyService
    {
        private readonly UnitOfWork _unitOfWork;

        public AnimalVarietyService() => _unitOfWork ??= new UnitOfWork();

        public async Task<ServiceResult> GetAll()
        {
            #region Business Rule
            #endregion Business Rule

            var varieties = await _unitOfWork.AnimalVarietyRepository.GetAllAsync();

            if (varieties == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<AnimalVariety>());
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, varieties);
            }
        }
    }
}
