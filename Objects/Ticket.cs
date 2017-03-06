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
            _id = id;
            _ticketNumber = tickNumber;
            _departmentId = departmentId;
            _name = name;
            _address = address;
            _phone = phone;
            _email = email;
            _product = product;
            _severity = severity;
            _description = description;
        }

        public override bool Equals(System.Object otherTicket)
        {
            if(!(otherTicket is Ticket))
            {
                return false;
            }
            else
            {
                Ticket newTicket = (Ticket) otherTicket;
                bool idEquality = this.GetId() == newTicket.GetId();
                bool ticketNumberEquality = this.GetTicketNumber() == newTicket.GetTicketNumber();
                bool nameEquality = this.GetName() == newTicket.GetName();
                bool addressEquality = this.GetAddress() == newTicket.GetAddress();
                bool phoneEquality = this.GetPhone() == newTicket.GetPhone();
                bool emailEquality = this.GetEmail() == newTicket.GetEmail();
                bool productEquality = this.GetProduct() == newTicket.GetProduct();
                bool severityEquality = this.GetSeverity() == newTicket.GetSeverity();
                bool descriptionEquality = this.GetDescription() == newTicket.GetDescription();
                return (idEquality && ticketNumberEquality && nameEquality && addressEquality && phoneEquality && emailEquality && productEquality && severityEquality && descriptionEquality);
            }
        }

        public int GetId()
        {
            return _id;
        }
        public void SetId(int id)
        {
            _id = id;
        }
        public int GetDepartmentId()
        {
            return _departmentId;
        }
        public void SetDepartmentId(int id)
        {
            _departmentId = id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public string GetAddress()
        {
            return _address;
        }
        public void SetAddress(string address)
        {
            _address = address;
        }
        public string GetPhone()
        {
            return _phone;
        }
        public void SetPhone(string phone)
        {
            _phone = phone;
        }
        public string GetEmail()
        {
            return _email;
        }
        public void SetEmail(string email)
        {
            _email = email;
        }
        public string GetProduct()
        {
            return _product;
        }
        public void SetProduct(string product)
        {
            _product = product;
        }
        public string GetSeverity()
        {
            return _severity;
        }
        public void SetSeverity(string severity)
        {
            _severity = severity;
        }
        public string GetDescription()
        {
            return _description;
        }
        public void SetDescription(string description)
        {
            _description = description;
        }


        public void DeleteAll()
        {
            DB.TableDeleteAll("tickets")
        }
    }
}
