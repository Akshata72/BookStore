using DatabaseLayer.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public AddFeedback AddFeedback(AddFeedback addAddress, int UserId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);
    }
}
