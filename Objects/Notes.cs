using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Ticketizer
{
    public class Note
    {
        private int _id;
        private DateTime _dateCreated;
        private int _adminId;
        private int _ticketId;
        private string _text;

        // begin Constructors and Getters
        public Note(DateTime dateCreated, int adminId, int ticketId, string text, int id = 0)
        {
            _id = id;
            _dateCreated = dateCreated;
            _adminId = adminId;
            _ticketId = ticketId;
            _text = text;
        }

        public int GetId()
        {
            return _id;
        }

        public DateTime GetDate()
        {
            return _dateCreated;
        }

        public int GetAdminId()
        {
            return _adminId;
        }

        public int GetTicketId()
        {
            return _ticketId;
        }

        public string GetText()
        {
            return _text;
        }
        // end Constructors and Getters

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO notes (date_created, admin_id, ticket_id, text) OUTPUT INSERTED.id VALUES (@NoteDate, @AdminId, @TicketId, @NoteText);", conn);

            cmd.Parameters.Add(new SqlParameter("@NoteDate", this.GetDate()));
            cmd.Parameters.Add(new SqlParameter("@AdminId", this.GetAdminId()));
            cmd.Parameters.Add(new SqlParameter("@TicketId", this.GetTicketId()));
            cmd.Parameters.Add(new SqlParameter("@NoteText", this.GetText()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Note Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM notes WHERE id = @NoteId;", conn);

            cmd.Parameters.Add(new SqlParameter("@NoteId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            DateTime foundDate = new DateTime();
            int foundAdminId = 0;
            int foundTicketId = 0;
            string foundText = null;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundDate = rdr.GetDateTime(1);
                foundAdminId = rdr.GetInt32(2);
                foundTicketId = rdr.GetInt32(3);
                foundText = rdr.GetString(4);
            }

            Note foundNote = new Note(foundDate, foundAdminId, foundTicketId, foundText, foundId);

            DB.CloseSqlConnection(conn, rdr);

            return foundNote;
        }

        public static List<Note> GetAll()
        {
            List<Note> allNotes = new List<Note>();

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM notes ORDER BY date_created;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foundId = rdr.GetInt32(0);
                DateTime foundDate = rdr.GetDateTime(1);
                int foundAdminId = rdr.GetInt32(2);
                int foundTicketId = rdr.GetInt32(3);
                string foundText = rdr.GetString(4);
                Note foundNote = new Note(foundDate, foundAdminId, foundTicketId, foundText, foundId);
                allNotes.Add(foundNote);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allNotes;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM notes;", conn);

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public override bool Equals(System.Object otherNote)
        {
            if(!(otherNote is Note))
            {
                return false;
            }
            else
            {
                Note newNote = (Note) otherNote;
                bool idEquality = this.GetId() == newNote.GetId();
                bool dateEquality = this.GetDate() == newNote.GetDate();
                bool adminIdEquality = this.GetAdminId() == newNote.GetAdminId();
                bool ticketIdEquality = this.GetTicketId() == newNote.GetTicketId();
                bool textEquality = this.GetText() == newNote.GetText();
                return (idEquality && adminIdEquality && ticketIdEquality && dateEquality && textEquality);
            }

        }
    }

}
