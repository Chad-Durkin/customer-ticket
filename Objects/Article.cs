using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Ticketizer
{
    public class Article
    {
        private int _id;
        private string _title;
        private DateTime _dateCreated;
        private string _text;

        public Article(string Title, DateTime DateCreated, string Text, int Id = 0)
        {
            _title = Title;
            _id = Id;
            _dateCreated = DateCreated;
            _text = Text;
        }

        public string GetTitle()
        {
            return _title;
        }

        public int GetId()
        {
            return _id;
        }

        public DateTime GetDate()
        {
            return _dateCreated;
        }

        public string GetText()
        {
            return _text;
        }


        public void AddToTicket(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO tickets_articles (ticket_id, article_id) VALUES (@TicketId, @ArticleId);", conn);
            cmd.Parameters.Add(new SqlParameter("@TicketId", id));
            cmd.Parameters.Add(new SqlParameter("@ArticleId", this.GetId()));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> foundTickets = new List<Ticket>();
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT tickets.* from tickets JOIN tickets_articles ON(tickets.id = tickets_articles.ticket_id) JOIN articles ON (articles.id = tickets_articles.article_id) WHERE articles.id = @ArticleId;", conn);
            cmd.Parameters.Add(new SqlParameter("@ArticleId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int idTicket = rdr.GetInt32(0);
                DateTime ticketNumberTicket = rdr.GetDateTime(1);
                string productTicket = rdr.GetString(2);
                int departmentIdTicket = rdr.GetInt32(3);
                string severityTicket = rdr.GetString(4);
                string descriptionTicket = rdr.GetString(5);
                int userIdTicket = rdr.GetInt32(6);
                Ticket newTicket = new Ticket(ticketNumberTicket, productTicket, descriptionTicket, departmentIdTicket, userIdTicket, severityTicket, idTicket);
                foundTickets.Add(newTicket);
            }

            DB.CloseSqlConnection(conn);

            return foundTickets;

        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO articles (title, date_created, text) OUTPUT INSERTED.id VALUES (@ArticleTitle, @ArticleDate, @ArticleText);", conn);

            cmd.Parameters.Add(new SqlParameter("@ArticleTitle", this.GetTitle()));
            cmd.Parameters.Add(new SqlParameter("@ArticleDate", this.GetDate()));
            cmd.Parameters.Add(new SqlParameter("@ArticleText", this.GetText()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Article Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM articles WHERE id = @ArticleId;", conn);

            cmd.Parameters.Add(new SqlParameter("@ArticleId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundTitle = null;
            DateTime foundDate = new DateTime();
            string foundText = null;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundTitle = rdr.GetString(1);
                foundDate = rdr.GetDateTime(2);
                foundText = rdr.GetString(3);
            }

            Article foundArticle = new Article(foundTitle, foundDate, foundText, foundId);

            DB.CloseSqlConnection(conn, rdr);

            return foundArticle;
        }

        public static List<Article> GetAll()
        {
            List<Article> allArticles = new List<Article>();

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM articles ORDER BY title;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foundId = rdr.GetInt32(0);
                string foundTitle = rdr.GetString(1);
                DateTime foundDate = rdr.GetDateTime(2);
                string foundText = rdr.GetString(3);
                Article foundArticle = new Article(foundTitle, foundDate, foundText, foundId);
                allArticles.Add(foundArticle);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allArticles;
        }



        public static void Update(int id, string newText)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE articles SET text = @NewText OUTPUT INSERTED.text WHERE id = @NoteId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NewText", newText));
            cmd.Parameters.Add(new SqlParameter("@NoteId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM articles WHERE id = @NoteId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NoteId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM articles;", conn);

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public override bool Equals(System.Object otherArticle)
        {
            if(!(otherArticle is Article))
            {
                return false;
            }
            else
            {
                Article newArticle = (Article) otherArticle;
                bool idEquality = this.GetId() == newArticle.GetId();
                bool titleEquality = this.GetTitle() == newArticle.GetTitle();
                bool dateEquality = this.GetDate() == newArticle.GetDate();
                bool textEquality = this.GetText() == newArticle.GetText();
                return (idEquality && titleEquality && dateEquality && textEquality);
            }

        }
    }
}
