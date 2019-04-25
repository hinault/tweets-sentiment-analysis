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

        public TextAnalyticsService(ILogger<TextAnalyticsService> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
        }
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
