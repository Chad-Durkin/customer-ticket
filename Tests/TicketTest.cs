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

        [Fact]
        public void UpdateTicketDescriptionTest()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            //Act
            Ticket.UpdateDepartmentId(newTicket.GetId(), 5);

            //Assert
            Assert.Equal(5, Ticket.Find(newTicket.GetId()).GetDepartmentId());
        }

        [Fact]
        public void UpdateTicketDepartmentIdTest()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            //Act
            Ticket.UpdateDescription(newTicket.GetId(), "Laptop");

            //Assert
            Assert.Equal("Laptop", Ticket.Find(newTicket.GetId()).GetDescription());
        }

        [Fact]
        public void Test_AddAdminToTicket()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            Admin newAdmin = new Admin("Johnny English", "007", "demo11");
            newAdmin.Save();

            //Act
            newTicket.AddAdmin(newAdmin.GetId());
            int result = newTicket.GetAdmins().Count;

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void UpdateOpenTest()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            //Act
            Ticket.CloseTicket(newTicket.GetId());
            Ticket foundTicket = Ticket.Find(newTicket.GetId());
            //Assert
            Assert.Equal(foundTicket.GetOpen(), 0);
        }

        [Fact]
        public void Test_GetAllOpenTickets()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();
            DateTime TicketNumber1 = new DateTime(2008, 6, 2, 8, 23, 52);
            Ticket newTicket1 = new Ticket(TicketNumber1, "Laptop", "Hardware", 3, 1);
            newTicket1.Save();

            //Act
            int result = Ticket.GetAllOpen().Count;
            int expected = 2;

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Test_GetAllClosedTickets()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();
            DateTime TicketNumber1 = new DateTime(2008, 6, 2, 8, 23, 52);
            Ticket newTicket1 = new Ticket(TicketNumber1, "Laptop", "Hardware", 3, 1);
            newTicket1.Save();
            Ticket.CloseTicket(newTicket.GetId());
            Ticket.CloseTicket(newTicket1.GetId());
            //Act
            int result = Ticket.GetAllClosed().Count;
            int expected = 2;

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void UpdateTicketStatusTest()
        {
            //Arrange
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            //Act
            Ticket.UpdateStatus(newTicket.GetId(), "Issue resolved");

            //Assert
            Assert.Equal("Issue resolved", Ticket.Find(newTicket.GetId()).GetStatus());
        }

        [Fact]
        public void FindTicketByConvertedTicketNumber()
        {
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();

            Ticket foundTicket = Ticket.FindByTicketNumber(newTicket.GetConvertedTicketNum());

            Assert.Equal(newTicket, foundTicket);
        }

        [Fact]
        public void FindNumTicketClose()
        {
            DateTime TicketNumber = new DateTime(2008, 5, 1, 8, 30, 52);
            Ticket newTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            newTicket.Save();
            Ticket otherTicket = new Ticket(TicketNumber, "Computer", "Bugs", 3, 1);
            otherTicket.Save();
            Ticket.CloseTicket(newTicket.GetId());

            Assert.Equal(1, Ticket.GetNumberClosed());
        }

        [Fact]
        public void Test_OpenedThisMonth()
        {
            DateTime DateCurrentOpen = new DateTime(2017, 3, 9, 8, 30, 10);
            DateTime DateLastMonth = new DateTime(2017, 2, 2, 2, 30, 20);
            DateTime DateCurrentClosed = new DateTime(2017, 3, 9, 8, 30, 20);
            Ticket CurrentOpen = new Ticket(DateCurrentOpen, "Computer", "Bugs", 3, 1, "med", 4, 1);
            CurrentOpen.Save();
            Ticket LastMonth = new Ticket(DateLastMonth, "Computer", "Bugs", 3, 1, "med", 2, 1);
            LastMonth.Save();
            Ticket CurrentClosed = new Ticket(DateCurrentClosed, "Computer", "Bugs", 3, 1, "med", 3, 0);
            CurrentClosed.Save();

            int result = Ticket.OpenedThisMonth();
            int expected = 2;

            Assert.Equal(expected, result);
            Console.WriteLine(Ticket.GetAll());
        }

        [Fact]
        public void Test_ClosedThisMonth()
        {
            DateTime DateCurrentOpen = new DateTime(2017, 3, 9, 8, 30, 10);
            DateTime DateLastMonth = new DateTime(2017, 2, 2, 2, 30, 20);
            DateTime DateCurrentClosed = new DateTime(2017, 3, 9, 8, 22, 22);
            Ticket CurrentOpen = new Ticket(DateCurrentOpen, "Computer", "Bugs", 3, 1, "med", 4, 1);
            CurrentOpen.Save();
            Ticket LastMonth = new Ticket(DateLastMonth, "Computer", "Bugs", 3, 1, "med", 2, 1);
            LastMonth.Save();
            Ticket CurrentClosed = new Ticket(DateCurrentClosed, "Computer", "Bugs", 3, 1, "med", 3, 0);
            CurrentClosed.Save();

            int result = Ticket.ClosedThisMonth();
            int expected = 1;

            Assert.Equal(expected, result);
            Console.WriteLine(Ticket.GetAll());
        }

        [Fact]
        public void Test_PercentClosure()
        {
            DateTime DateCurrentOpen = new DateTime(2017, 3, 9, 8, 30, 10);
            DateTime DateLastMonth = new DateTime(2017, 2, 2, 2, 30, 20);
            DateTime DateCurrentClosed = new DateTime(2017, 3, 9, 8, 22, 22);
            Ticket CurrentOpen = new Ticket(DateCurrentOpen, "Computer", "Bugs", 3, 1, "med", 4, 1);
            CurrentOpen.Save();
            Ticket LastMonth = new Ticket(DateLastMonth, "Computer", "Bugs", 3, 1, "med", 2, 1);
            LastMonth.Save();
            Ticket CurrentClosed = new Ticket(DateCurrentClosed, "Computer", "Bugs", 3, 1, "med", 3, 0);
            CurrentClosed.Save();

            int result = Ticket.PercentClosedInMonth();
            int expected = 50;

            Assert.Equal(expected, result);
            Console.WriteLine(Ticket.GetAll());
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
