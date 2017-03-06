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
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket testTicket = new Ticket(TicketNumber, "John", "2000 EastLake", "5555555555", "john@someMail.com", "Computer", "Bugs", 3);
            testTicket.Save();

            //Act
            List<Ticket> result = Ticket.GetAll();
            List<Ticket> testResult = new List<Ticket>{testTicket};

            Console.WriteLine(result[0].GetProduct());
            Console.WriteLine(testResult[0].GetProduct());

            //Assert
            Assert.Equal(result, testResult);
        }


        public void Dispose()
        {
            Ticket.DeleteAll();
            Admin.DeleteAll();
        }
    }
}
