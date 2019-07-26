using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TweetsSentimentAnalysis.Models;
using TweetsSentimentAnalysis.Services;

namespace TweetsSentimentAnalysis.Pages
{
    public class IndexModel : PageModel
    {

        /// <summary>
        /// Liste des Tweets
        /// </summary>
        public IList<string> Tweets { get; set; }
        public IList<ResultModel> Results { get; set; }
        /// <summary>
        /// Paramètre de recherche
        /// </summary>
        [Required]
        [StringLength(50)]
        [BindProperty]
        public string Tag { get; set; }

        /// <summary>
        /// Recherche avec Twitter
        /// </summary>
        private readonly ITweetsSearch _tweetsSearch;


        private readonly ITextAnalyticsService _textAnalyticsService;

        /// <summary>
        ///  Initialisation d’une nouvelle instance de la classe PageModel
        /// </summary>
        /// <param name="tweetsSearch">Requis pour injecter le service ITweetsSearch </param>
        public IndexModel(ITweetsSearch tweetsSearch, ITextAnalyticsService textAnalyticsService)
        {

            _tweetsSearch = tweetsSearch;
            _textAnalyticsService = textAnalyticsService;
        }

        /// <summary>
        /// Méthode appelée lors d'une requête Get
        /// </summary>
        public void OnGet()
        {
            Results = new List<ResultModel>();
        }


        /// <summary>
        /// Méthode appelée lors d'une requête Post
        /// </summary>

        public void OnPost()
        {
            Results = new List<ResultModel>();

            Tweets = _tweetsSearch.GetTweets(Tag);

            if(Tweets.Any())
            { 
            Results = _textAnalyticsService.Language(Tweets).Result;

            Results = _textAnalyticsService.Sentiment(Results).Result;
            }
        }
    }
}
