using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Ticketizer
{
    public class Department
    {
        private int _id;
        private string _name;

        public Department(string name, int id = 0)
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

            SqlCommand cmd = new SqlCommand("INSERT INTO departments (name) OUTPUT INSERTED.id VALUES (@DepartmentName);", conn);

            cmd.Parameters.Add(new SqlParameter("@DepartmentName", this.GetName()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Department Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM departments WHERE id = @DepartmentId;", conn);

            cmd.Parameters.Add(new SqlParameter("@DepartmentId", id));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = null;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
            }

            Department foundDepartment = new Department(foundName, foundId);

            DB.CloseSqlConnection(conn, rdr);

            return foundDepartment;
        }

        public static List<Department> GetAll()
        {
            List<Department> allDepartments = new List<Department>();

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM departments ORDER BY name;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foundId = rdr.GetInt32(0);
                string foundName = rdr.GetString(1);
                Department foundDepartment = new Department(foundName, foundId);
                allDepartments.Add(foundDepartment);
            }

            DB.CloseSqlConnection(conn, rdr);

            return allDepartments;
        }

        public static void Update(int id, string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE departments SET name = @NewName OUTPUT INSERTED.name WHERE id = @DepartmentId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NewName", newName));
            cmd.Parameters.Add(new SqlParameter("@DepartmentId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM departments WHERE id = @DepartmentId;", conn);
            cmd.Parameters.Add(new SqlParameter("@DepartmentId", id));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM departments;", conn);

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }


        public override bool Equals(System.Object otherDepartment)
        {
            if(!(otherDepartment is Department))
            {
                return false;
            }
            else
            {
                Department newDepartment = (Department) otherDepartment;
                bool idEquality = this.GetId() == newDepartment.GetId();
                bool nameEquality = this.GetName() == newDepartment.GetName();
                return (idEquality && nameEquality);
            }

        }
    }
}
