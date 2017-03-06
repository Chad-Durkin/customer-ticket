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
            Ticket testTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            testTicket.Save();

            //Act
            List<Ticket> result = Ticket.GetAll();
            List<Ticket> testResult = new List<Ticket>{testTicket};

            //Assert
            Assert.Equal(result, testResult);
        }

        [Fact]
        public void Test_Find()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket testTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            testTicket.Save();

            //Assert
            Assert.Equal(testTicket, Ticket.Find(testTicket.GetId()));
        }

        //Delete Test
        [Fact]
        public void Test_DeleteSpecificTest()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            //Act
            Ticket.Delete(newTicket.GetId());

            List<Ticket> expected = new List<Ticket>();
            List<Ticket> actual = Ticket.GetAll();

            //Assert
            Assert.Equal(expected, actual);
        }

        //Update Ticket Test
        [Fact]
        public void UpdateTicketSeverityTest()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            //Act
            Ticket.UpdateSeverity(newTicket.GetId(), "Medium");

            //Assert
            Assert.Equal("Medium", Ticket.Find(newTicket.GetId()).GetSeverity());
        }


        public void Dispose()
        {
            Ticket.DeleteAll();
            Admin.DeleteAll();
        }
    }
}
