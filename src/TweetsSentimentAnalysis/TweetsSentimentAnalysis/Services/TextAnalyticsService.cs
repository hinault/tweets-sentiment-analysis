using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetsSentimentAnalysis.Models;

namespace TweetsSentimentAnalysis.Services
{
    public class TextAnalyticsService : ITextAnalyticsService
    {
        private readonly ILogger<TextAnalyticsService> _logger;
        private readonly IConfiguration _config;
        private readonly ITextAnalyticsClient _client;

        public TextAnalyticsService(ILogger<TextAnalyticsService> logger, IConfiguration config, ITextAnalyticsClient client)
        {
            _config = config;
            _logger = logger;
            _client = client;
        }
        public Task<List<ResultModel>> Language(List<string> tweets)
        {
            throw new NotImplementedException();
        }

      

        public Task<List<ResultModel>> Sentiment(List<ResultModel> result)
        {
            throw new NotImplementedException();
        }

    }
}
