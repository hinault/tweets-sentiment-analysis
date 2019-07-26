using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace TweetsSentimentAnalysis.Services
{
    /// <summary>
    /// Service pour la recherche sur Twitter
    /// </summary>
    public class TweetsSearch : ITweetsSearch
    {

        /// <summary>
        /// Journalisation
        /// </summary>
        private readonly ILogger<TweetsSearch> _logger;
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Initialisation d’une nouvelle instance de la classe TweetsSearch
        /// </summary>
        /// <param name="logger">Requis pour la journalisation</param>
        /// <param name="config">Requis pour l'accès aux informations de configuration</param>
        public TweetsSearch(ILogger<TweetsSearch> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
           
        }

        /// <summary>
        /// Méthode pour obtenir les tweets Twitter en fonction du Tag
        /// </summary>
        /// <param name="tag">Paramètre utilisé pour filtrer les tweets</param>
        /// <returns>Retrourne une liste de sting</returns>
        public IList<string> GetTweets(string tag)
        {
            //Authentification de l'application
            Auth.SetUserCredentials(_config.GetValue<string>("Twitter:ConsumerKey"), _config.GetValue<string>("Twitter:ConsumerSecret"),
              _config.GetValue<string>("Twitter:AccesToken"), _config.GetValue<string>("Twitter:AccesTokenSecret"));

            //Journalisation
            _logger.LogInformation(
            "TweetsSearch.GetTweets called. tag: {TAG}",
            tag);

            //Initialisation des paramètres de recherches 
            var searchParameter =  Search.CreateTweetSearchParameter("#"+tag);
            searchParameter.MaximumNumberOfResults = 50;
            
            //Recherche des Tweets
            var tweets = Search.SearchTweets(searchParameter);

          return  tweets!=null && tweets.Any() ? tweets.Select(x => x.FullText).ToList() : new List<string>();
        }
    }
}
