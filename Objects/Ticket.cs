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
        private string _description;
        private string _severity;

        public Ticket(ticketNumber, departmentId, name, address, phone, email, product, description, severity = null, id = 0)
        {
            _id = id;
            _ticketNumber = tickNumber;
            _departmentId = departmentId;
            _name = name;
            _address = address;
            _phone = phone;
            _email = email;
            _product = product;
            _description = description;
            _severity = severity;
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO tickets (ticket_number, department_id, name, address, phone, email, product, description, severity) OUPUT INSERTED.id VALUES (@ticketNumber, @departmentId, @name, @address, @phone, @email, @product, @description, @severity);", conn);

            cmd.Parameters.Add(new SqlParameters("@ticketNumber", this.GetTicketNumber()));
            cmd.Parameters.Add(new SqlParameters("@departmentId", this.GetDepartmentId()));
            cmd.Parameters.Add(new SqlParameters("@name", this.GetName()));
            cmd.Parameters.Add(new SqlParameters("@address", this.GetAddress()));
            cmd.Parameters.Add(new SqlParameters("@phone", this.GetPhone()));
            cmd.Parameters.Add(new SqlParameters("@email", this.GetEmail()));
            cmd.Parameters.Add(new SqlParameters("@product", this.GetProduct()));
            cmd.Parameters.Add(new SqlParameters("@description", this.GetDescription()));
            cmd.Parameters.Add(new SqlParameters("@severity", this.GetSeverity()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public List<Ticket> GetAll()
        {
            List<Ticket> allTickets = new List<Ticket>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tickets", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            int idTicket = 0;
            DateTime ticketNumberTicket = new DateTime();
            int departmentIdTicket = 0;
            string nameTicket = null;
            string addressTicket = null;
            string phoneTicket = null;
            string emailTicket = null;
            string productTicket = null;
            string descriptionTicket = null;
            string severityTicket = null;

            while (rdr.Read())
            {
                idTicket = rdr.GetInt32(0);
                ticketNumberTicket = rdr.GetDateTime(1);
                departmentIdTicket = rdr.GetInt32(2);
                nameTicket = rdr.GetString(3);
                addressTicket = rdr.GetString(4);
                phoneTicket = rdr.GetString(5);
                emailTicket = rdr.GetString(6);
                productTicket = rdr.GetString(7);
                descriptionTicket = rdr.GetString(8);
                severityTicket = rdr.GetString(9);
                Ticket newTicket = new Ticket(ticketNumberTicket, departmentIdTicket, nameTicket, addressTicket, phoneTicket, emailTicket, productTicket, descriptionTicket, severityTicket, idTicket);
                allTickets.Add(newTicket);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allTickets;
        }

        public int GetId()
        {
            return _id;
        }
        public void SetId(int id)
        {
            _id = id;
        }

        public DateTime GetTicketNumber()
        {
            return _ticketNumber;
        }
        public void SetTicketNumber()
        {
            _ticketNumber = ticketNumber;
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
