using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetsSentimentAnalysis.Models;

namespace TweetsSentimentAnalysis.Services
{
    interface ITextAnalyticsService
    {
        Task<List<ResultModel>> Sentiment(List<ResultModel> result);

        Task<List<ResultModel>> Language(List<string> tweett);
    }
}
