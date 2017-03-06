using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Ticketizer
{
    public class AdminTest : IDisposable
    {

        public AdminTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        //Empty on load
        [Fact]
        public void Admin_EmptyOnLoad()
        {
            Assert.Equal(0, Admin.GetAll().Count);
        }

        //Equality Test
        [Fact]
        public void Equality_Test()
        {
            Admin firstAdmin = new Admin("Johnny English");
            Admin secondAdmin = new Admin("Johnny English");

            Assert.Equal(firstAdmin, secondAdmin);
        }

        //Save Test
        [Fact]
        public void SaveTest_AdminSave()
        {
            Admin newAdmin = new Admin("Johnny English");
            newAdmin.Save();

            Assert.Equal(newAdmin, Admin.GetAll()[0]);
        }

        //Find Test
        [Fact]
        public void FindTest_FindAdmin()
        {
            Admin newAdmin = new Admin("Johnny English");
            newAdmin.Save();

            Assert.Equal(newAdmin, Admin.Find(newAdmin.GetId()));
        }

        //Delete Test
        [Fact]
        public void DeleteSpecificTest()
        {
            Admin newAdmin = new Admin("Johnny English");
            newAdmin.Save();

            Admin.Delete(newAdmin.GetId());

            List<Admin> expected = new List<Admin>();
            List<Admin> actual = Admin.GetAll();

            Assert.Equal(expected, actual);
        }

        //Update Admin Test
        [Fact]
        public void UpdateAdminNameTest()
        {
            Admin newAdmin = new Admin("Johnny English");
            newAdmin.Save();

            Admin.Update(newAdmin.GetId(), "Jenny English");


            Assert.Equal("Jenny English", Admin.Find(newAdmin.GetId()).GetName());
        }


        public void Dispose()
        {
            Admin.DeleteAll();
        }
    }
}
