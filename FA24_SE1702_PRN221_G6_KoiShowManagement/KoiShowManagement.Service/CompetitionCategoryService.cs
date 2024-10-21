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
    public interface ICompetitionCategoryService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> Save(CompetitionCategory competitionCategory);
        Task<ServiceResult> DeleteById(int id);
        Task<ServiceResult> GetCompetitionCategoryList();
    }
    public class CompetitionCategoryService : ICompetitionCategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CompetitionCategoryService() => _unitOfWork ??= new UnitOfWork();

        public async Task<ServiceResult> GetAll()
        {
            #region Business Rule

            #endregion Business Rule

            var competitionCategory = await _unitOfWork.CompetitionCategoryRepository.GetAllWithDetailsAsync();

            if (competitionCategory == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<CompetitionCategory>());
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, competitionCategory);
            }
        }

        public async Task<ServiceResult> GetById(int id)
        {
            #region Business Rule

            #endregion Business Rule

            var competitionCategory = await _unitOfWork.CompetitionCategoryRepository.GetByIdAsync(id);

            if (competitionCategory == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, competitionCategory);
            }
        }

        public async Task<ServiceResult> Save(CompetitionCategory competitionCategory)
        {
            try
            {
                int result = -1;

                var competitionTmp = _unitOfWork.CompetitionCategoryRepository.GetById(competitionCategory.CategoryId);

                if (competitionTmp != null)
                {
                    result = await _unitOfWork.CompetitionCategoryRepository.UpdateAsync(competitionCategory);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, competitionCategory);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.CompetitionCategoryRepository.CreateAsync(competitionCategory);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, competitionCategory);
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

                var competitionResult = _unitOfWork.CompetitionCategoryRepository.GetById(id);

                if (competitionResult != null)
                {
                    // Thực hiện xóa dữ liệu
                    result = await _unitOfWork.CompetitionCategoryRepository.RemoveAsync(competitionResult);

                    // Kiểm tra kết quả của quá trình xóa
                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                    }
                    else
                    {
                        // Trả về thông báo lỗi nếu không thể xóa
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    // Trả về cảnh báo nếu không tìm thấy dữ liệu
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về kết quả lỗi
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<ServiceResult> GetCompetitionCategoryList()
        {
            #region Business Rule

            #endregion Business Rule

            var competitionCategory = await _unitOfWork.CompetitionCategoryRepository.GetAllAsync();

            if (competitionCategory == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Competition>());
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, competitionCategory);
            }
        }
    }
}
