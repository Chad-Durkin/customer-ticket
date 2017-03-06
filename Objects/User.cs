using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Ticketizer
{
    public class User
    {
        private int _id;
        private string _name;
        private string _address;
        private string _phone;
        private string _email;

        public User(string name, string address, string phone, string email, int id = 0)
        {
            _id = id;
            _name = name;
            _address = address;
            _phone = phone;
            _email = email;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetAddress()
        {
            return _address;
        }

        public string GetPhone()
        {
            return _phone;
        }

        public string GetEmail()
        {
            return _email;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO users (name, address, phone, email) OUTPUT INSERTED.id VALUES (@UserName, @UserAddress, @UserPhone, @UserEmail);", conn);

            cmd.Parameters.Add(new SqlParameter("@UserName", this.GetName()));
            cmd.Parameters.Add(new SqlParameter("@UserAddress", this.GetAddress()));
            cmd.Parameters.Add(new SqlParameter("@UserPhone", this.GetPhone()));
            cmd.Parameters.Add(new SqlParameter("@UserEmail", this.GetEmail()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static User Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @UserId;", conn);

            cmd.Parameters.Add(new SqlParameter("@UserId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;
            string foundAddress = null;
            string foundPhone = null;
            string foundEmail = null;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundAddress = rdr.GetString(2);
                foundPhone = rdr.GetString(3);
                foundEmail = rdr.GetString(4);
            }

            User foundUser = new User(foundName, foundAddress, foundPhone, foundEmail, foundId);

            DB.CloseSqlConnection(conn, rdr);

            return foundUser;
        }

        public static List<User> GetAll()
        {
            List<User> allUsers = new List<User>();

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM users ORDER BY name;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foundId = rdr.GetInt32(0);
                string foundName = rdr.GetString(1);
                string foundAddress = rdr.GetString(2);
                string foundPhone = rdr.GetString(3);
                string foundEmail = rdr.GetString(4);
                User foundUser = new User(foundName, foundAddress, foundPhone, foundEmail, foundId);
                allUsers.Add(foundUser);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allUsers;
        }


        public static void Update(int id, string newName, string newAddress, string newPhone, string newEmail)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE users SET name = @NewName, address = @NewAddress, phone = @NewPhone, email = @NewEmail WHERE id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NewName", newName));
            cmd.Parameters.Add(new SqlParameter("@NewAddress", newAddress));
            cmd.Parameters.Add(new SqlParameter("@NewPhone", newPhone));
            cmd.Parameters.Add(new SqlParameter("@NewEmail", newEmail));
            cmd.Parameters.Add(new SqlParameter("@UserId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM users;", conn);

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public override bool Equals(System.Object otherUser)
        {
            if(!(otherUser is User))
            {
                return false;
            }
            else
            {
                User newUser = (User) otherUser;
                bool idEquality = this.GetId() == newUser.GetId();
                bool nameEquality = this.GetName() == newUser.GetName();
                bool addressEquality = this.GetAddress() == newUser.GetAddress();
                bool phoneEquality = this.GetPhone() == newUser.GetPhone();
                bool emailEquality = this.GetEmail() == newUser.GetEmail();
                return (idEquality && nameEquality && addressEquality && phoneEquality && emailEquality);
            }

        }
    }
}
