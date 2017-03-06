using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

        public Ticket(DateTime ticketNumber, string name, string address, string phone, string email, string product, string description, int departmentId, string severity = "low", int id = 0)
        {
            _id = id;
            _ticketNumber = ticketNumber;
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
                bool departmentIdEquality = this.GetDepartmentId() == newTicket.GetDepartmentId();
                bool severityEquality = this.GetSeverity() == newTicket.GetSeverity();
                bool descriptionEquality = this.GetDescription() == newTicket.GetDescription();
                return (idEquality && ticketNumberEquality && nameEquality && addressEquality && phoneEquality && emailEquality && productEquality && departmentIdEquality && severityEquality && descriptionEquality);
            }
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO tickets (ticket_number, name, address, phone, email, product, department_id, severity, description) OUTPUT INSERTED.id VALUES (@ticketNumber, @name, @address, @phone, @email, @product, @departmentId, @severity, @description);", conn);

            cmd.Parameters.Add(new SqlParameter("@ticketNumber", this.GetTicketNumber()));
            cmd.Parameters.Add(new SqlParameter("@name", this.GetName()));
            cmd.Parameters.Add(new SqlParameter("@address", this.GetAddress()));
            cmd.Parameters.Add(new SqlParameter("@phone", this.GetPhone()));
            cmd.Parameters.Add(new SqlParameter("@email", this.GetEmail()));
            cmd.Parameters.Add(new SqlParameter("@product", this.GetProduct()));
            cmd.Parameters.Add(new SqlParameter("@departmentId", this.GetDepartmentId()));
            cmd.Parameters.Add(new SqlParameter("@severity", this.GetSeverity()));
            cmd.Parameters.Add(new SqlParameter("@description", this.GetDescription()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static List<Ticket> GetAll()
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
                nameTicket = rdr.GetString(2);
                addressTicket = rdr.GetString(3);
                phoneTicket = rdr.GetString(4);
                emailTicket = rdr.GetString(5);
                productTicket = rdr.GetString(6);
                departmentIdTicket = rdr.GetInt32(7);
                severityTicket = rdr.GetString(8);
                descriptionTicket = rdr.GetString(9);
                Ticket newTicket = new Ticket(ticketNumberTicket, nameTicket, addressTicket, phoneTicket, emailTicket, productTicket, descriptionTicket, departmentIdTicket, severityTicket, idTicket);
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
        public void SetTicketNumber(DateTime ticketNumber)
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


        public static void DeleteAll()
        {
            DB.TableDeleteAll("tickets");
        }
    }
}
