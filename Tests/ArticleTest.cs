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
            Assert.Equal(0, Article.GetAll().Count);
        }

        //Equality Test
        [Fact]
        public void Equality_Test()
        {
            Article firstArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            Article secondArticle = new Article("Fixing A Problem", date1, "Here is some article text!");

            Assert.Equal(firstArticle, secondArticle);
        }

        //Save Test
        [Fact]
        public void SaveTest_ArticleSave()
        {
            Article newArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            newArticle.Save();

            Assert.Equal(newArticle, Article.GetAll()[0]);
        }

        //Find Test
        [Fact]
        public void FindTest_FindArticle()
        {
            Article newArticle = new Article("Fixing A Problem", date1, "Here is some article text!");
            newArticle.Save();

            Assert.Equal(newArticle, Article.Find(newArticle.GetId()));
        }


        public void Dispose()
        {
            Article.DeleteAll();
        }
    }
}
