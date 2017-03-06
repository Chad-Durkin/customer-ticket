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
        private string _product;
        private string _description;
        private string _severity;
        private int _userId;

        public Ticket(DateTime ticketNumber, string product, string description, int departmentId, int userId, string severity = "low", int id = 0)
        {
            _id = id;
            _ticketNumber = ticketNumber;
            _departmentId = departmentId;
            _product = product;
            _userId = userId;
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
                bool productEquality = this.GetProduct() == newTicket.GetProduct();
                bool departmentIdEquality = this.GetDepartmentId() == newTicket.GetDepartmentId();
                bool userIdEquality = this.GetUserId() == newTicket.GetUserId();
                bool severityEquality = this.GetSeverity() == newTicket.GetSeverity();
                bool descriptionEquality = this.GetDescription() == newTicket.GetDescription();
                return (idEquality && ticketNumberEquality && productEquality && departmentIdEquality && userIdEquality && severityEquality && descriptionEquality);
            }
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO tickets (ticket_number, product, department_id, user_Id, severity, description) OUTPUT INSERTED.id VALUES (@ticketNumber, @product, @departmentId, @userId, @severity, @description);", conn);

            cmd.Parameters.Add(new SqlParameter("@ticketNumber", this.GetTicketNumber()));
            cmd.Parameters.Add(new SqlParameter("@product", this.GetProduct()));
            cmd.Parameters.Add(new SqlParameter("@departmentId", this.GetDepartmentId()));
            cmd.Parameters.Add(new SqlParameter("@userId", this.GetUserId()));
            cmd.Parameters.Add(new SqlParameter("@severity", this.GetSeverity()));
            cmd.Parameters.Add(new SqlParameter("@description", this.GetDescription()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Ticket Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tickets WHERE id = @TicketId", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int idTicket = 0;
            DateTime ticketNumberTicket = new DateTime();
            int departmentIdTicket = 0;
            string productTicket = null;
            string descriptionTicket = null;
            int userIdTicket = 0;
            string severityTicket = null;

            while (rdr.Read())
            {
                idTicket = rdr.GetInt32(0);
                ticketNumberTicket = rdr.GetDateTime(1);
                productTicket = rdr.GetString(2);
                departmentIdTicket = rdr.GetInt32(3);
                severityTicket = rdr.GetString(4);
                descriptionTicket = rdr.GetString(5);
                userIdTicket = rdr.GetInt32(6);
            }

            Ticket foundTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket);

            DB.CloseSqlConnection(conn, rdr);

            return foundTicket;
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
            string productTicket = null;
            int userIdTicket = 0;
            string descriptionTicket = null;
            string severityTicket = null;

            while (rdr.Read())
            {
                idTicket = rdr.GetInt32(0);
                ticketNumberTicket = rdr.GetDateTime(1);
                productTicket = rdr.GetString(2);
                departmentIdTicket = rdr.GetInt32(3);
                severityTicket = rdr.GetString(4);
                descriptionTicket = rdr.GetString(5);
                userIdTicket = rdr.GetInt32(6);
                Ticket newTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket);
                allTickets.Add(newTicket);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allTickets;
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM tickets WHERE id = @TicketId;", conn);
            cmd.Parameters.Add(new SqlParameter("@TicketId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public static void UpdateSeverity(int ticketId, string newSeverity)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE tickets SET severity = @TicketSeverity WHERE id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketSeverity", newSeverity));
            cmd.Parameters.Add(new SqlParameter("@TicketId", ticketId));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

        }
        public static void UpdateDescription(int ticketId, string newDescription)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE tickets SET description = @TicketDescription WHERE id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketDescription", newDescription));
            cmd.Parameters.Add(new SqlParameter("@TicketId", ticketId));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

        }
        public static void UpdateDepartmentId(int ticketId, int newDepartmentId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE tickets SET depart = @TicketDepartmentId WHERE id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketDepartmentId", newDepartmentId));
            cmd.Parameters.Add(new SqlParameter("@TicketId", ticketId));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

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
        public string GetProduct()
        {
            return _product;
        }
        public void SetProduct(string product)
        {
            _product = product;
        }
        public int GetUserId()
        {
            return _userId;
        }
        public void SetUserId(int userId)
        {
            _userId = userId;
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
