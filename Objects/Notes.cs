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
    }

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

    public DateTime GetDateCreated()
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


}
