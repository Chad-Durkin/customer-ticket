using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Ticketizer
{
    public class DepartmentTest : IDisposable
    {

        public DepartmentTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        //Empty on load
        [Fact]
        public void Department_EmptyOnLoad()
        {
            Assert.Equal(0, Department.GetAll().Count);
        }

        //Equality Test
        [Fact]
        public void Equality_Test()
        {
            Department firstDepartment = new Department("Sales");
            Department secondDepartment = new Department("Sales");

            Assert.Equal(firstDepartment, secondDepartment);
        }

        //Save Test
        [Fact]
        public void SaveTest_DepartmentSave()
        {
            Department newDepartment = new Department("Sales");
            newDepartment.Save();

            Assert.Equal(newDepartment, Department.GetAll()[0]);
        }

        //Find Test
        [Fact]
        public void FindTest_FindDepartment()
        {
            Department newDepartment = new Department("Sales");
            newDepartment.Save();

            Assert.Equal(newDepartment, Department.Find(newDepartment.GetId()));
        }

        //Delete Test
        [Fact]
        public void DeleteSpecificTest()
        {
            Department newDepartment = new Department("Sales");
            newDepartment.Save();

            Department.Delete(newDepartment.GetId());

            List<Department> expected = new List<Department>();
            List<Department> actual = Department.GetAll();

            Assert.Equal(expected, actual);
        }

        //Update Department Test
        [Fact]
        public void UpdateDepartmentNameTest()
        {
            Department newDepartment = new Department("Sales");
            newDepartment.Save();

            Department.Update(newDepartment.GetId(), "Repair");


            Assert.Equal("Repair", Department.Find(newDepartment.GetId()).GetName());
        }


        public void Dispose()
        {
            Department.DeleteAll();
        }
    }
}
