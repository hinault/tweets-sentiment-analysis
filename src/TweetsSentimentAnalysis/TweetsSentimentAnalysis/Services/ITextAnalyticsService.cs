using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetsSentimentAnalysis.Models;

namespace TweetsSentimentAnalysis.Services
{
    interface ITextAnalyticsService
    {
        Task<IList<ResultModel>> Sentiment(IList<ResultModel> result);

        Task<IList<ResultModel>> Language(IList<string> tweett);
    }
}
