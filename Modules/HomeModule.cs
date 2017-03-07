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
            //login
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

                int deptId = 0;

                if(Request.Form["department-id"] != null)
                {
                    deptId = Request.Form["department-id"];
                }

                Ticket testTicket = new Ticket(DateTime.Now, Request.Form["ticket-product"], Request.Form["ticket-description"], deptId, newUser.GetId(), Request.Form["severity"]);
                testTicket.Save();

                return View["index.cshtml", ModelMaker()];
            };

            Get["/ticket/{id}"] = parameters => {
                Dictionary<string, object> model = ModelMaker();
                model.Add("Ticket", Ticket.Find(parameters.id));
                model.Add("User", User.Find(Ticket.Find(parameters.id).GetUserId()));
                model.Add("Department", Department.Find(Ticket.Find(parameters.id).GetDepartmentId()));
                model.Add("Notes", Note.GetAll(parameters.id));

                return View["ticket.cshtml", model];

            };

            Patch["/ticket/{id}"] = parameters => {
                if(Request.Form["open-status"] == 0)
                {
                    Ticket.CloseTicket(parameters.id);
                }

                User.Update(Request.Form["user-id"], Request.Form["name"], Request.Form["address"], Request.Form["phone"], Request.Form["email"]);

                Ticket.UpdateSeverity(parameters.id, Request.Form["severity"]);
                Ticket.UpdateDepartmentId(parameters.id, Request.Form["department-id"]);
                Ticket.UpdateDescription(parameters.id, Request.Form["description"]);
                Ticket.UpdateStatus(parameters.id, Request.Form["current-status"]);


                Dictionary<string, object> model = ModelMaker();
                model.Add("Ticket", Ticket.Find(parameters.id));
                model.Add("User", User.Find(Ticket.Find(parameters.id).GetUserId()));
                model.Add("Department", Department.Find(Ticket.Find(parameters.id).GetDepartmentId()));
                model.Add("Notes", Note.GetAll(parameters.id));

                return View["ticket.cshtml", model];
            };

            Post["/ticket/{id}/add-note"] = parameters => {
                //REQUIRES ADMIN ID
                Note newNote = new Note(DateTime.Now, 1, parameters.id, Request.Form["note"]);
                newNote.Save();

                Dictionary<string, object> model = ModelMaker();
                model.Add("Ticket", Ticket.Find(parameters.id));
                model.Add("User", User.Find(Ticket.Find(parameters.id).GetUserId()));
                model.Add("Department", Department.Find(Ticket.Find(parameters.id).GetDepartmentId()));
                model.Add("Notes", Note.GetAll(parameters.id));

                return View["ticket.cshtml", model];
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
