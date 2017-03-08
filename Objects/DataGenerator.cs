using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Ticketizer
{
    public class KAGenerator
    {
        public static List<Article> GenerateArticle()
        {
            List<Article> fakeNews = new List<Article>{};
            for(var i = 0; i< titleList.Count; i++)
            {
                string title = titleList[0];
                DateTime date = dateList[0];
                string content = texts[0];
                Article tempArticle = new Article(title, date, content);
                fakeNews.Add(tempArticle);
            }

            return fakeNews;
        }

// data
        List<string> titleList = new List<string>
        {
            "Get achievements, play anywhere"
        };


        List<DateTime> dateList = new List<DateTime>{};
        DateTime today = new DateTime(2016, 09, 07);
        List<DateTime> dateList.Add(today);


        List<string> texts = new List<string>
        {
            "Many of the same free and paid games are available across all Windows 10 devices—no matter which game is your favorite, chances are you can play it on any screen. When you're signed in to an Xbox Live-enabled game with your Microsoft account, your game progress saves to the cloud. If you're in the middle of a game on your Windows 10 phone and are interrupted, you can pick it up again later on your PC, right where you left off. Any achievements you pick up—for milestones in a game, or for special challenges—add to your total gamerscore, yours to keep. Compete on game leaderboards with your friends and people around the world on Xbox Live. To see the achievements, select Start Start symbol button, then select Xbox Image of Xbox logo. Select My games from the menu, choose the game, and then select Achievements. Pick individual achievements to see a brief description about how to unlock them."
        };
    }


}
