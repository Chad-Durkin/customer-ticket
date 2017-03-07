using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Ticketizer
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            //index
            Get["/"] = _ => {
                return View["index.cshtml", ModelMaker()];
            };
            //add dept
            Post["/add-department"] = _ => {
                Department newDepartment = new Department(Request.Form["department-name"]);
                newDepartment.Save();

                return View["index.cshtml", ModelMaker()];
            };
            //new ticket form
            Get["/new-ticket"] = _ => {
                return View["new-ticket", ModelMaker()];
            };
            //new ticket creation
            Post["/new-ticket"] = _ => {
                User newUser = new User(Request.Form["user-name"], Request.Form["user-address"], Request.Form["user-phone"], Request.Form["user-email"]);
                newUser.Save();

                int deptId;

                if(Request.Form["department-id"] = null)
                {
                    deptId = 1;
                }

                Ticket testTicket = new Ticket(DateTime.Now, Request.Form["ticket-product"], Request.Form["ticket-description"], deptId, newUser.GetId(), Request.Form["severity"]);
                testTicket.Save();

                return View["index.cshtml", ModelMaker()];
            };

        }

        // Model Maker
        public Dictionary<string, object> ModelMaker()
        {
            Dictionary<string, object> model = new Dictionary<string, object>{
                {"Departments", Department.GetAll()},
                {"Admins", Admin.GetAll()},
                {"Tickets", Ticket.GetAll()}
            };

            return model;
        }
    }
}
