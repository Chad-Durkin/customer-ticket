using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Ticketizer
{
    public class ArticleTest : IDisposable
    {

        public DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);

        public ArticleTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        //Empty on load
        [Fact]
        public void Article_EmptyOnLoad()
        {
            //Assert
            Assert.Equal(0, Article.GetAll().Count);
        }

        //Equality Test
        [Fact]
        public void Equality_Test()
        {
            //Arrange
            Article firstArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            Article secondArticle = new Article("Fixing A Problem", date1, "Here is some article text!");

            //Assert
            Assert.Equal(firstArticle, secondArticle);
        }

        //Save Test
        [Fact]
        public void SaveTest_ArticleSave()
        {
            //Arrange
            Article newArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            newArticle.Save();

            //Assert
            Assert.Equal(newArticle, Article.GetAll()[0]);
        }

        //Find Test
        [Fact]
        public void FindTest_FindArticle()
        {
            //Arrange
            Article newArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            newArticle.Save();

            //Assert
            Assert.Equal(newArticle, Article.Find(newArticle.GetId()));
        }

        //Delete Test
        [Fact]
        public void DeleteSpecificTest()
        {
            //Arrange
            Article newArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            newArticle.Save();

            //Act
            Article.Delete(newArticle.GetId());

            List<Article> expected = new List<Article>();
            List<Article> actual = Article.GetAll();

            //Assert
            Assert.Equal(expected, actual);
        }

        //Update Article Test
        [Fact]
        public void UpdateArticleNameTest()
        {
            //Arrange
            Article newArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            newArticle.Save();

            //Act
            Article.Update(newArticle.GetId(), "Here is new article text!");


            //Assert
            Assert.Equal("Here is new article text!", Article.Find(newArticle.GetId()).GetText());
        }

        //Add knowledge articles to the join table with ticket id
        [Fact]
        public void Add_KnowledgeArticleToTicket()
        {
            //Arrange
            Article newArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            newArticle.Save();
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            //Act
            newArticle.ArticleToTicket(newTicket.GetId());
            List<Ticket> actual = newArticle.GetTickets();
            List<Ticket> expected = new List<Ticket>{newTicket};

            //Assert
            Assert.Equal(actual, expected);


        }

        //Search through articles by keyword
        [Fact]
        public void Search_ForKnowledgeArticlesByKeyWords()
        {
            //Arrange
            Article firstArticle = new Article("Fixing seatbelts", date1, "Here is some article text!");
            firstArticle.Save();
            Article secondArticle = new Article("Have a problem with your seatbelt?", date1, "Here is some article text!");
            secondArticle.Save();
            Article thirdArticle = new Article("What to do when seatbelt malfunctions", date1, "Here is some article text!");
            thirdArticle.Save();
            Article forthArticle = new Article("Fixing a window problem", date1, "Here is some article text!");
            forthArticle.Save();
            Article fifthArticle = new Article("Fixing A door Problem", date1, "Here is some article text!");
            fifthArticle.Save();

            //Act
            List<Article> searchedArticles = Article.SearchKeyWord("seatbelt");
            List<Article> expectedArticles = new List<Article>{firstArticle, secondArticle, thirdArticle};

            //Assert
            Assert.Equal(searchedArticles, expectedArticles);
        }

        //Search through articles by more than one keyword
        [Fact]
        public void Search_ForKnowledgeArticlesByKeyWords()
        {
            //Arrange
            Article firstArticle = new Article("Fixing seatbelts", date1, "Here is some article text!");
            firstArticle.Save();
            Article secondArticle = new Article("Have a problem with your seatbelt?", date1, "Here is some article text!");
            secondArticle.Save();
            Article thirdArticle = new Article("What to do when seatbelt malfunctions", date1, "Here is some article text!");
            thirdArticle.Save();
            Article forthArticle = new Article("Help with a window problem", date1, "Here is some article text!");
            forthArticle.Save();
            Article fifthArticle = new Article("Help with a A door Problem", date1, "Here is some article text!");
            fifthArticle.Save();

            //Act
            List<Article> searchedArticles = Article.SearchKeyWord("seatbelt fixing");
            List<Article> expectedArticles = new List<Article>{firstArticle};

            //Assert
            Assert.Equal(searchedArticles, expectedArticles);
        }



        public void Dispose()
        {
            Article.DeleteAll();
        }
    }
}
