using BusinessLayer.Interface;
using DatabaseLayer.Feedback;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedBackBL :IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedBackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId)
        {
            try
            {
                return feedbackRL.AddFeedback(addFeedback, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FeedbackResponse> GetAllFeedbacks(int bookId)
        {
            try
            {
                return feedbackRL.GetAllFeedbacks(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
