using Azure.Identity;
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
    public interface ICompetitionService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> Save(Competition competition);
        Task<ServiceResult> DeleteById(int id);
        Task<ServiceResult> GetCompetitionList();
    }
    public class CompetitionService : ICompetitionService
    {
        private readonly UnitOfWork _unitOfWork;

        public CompetitionService() => _unitOfWork ??= new UnitOfWork();

        //public CompetitionService() 
        //{
        //    _unitOfWork ??= new UnitOfWork();
        //}


        public async Task<ServiceResult> GetAll()
        {
            #region Business Rule

            #endregion Business Rule

            var competition = await _unitOfWork.CompetitionsRepository.GetAllAsync();

            if (competition == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Competition>());
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, competition);
            }
        }

        public async Task<ServiceResult> GetById(int id)
        {
            #region Business Rule

            #endregion Business Rule

            var competition = await _unitOfWork.CompetitionsRepository.GetByIdAsync(id);

            if (competition == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, competition);
            }
        }

        public async Task<ServiceResult> Save(Competition competition)
        {
            try
            {
                int result = -1;

                var competitionTmp = this.GetById(competition.CompetitionId);

                if (competitionTmp.Result.Status == Const.SUCCESS_READ_CODE)
                {
                    result = await _unitOfWork.CompetitionsRepository.UpdateAsync(competition);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, competition);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.CompetitionsRepository.CreateAsync(competition);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, competition);
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

                var competitionResult = this.GetById(id);

                if (competitionResult != null && competitionResult.Result.Status == Const.SUCCESS_READ_CODE)
                {
                    result = await _unitOfWork.CompetitionsRepository.RemoveAsync((Competition)competitionResult.Result.Data);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                    }
                    else
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, competitionResult.Result.Data);
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

        public async Task<ServiceResult> GetCompetitionList()
        {
            #region Business Rule

            #endregion Business Rule

            var competition = await _unitOfWork.CompetitionsRepository.GetAllAsync();

            if (competition == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Competition>());
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, competition);
            }
        }
    }
}
