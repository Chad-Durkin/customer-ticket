
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Ticketizer
{
    public class UserTest : IDisposable
    {
        public UserTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        //Empty on load
        [Fact]
        public void User_EmptyOnLoad()
        {
            Assert.Equal(0, User.GetAll().Count);
        }

        //Equality Test
        [Fact]
        public void Equality_Test()
        {
            User firstUser = new User("John", "2000 EastLake", "5555555555", "john@someMail.com");
            User secondUser = new User("John", "2000 EastLake", "5555555555", "john@someMail.com");

            Assert.Equal(firstUser, secondUser);
        }

        //Save Test
        [Fact]
        public void SaveTest_UserSave()
        {
            User newUser = new User("John", "2000 EastLake", "5555555555", "john@someMail.com");
            newUser.Save();

            Assert.Equal(newUser, User.GetAll()[0]);
        }

        //Find Test
        [Fact]
        public void FindTest_FindUser()
        {
            User newUser = new User("John", "2000 EastLake", "5555555555", "john@someMail.com");
            newUser.Save();

            Assert.Equal(newUser, User.Find(newUser.GetId()));
        }

        //Delete Test
        [Fact]
        public void DeleteSpecificTest()
        {
            User newUser = new User("John", "2000 EastLake", "5555555555", "john@someMail.com");
            newUser.Save();

            User.Delete(newUser.GetId());

            List<User> expected = new List<User>();
            List<User> actual = User.GetAll();

            Assert.Equal(expected, actual);
        }

        //Update User Test
        [Fact]
        public void UpdateUserNameTest()
        {
            User newUser = new User("John", "2000 EastLake", "5555555555", "john@someMail.com");
            newUser.Save();

            User.Update(newUser.GetId(), "Jenny", "2001 EastLake", "5525555555", "jen@someMail.com");


            Assert.Equal("Jenny", User.Find(newUser.GetId()).GetName());
            Assert.Equal("2001 EastLake", User.Find(newUser.GetId()).GetAddress());
            Assert.Equal("5525555555", User.Find(newUser.GetId()).GetPhone());
            Assert.Equal("jen@someMail.com", User.Find(newUser.GetId()).GetEmail());

        }


        public void Dispose()
        {
            User.DeleteAll();
        }
    }
}
