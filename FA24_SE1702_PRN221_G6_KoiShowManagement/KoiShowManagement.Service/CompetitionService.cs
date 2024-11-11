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

        public CompetitionService() => _unitOfWork = new UnitOfWork();

        public async Task<ServiceResult> GetAll()
        {
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
                var competitionTmp = _unitOfWork.CompetitionsRepository.GetById(competition.CompetitionId);

                if (competitionTmp != null)
                {
                    result = await _unitOfWork.CompetitionsRepository.UpdateAsync(competition);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, competition);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
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
                var competitionResult = _unitOfWork.CompetitionsRepository.GetById(id);

                if (competitionResult != null)
                {

                    result = await _unitOfWork.CompetitionsRepository.RemoveAsync(competitionResult);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
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
