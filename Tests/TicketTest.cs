using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ticketizer
{
    public class TicketTest : IDisposable
    {
        public BandsTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        public Dispose()
        {
            Admin.DeleteAll();
            Ticket.DeleteAll();
        }
    }
}
