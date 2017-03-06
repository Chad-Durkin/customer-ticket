using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Ticketizer
{
    public class Admin
    {
        private int _id;
        private string _name;

        public Admin(string name, int id = 0)
        {
            _id = id;
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO admins (name) OUTPUT INSERTED.id VALUES (@AdminName);", conn);

            cmd.Parameters.Add(new SqlParameter("@AdminName", this.GetName()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Admin Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM admins WHERE id = @AdminId;", conn);

            cmd.Parameters.Add(new SqlParameter("@AdminId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
            }

            Admin foundAdmin = new Admin(foundName, foundId);

            DB.CloseSqlConnection(conn, rdr);

            return foundAdmin;
        }

        public static List<Admin> GetAll()
        {
            List<Admin> allAdmins = new List<Admin>();

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM admins ORDER BY name;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foundId = rdr.GetInt32(0);
                string foundName = rdr.GetString(1);
                Admin foundAdmin = new Admin(foundName, foundId);
                allAdmins.Add(foundAdmin);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allAdmins;
        }


        public static void Update(int id, string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE admins SET name = @NewName OUTPUT INSERTED.name WHERE id = @AdminId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NewName", newName));
            cmd.Parameters.Add(new SqlParameter("@AdminId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM admins WHERE id = @AdminId;", conn);
            cmd.Parameters.Add(new SqlParameter("@AdminId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM admins;", conn);

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public override bool Equals(System.Object otherAdmin)
        {
            if(!(otherAdmin is Admin))
            {
                return false;
            }
            else
            {
                Admin newAdmin = (Admin) otherAdmin;
                bool idEquality = this.GetId() == newAdmin.GetId();
                bool nameEquality = this.GetName() == newAdmin.GetName();
                return (idEquality && nameEquality);
            }

        }
    }
}
