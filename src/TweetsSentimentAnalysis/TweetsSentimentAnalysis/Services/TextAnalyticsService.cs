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
    /// Service pour l'analyse sentimentale
    /// </summary>
    public class TextAnalyticsService : ITextAnalyticsService
    {
        /// <summary>
        /// Journalisation
        /// </summary>
        private readonly ILogger<TextAnalyticsService> _logger;
  
        /// <summary>
        /// Client pour l'accès à l'API TextAnalytics
        /// </summary>
        private readonly ITextAnalyticsClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">Requis pour la journalisation</param>
        /// <param name="client">Requis pour le client TexteAnalytics</param>
        public TextAnalyticsService(ILogger<TextAnalyticsService> logger,  ITextAnalyticsClient client)
        {
            _logger = logger;
            _client = client;
        }
        /// <summary>
        /// Detection de la langue
        /// </summary>
        /// <param name="tweets">Liste des tweets à utiliser</param>
        /// <returns>Liste de type ResultModel</returns>
        public async Task<IList<ResultModel>> Language(IList<string> tweets)
        {
            //Journalisation
            _logger.LogInformation("TextAnalyticsService.Language called");

            //Initialisation d'une liste de LanguageInput pour la requete 
            var languagesInput = new List<LanguageInput>();
          
            //Création du document qui sera analysé. Ajout des tweets au document en attribuant à chacun des identifiant. 
            int i = 1;
            foreach (string tweet in tweets)
            {
                languagesInput.Add(new LanguageInput(id:i.ToString(), text:tweet));
                i++;
            }

            //Passage du document à l'API pour la detection de la langue
            var langResults = await _client.DetectLanguageAsync(false, new LanguageBatchInput(languagesInput));

            //Initialisation de l'objet de retour
            var resultsModel = new List<ResultModel>();

            //AJout des résultats de la detection de la langue au resulatModels
            foreach(var document in langResults.Documents)
            {
                resultsModel.Add(new ResultModel { Language = document.DetectedLanguages[0].Name,
                    LanguageIso = document.DetectedLanguages[0].Iso6391Name, Id = document.Id,
                    Text = tweets.ElementAt(int.Parse(document.Id) - 1) });

                _logger.LogInformation($"Document ID: {document.Id} , Language: {document.DetectedLanguages[0].Name}");
            }

            return resultsModel;
        }


        /// <summary>
        /// Detection des sentiments
        /// </summary>
        /// <param name="result"></param>
        /// <returns>Liste de type ResultModel</returns>
        public async Task<IList<ResultModel>> Sentiment(IList<ResultModel> result)
        {
            //Journalisation
            _logger.LogInformation("TextAnalyticsService.Sentiment called");

            //Initialisation d'une liste de MultiLanguageInput pour la requete 
            var multilanguagesInput = new List<MultiLanguageInput>();

            //Création du document qui sera analysé. Ajout du texte, de la langue du texte et l'ID. 
            foreach (var item in result)
            {
                multilanguagesInput.Add(new MultiLanguageInput(item.LanguageIso, item.Id, item.Text));
            }

            //Passage du document à l'API pour la detectiion des sentiments
            var sentimentResults = await _client.SentimentAsync(
                false,
                new MultiLanguageBatchInput(multilanguagesInput));

            //Lecture des reponses et ajout du score aux resultats 
            foreach (var document in sentimentResults.Documents)
            {
                result.Single(x => x.Id == document.Id).Score = document.Score.Value.ToString();
              
                _logger.LogInformation($"Document ID: {document.Id} , Sentiment Score: {document.Score:0.00}");
            }

            return result;
        }

    }
}
