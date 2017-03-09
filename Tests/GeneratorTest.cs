using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Ticketizer
{
    public class GenerateTest : IDisposable
    {
        public GenerateTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void TEST_CreateOneArticle()
        {
            Assert.Equal(20, KAGenerator.GenerateArticle().Count);
        }

        [Fact]
        public void Test_Create50Users()
        {
            KAGenerator.GenerateTickets();

            Random r = new Random();

            Ticket newTicket = new Ticket(DateTime.Now, "Product", "Description", Department.GetAll()[r.Next(0, Department.GetAll().Count - 1)].GetId(), User.GetAll()[r.Next(0, User.GetAll().Count - 1)].GetId());
            newTicket.Save();

            Assert.Equal(50, User.GetAll().Count);
        }


        [Fact]
        public void Test_Create50Tickets()
        {
            KAGenerator.GenerateTickets();

            Assert.Equal(50, Ticket.GetAll().Count);
        }

        public void Dispose()
        {
            Ticket.DeleteAll();
            Admin.DeleteAll();
            User.DeleteAll();
            Department.DeleteAll();
            Article.DeleteAll();
            Note.DeleteAll();
        }
    }
}
