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

    public int GetId()
    {
        return _id;
    }
    public void SetId(int id)
    {
        _id = id;
    }
    public string GetName()
    {
        return _string;
    }
    public void SetName(string name)
    {
        _name = name;
    }

    public void DeleteAll()
    {
        DB.TableDeleteAll("admins");
    }
}
