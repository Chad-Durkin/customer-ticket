using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ticketizer
{
    public class TicketTest : IDisposable
    {
        public TicketTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_EmptyAtFirst()
        {
            int result = Ticket.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Save()
        {
            DateTime TicketNumber = DateTime.Now;
            Ticket testTicket = new Ticket(TicketNumber, 3, "John", "2000 EastLake", "5555555555", "john@someMail.com", "Computer", "Bugs");
            testTicket.Save();

            List<Ticket> result = Ticket.GetAll();
            List<Ticket> testResukt = new List<Ticket>{testTicket};

            Assert.Equal(result, testResult);
        }


        public Dispose()
        {
            Admin.DeleteAll();
            Ticket.DeleteAll();
        }
    }
}
