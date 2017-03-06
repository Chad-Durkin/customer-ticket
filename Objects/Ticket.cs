using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Ticketizer
{
    public class Ticket
    {
        private int _id;
        private DateTime _ticketNumber;
        private int _departmentId;
        private string _name;
        private string _address;
        private string _phone;
        private string _email;
        private string _product;
        private string _severity;
        private string _description;

        public Ticket(tickNumber, departmentId, name, address, phone, email, product, severity, description, id = 0)
        {
          _id;
          _ticketNumber;
          _departmentId;
          _name;
          _address;
          _phone;
          _email;
          _product;
          _severity;
          _description;
        }
    }
}
