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
        private int _open;
        private string _status;
        private string _convertedTicketNumber;


        public Ticket(DateTime ticketNumber, string product, string description, int departmentId, int userId, string severity = "low", int id = 0, int open = 1, string status = "Unresolved")
        {
            _id = id;
            _ticketNumber = ticketNumber;
            _departmentId = departmentId;
            _product = product;
            _userId = userId;
            _description = description;
            _severity = severity;
            _open = open;
            _status = status;
            _convertedTicketNumber = ConvertTicketNumber(ticketNumber);
        }

        public static string ConvertTicketNumber(DateTime ticketNumber)
        {
            string newTicket;
            newTicket = ticketNumber.ToString();
            newTicket = newTicket.Replace("/", "");
            newTicket = newTicket.Replace(":", "");
            newTicket = newTicket.Replace(" ", "");
            newTicket = newTicket.Replace("AM", "0");
            newTicket = newTicket.Replace("PM", "1");

            return newTicket;
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
                bool openEquality = this.GetOpen() == newTicket.GetOpen();
                bool statusEquality = this.GetStatus() == newTicket.GetStatus();
                bool convertedTicketNumberEquality =this.GetConvertedTicketNum() == newTicket.GetConvertedTicketNum();
                return (idEquality && ticketNumberEquality && productEquality && departmentIdEquality && userIdEquality && severityEquality && descriptionEquality && openEquality && statusEquality && convertedTicketNumberEquality);
            }
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO tickets (ticket_number, product, department_id, user_Id, severity, description, open_status, status, converted_ticket_number) OUTPUT INSERTED.id VALUES (@ticketNumber, @product, @departmentId, @userId, @severity, @description, @openTicket, @statusTicket, @ConvertedNumber);", conn);

            cmd.Parameters.Add(new SqlParameter("@ticketNumber", this.GetTicketNumber()));
            cmd.Parameters.Add(new SqlParameter("@product", this.GetProduct()));
            cmd.Parameters.Add(new SqlParameter("@departmentId", this.GetDepartmentId()));
            cmd.Parameters.Add(new SqlParameter("@userId", this.GetUserId()));
            cmd.Parameters.Add(new SqlParameter("@severity", this.GetSeverity()));
            cmd.Parameters.Add(new SqlParameter("@description", this.GetDescription()));
            cmd.Parameters.Add(new SqlParameter("@openTicket", this.GetOpen()));
            cmd.Parameters.Add(new SqlParameter("@statusTicket", this.GetStatus()));
            cmd.Parameters.Add(new SqlParameter("@ConvertedNumber", this.GetConvertedTicketNum()));

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
            int openTicket = 0;
            string statusTicket = null;
            string convTicketNum = null;

            while (rdr.Read())
            {
                idTicket = rdr.GetInt32(0);
                ticketNumberTicket = rdr.GetDateTime(1);
                productTicket = rdr.GetString(2);
                departmentIdTicket = rdr.GetInt32(3);
                severityTicket = rdr.GetString(4);
                descriptionTicket = rdr.GetString(5);
                userIdTicket = rdr.GetInt32(6);
                openTicket = rdr.GetByte(7);
                statusTicket = rdr.GetString(8);
                convTicketNum = rdr.GetString(9);
            }

            Ticket foundTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket, openTicket, statusTicket);
            foundTicket.SetConvertedTicketNum(convTicketNum);

            DB.CloseSqlConnection(conn, rdr);

            return foundTicket;
        }

        public static Ticket FindByTicketNumber(string ticketNum)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tickets WHERE converted_ticket_number = @TicketNum", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketNum", ticketNum));

            SqlDataReader rdr = cmd.ExecuteReader();

            int idTicket = 0;
            DateTime ticketNumberTicket = new DateTime();
            int departmentIdTicket = 0;
            string productTicket = null;
            string descriptionTicket = null;
            int userIdTicket = 0;
            string severityTicket = null;
            int openTicket = 0;
            string statusTicket = null;
            string convTicketNum = null;

            while (rdr.Read())
            {
                idTicket = rdr.GetInt32(0);
                ticketNumberTicket = rdr.GetDateTime(1);
                productTicket = rdr.GetString(2);
                departmentIdTicket = rdr.GetInt32(3);
                severityTicket = rdr.GetString(4);
                descriptionTicket = rdr.GetString(5);
                userIdTicket = rdr.GetInt32(6);
                openTicket = rdr.GetByte(7);
                statusTicket = rdr.GetString(8);
                convTicketNum = rdr.GetString(9);
            }

            Ticket foundTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket, openTicket, statusTicket);
            foundTicket.SetConvertedTicketNum(convTicketNum);

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
            int openTicket = 0;
            string statusTicket = null;
            string convTicketNum = null;

            while (rdr.Read())
            {
                idTicket = rdr.GetInt32(0);
                ticketNumberTicket = rdr.GetDateTime(1);
                productTicket = rdr.GetString(2);
                departmentIdTicket = rdr.GetInt32(3);
                severityTicket = rdr.GetString(4);
                descriptionTicket = rdr.GetString(5);
                userIdTicket = rdr.GetInt32(6);
                openTicket = rdr.GetByte(7);
                statusTicket = rdr.GetString(8);
                convTicketNum = rdr.GetString(9);
                Ticket newTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket, openTicket, statusTicket);
                newTicket.SetConvertedTicketNum(convTicketNum);
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

            SqlCommand cmd = new SqlCommand("UPDATE tickets SET department_id = @TicketDepartmentId WHERE id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketDepartmentId", newDepartmentId));
            cmd.Parameters.Add(new SqlParameter("@TicketId", ticketId));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public static void CloseTicket(int ticketId)
        {
            int openStatus = 0;
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE tickets SET open_status = @TicketOpen OUTPUT INSERTED.open_status WHERE id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketOpen", openStatus));
            cmd.Parameters.Add(new SqlParameter("@TicketId", ticketId));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public static void OpenTicket(int ticketId)
        {
            int openStatus = 1;
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE tickets SET open_status = @TicketOpen OUTPUT INSERTED.open_status WHERE id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketOpen", openStatus));
            cmd.Parameters.Add(new SqlParameter("@TicketId", ticketId));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public void AddAdmin(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO tickets_admins (ticket_id, admin_id) VALUES (@TicketId, @AdminId);", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketId", this.GetId()));
            cmd.Parameters.Add(new SqlParameter("@AdminId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public List<Admin> GetAdmins()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT admins.* FROM tickets JOIN tickets_admins ON (tickets.id = tickets_admins.ticket_id) JOIN admins ON (tickets_admins.admin_id = admins.id) WHERE tickets.id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            int adminId = 0;
            string adminName = null;
            string adminUsername = null;
            string adminPassword = null;
            List<Admin> allAdmins = new List<Admin>{};

            while(rdr.Read())
            {
                adminId = rdr.GetInt32(0);
                adminName = rdr.GetString(1);
                adminUsername = rdr.GetString(2);
                adminPassword = rdr.GetString(3);
                Admin foundAdmin = new Admin(adminName, adminUsername, adminPassword, adminId);
                allAdmins.Add(foundAdmin);
            }


            DB.CloseSqlConnection(conn, rdr);

            return allAdmins;
        }

        public static List<Ticket> GetAllByDept(int id)
        {
            List<Ticket> allTickets = new List<Ticket>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT tickets.* FROM tickets JOIN departments ON(tickets.department_id = departments.id) WHERE departments.id = @DeptId", conn);

            cmd.Parameters.Add(new SqlParameter("@DeptId", id));

            SqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                int idTicket = rdr.GetInt32(0);
                DateTime ticketNumberTicket = rdr.GetDateTime(1);
                string productTicket = rdr.GetString(2);
                int departmentIdTicket = rdr.GetInt32(3);
                string severityTicket = rdr.GetString(4);
                string descriptionTicket = rdr.GetString(5);
                int userIdTicket = rdr.GetInt32(6);
                int openTicket = rdr.GetByte(7);
                string ticketStatus = rdr.GetString(8);
                string convTicketNum = rdr.GetString(9);
                Ticket newTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket, openTicket, ticketStatus);
                newTicket.SetConvertedTicketNum(convTicketNum);
                allTickets.Add(newTicket);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allTickets;
        }

        public static List<Ticket> GetAllOpen()
        {
            List<Ticket> allTickets = new List<Ticket>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tickets WHERE open_status = @OpenStatus ORDER BY ticket_number;", conn);

            cmd.Parameters.Add(new SqlParameter("@OpenStatus", "1"));

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int idTicket = rdr.GetInt32(0);
                DateTime ticketNumberTicket = rdr.GetDateTime(1);
                string productTicket = rdr.GetString(2);
                int departmentIdTicket = rdr.GetInt32(3);
                string severityTicket = rdr.GetString(4);
                string descriptionTicket = rdr.GetString(5);
                int userIdTicket = rdr.GetInt32(6);
                int openTicket = rdr.GetByte(7);
                string ticketStatus = rdr.GetString(8);
                string ticketConvNum = rdr.GetString(9);
                Ticket newTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket, openTicket, ticketStatus);
                allTickets.Add(newTicket);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allTickets;
        }

        public static List<Ticket> GetAllClosed()
        {
            List<Ticket> allTickets = new List<Ticket>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tickets WHERE open_status = @OpenStatus ORDER BY ticket_number", conn);

            cmd.Parameters.Add(new SqlParameter("@OpenStatus", "0"));

            SqlDataReader rdr = cmd.ExecuteReader();

            int idTicket = 0;
            DateTime ticketNumberTicket = new DateTime();
            int departmentIdTicket = 0;
            string productTicket = null;
            int userIdTicket = 0;
            string descriptionTicket = null;
            string severityTicket = null;
            int openTicket = 0;
            string ticketStatus = null;
            string convTicketNum = null;

            while (rdr.Read())
            {
                idTicket = rdr.GetInt32(0);
                ticketNumberTicket = rdr.GetDateTime(1);
                productTicket = rdr.GetString(2);
                departmentIdTicket = rdr.GetInt32(3);
                severityTicket = rdr.GetString(4);
                descriptionTicket = rdr.GetString(5);
                userIdTicket = rdr.GetInt32(6);
                openTicket = rdr.GetByte(7);
                ticketStatus = rdr.GetString(8);
                convTicketNum =rdr.GetString(9);
                Ticket newTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket, openTicket, ticketStatus);
                newTicket.SetConvertedTicketNum(convTicketNum);
                allTickets.Add(newTicket);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allTickets;
        }

        public static void UpdateStatus(int ticketId, string newStatus)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE tickets SET status = @TicketStatus WHERE id = @TicketId;", conn);

            cmd.Parameters.Add(new SqlParameter("@TicketStatus", newStatus));
            cmd.Parameters.Add(new SqlParameter("@TicketId", ticketId));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        //Analytics

        public static int GetNumberClosed()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM tickets WHERE open_status = 0;", conn);

            SqlDataReader rdr =cmd.ExecuteReader();

            int numberClosed = 0;
            while(rdr.Read())
            {
                numberClosed = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);

            return numberClosed;
        }

        public static int GetNumberOpen()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM tickets WHERE open_status = 1;", conn);

            SqlDataReader rdr =cmd.ExecuteReader();

            int numberOpen = 0;
            while(rdr.Read())
            {
                numberOpen = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);

            return numberOpen;
        }
        // 
        // public static bool CheckCloseInWeek(DateTime closeDate)
        // {
        //     var currentDay = DateTime.Today;
        //     string DOW = currentDay.dateValue.ToString("ddd");
        //     int increment = 1;
        //
        //     while(DOW != "Sun")
        //     {
        //         currentDay.AddDays(-increment);
        //         string DOW = currentDay.ToString("ddd");
        //         increment ++;
        //     }
        //
        //     var firstDay = currentDay;
        //     var lastDay = firstDay.AddDays(6);
        //
        //     if(closeDate >= firstDay && closeDate <= lastDay)
        //     {
        //         return true;
        //     }
        //     return false;
        // }

        //Getters/Setters
        public int GetId()
        {
            return _id;
        }
        public void SetId(int id)
        {
            _id = id;
        }
        public int GetOpen()
        {
            return _open;
        }
        public string GetStatus()
        {
            return _status;
        }
        public void SetStatus(string status)
        {
            _status = status;
        }
        public void SetOpen(int open)
        {
            _open = open;
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

        public string GetConvertedTicketNum()
        {
            return _convertedTicketNumber;
        }

        public void SetConvertedTicketNum(string newConverted)
        {
            _convertedTicketNumber = newConverted;
        }


        public static void DeleteAll()
        {
            DB.TableDeleteAll("tickets");
        }
    }
}
