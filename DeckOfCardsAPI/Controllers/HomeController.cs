using DeckOfCardsAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DeckOfCardsAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CardList()
        {
            string APIText = GetListCards();
            List<Card> cards = ConvertListCards(APIText);

            return View(cards);
        }

        public string GetListCards()
        {
            string APIkey = "kk5xbsykhoe4";
            string URL = $"https://deckofcardsapi.com/api/deck/{APIkey}/draw/?count=5";

            HttpWebRequest request = WebRequest.CreateHttp(URL);
            //sometimes need additional information key/login/setting headers/etc

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());


            string APIText = rd.ReadToEnd();

            return APIText;
        }

        public List<Card> ConvertListCards(string APIText)
        {
            JToken json = JToken.Parse(APIText);

            List<JToken> filmTokens = json["cards"].ToList();
            List<Card> CardResults = new List<Card>();

            foreach (JToken j in filmTokens)
            {
                Card c = new Card();
                c.Code = j["code"].ToString();
                c.ImageURL = j["image"].ToString();
                c.Value = j["value"].ToString();
                c.Suit = j["suit"].ToString();

                CardResults.Add(c);
            }

            return CardResults;

        }
    }
}