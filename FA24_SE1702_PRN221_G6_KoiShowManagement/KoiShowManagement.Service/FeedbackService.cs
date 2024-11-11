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
    public interface IFeedBackService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> Save(Feedback feedback);
        Task<ServiceResult> DeleteById(int id);

        Task<ServiceResult> GetAnimalsList();
        Task<ServiceResult> GetCompetitionsList();
        Task<ServiceResult> GetUsersList();


    }
    public class FeedbackService :IFeedBackService
    {
        private readonly UnitOfWork _unitOfWork;
        public FeedbackService() => _unitOfWork = new UnitOfWork();

        public async Task<ServiceResult> GetAll()
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetAllWithDetailsAsync();

            if (feedbacks == null || !feedbacks.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedbacks);
        }


        public async Task<ServiceResult> SearchString(string? searchT, string? searchTT, string? searchTTT)
        {
            var feedbacks = (await _unitOfWork.FeedbackRepository.GetAllWithDetailsAsync());
            if (!string.IsNullOrWhiteSpace(searchT))
            {
                if (int.TryParse(searchT, out int userId))
                {
                    feedbacks = feedbacks
                        .Where(f => f.UserId == userId)
                        .ToList();
                }

            }

            if (!string.IsNullOrWhiteSpace(searchTT))
            {
                feedbacks = feedbacks
                    .Where(f => f.Comments != null && f.Comments.Contains(searchTTT, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchTTT))
            {
                if (int.TryParse(searchTTT, out int rating))
                {
                    feedbacks = feedbacks
                        .Where(f => f.Rating == rating)
                        .ToList();
                }

            }
            if (feedbacks.Any())
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedbacks);
            }
            else
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Registration>());
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
            var feedback = await _unitOfWork.FeedbackRepository.GetByIdWithDetailsAsync(id);
            if (feedback == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedback);
            }
        }
        public async Task<ServiceResult> Save(Feedback feedback)
        {
            try
            {
                #region Business Rule

                #endregion Business Rule
                int result = -1;
                var feedbackTmp = _unitOfWork.FeedbackRepository.GetById(feedback.FeedbackId);


                if (feedbackTmp != null)
                {
                    result = await _unitOfWork.FeedbackRepository.UpdateAsync(feedback);
                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, feedback);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.FeedbackRepository.CreateAsync(feedback);
                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, feedback);
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
                var feedbacknResult = _unitOfWork.FeedbackRepository.GetById(id);

                if (feedbacknResult != null)
                {
                    result = await _unitOfWork.FeedbackRepository.RemoveAsync(feedbacknResult);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, feedbacknResult);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, feedbacknResult);
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

        public Task<ServiceResult> GetAnimalsList()
        {
            throw new NotImplementedException();
        }
    }
}
