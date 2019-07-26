using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetsSentimentAnalysis.Models;

namespace TweetsSentimentAnalysis.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TextAnalyticsService : ITextAnalyticsService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<TextAnalyticsService> _logger;
  
        /// <summary>
        /// 
        /// </summary>
        private readonly ITextAnalyticsClient _client;

        public TextAnalyticsService(ILogger<TextAnalyticsService> logger,  ITextAnalyticsClient client)
        {
            _logger = logger;
            _client = client;
        }
        public async Task<IList<ResultModel>> Language(IList<string> tweets)
        {

            _logger.LogInformation("TextAnalyticsService.Language called");

            var languagesInput = new List<LanguageInput>();

          
            int i = 1;
            foreach (string tweet in tweets)
            {
                languagesInput.Add(new LanguageInput(id:i.ToString(), text:tweet));
                i++;
            }

            var langResults = await _client.DetectLanguageAsync(false, new LanguageBatchInput(languagesInput));

            var resultsModel = new List<ResultModel>();

            foreach(var document in langResults.Documents)
            {
                resultsModel.Add(new ResultModel { Language = document.DetectedLanguages[0].Name,
                    LanguageIso = document.DetectedLanguages[0].Iso6391Name, Id = document.Id,
                    Text = tweets.ElementAt(int.Parse(document.Id) - 1) });

                _logger.LogInformation($"Document ID: {document.Id} , Language: {document.DetectedLanguages[0].Name}");
            }

            return resultsModel;
        }

      

        public async Task<IList<ResultModel>> Sentiment(IList<ResultModel> result)
        {
            _logger.LogInformation("TextAnalyticsService.Sentiment called");

            var multilanguagesInput = new List<MultiLanguageInput>();

            foreach(var item in result)
            {
                multilanguagesInput.Add(new MultiLanguageInput(item.LanguageIso, item.Id, item.Text));
            }

            var sentimentResults = await _client.SentimentAsync(
                false,
                new MultiLanguageBatchInput(multilanguagesInput));

            foreach (var document in sentimentResults.Documents)
            {
                result.Single(x => x.Id == document.Id).Score = document.Score.Value.ToString();
              
                _logger.LogInformation($"Document ID: {document.Id} , Sentiment Score: {document.Score:0.00}");
            }

            return result;
        }

    }
}
