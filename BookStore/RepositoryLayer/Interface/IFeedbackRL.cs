using DatabaseLayer.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public AddFeedback AddFeedback(AddFeedback addAddress, int UserId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);
    }
}
