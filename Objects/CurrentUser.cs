using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Ticketizer
{
    public class CurrentUser
    {
        private int _id;
        private bool _verified;
        private int _adminId;

        public static CurrentUser currentUser = new CurrentUser();

        public CurrentUser(int adminId = 0, bool verify = false, int id = 0)
        {
            _id = id;
            _verified = verify;
            _adminId = adminId;
        }

        public int GetId()
        {
            return _id;
        }

        public bool GetVerify()
        {
            return _verified;
        }

        public int GetAdminId()
        {
            return _adminId;
        }

        public void SetAdminid(int adminId)
        {
            _adminId = adminId;
        }

        public void SetVerify(bool verify)
        {
            _verified = verify;
        }

    }
}
