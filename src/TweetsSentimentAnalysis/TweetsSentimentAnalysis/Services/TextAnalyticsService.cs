using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetsSentimentAnalysis.Models;

namespace TweetsSentimentAnalysis.Services
{
    public class TextAnalyticsService : ITextAnalyticsService
    {
        public Task<List<ResultModel>> Language(List<ResultModel> result)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultModel>> Sentiment(List<ResultModel> result)
        {
            throw new NotImplementedException();
        }
    }
}
