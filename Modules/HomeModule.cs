using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System.Threading.Tasks;

namespace Ticketizer
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            //Home
            Get["/"] = _ => {
                if(CurrentUser.currentUser.GetVerify())
                {
                    return View["index.cshtml", ModelMaker()];
                }
                else
                {
                    return View["login.cshtml"];
                }
            };
            //Login
            Post["/login"] = _ =>
            {
                if(Admin.VerifyLogin(Request.Form["user-name"], Request.Form["password"]))
                {
                    Admin thisAdmin = Admin.FindByUsername(Request.Form["user-name"]);
                    CurrentUser.currentUser.SetVerify(true);
                    CurrentUser.currentUser.SetAdminid(thisAdmin.GetId());
                    if(CurrentUser.currentUser.GetVerify())
                    {
                        return View["index.cshtml", ModelMaker()];
                    }
                }
                return View["login.cshtml"];
            };
            //Archive
            Get["/archive"] = _ => {
                return View["archive.cshtml", Ticket.GetAllClosed()];
            };
            //Add dept
            Post["/add-department"] = _ => {
                Department newDepartment = new Department(Request.Form["department-name"]);
                newDepartment.Save();

                return View["index.cshtml", ModelMaker()];
            };
            //Tickets
            Get["/new-ticket"] = _ => {
                return View["new-ticket", ModelMaker()];
            };
            //Create Ticket
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
            //Find User
            Post["/find-user"] = _ => {
                User foundUser = User.FindByName(Request.Form["find-user"]);
                Dictionary<string, object> model = ModelMaker();
                model.Add("User", foundUser);
                return View["new-ticket-found-user.cshtml", model];
            };

            //New Ticket with user info populated
            Post["/new-ticket-user"] = _ => {
                User.Update(Request.Form["user-id"], Request.Form["user-name"], Request.Form["user-address"], Request.Form["user-phone"], Request.Form["user-email"]);

                User newUser = User.Find(Request.Form["user-id"]);

                int deptId = 0;

                if(Request.Form["department-id"] != null)
                {
                    deptId = Request.Form["department-id"];
                }

                Ticket testTicket = new Ticket(DateTime.Now, Request.Form["ticket-product"], Request.Form["ticket-description"], deptId, newUser.GetId(), Request.Form["severity"]);
                testTicket.Save();

                return View["index.cshtml", ModelMaker()];
            };
            //Opened Ticket
            Get["/ticket/{id}"] = parameters => {
                Dictionary<string, object> model = ModelMaker();
                model.Add("Ticket", Ticket.Find(parameters.id));
                model.Add("User", User.Find(Ticket.Find(parameters.id).GetUserId()));
                model.Add("Department", Department.Find(Ticket.Find(parameters.id).GetDepartmentId()));
                model.Add("Notes", Note.GetAll(parameters.id));
                model.Add("ArticlesAttached", Article.GetAllAttached(parameters.id));

                return View["ticket.cshtml", model];
            };
            //Search Ticket
            Get["/find-ticket"] = _ => {
                Ticket foundTicket = Ticket.FindByTicketNumber(Request.Query["ticket-number"]);
                if(foundTicket.GetUserId() == 0)
                {
                    return View["new-ticket", ModelMaker()];
                }
                else
                {
                    Dictionary<string, object> model = ModelMaker();
                    model.Add("Ticket", foundTicket);
                    model.Add("User", User.Find(foundTicket.GetUserId()));
                    model.Add("Department", Department.Find(foundTicket.GetDepartmentId()));
                    model.Add("Notes", Note.GetAll(foundTicket.GetId()));
                    model.Add("ArticlesAttached", Article.GetAllAttached(foundTicket.GetId()));

                    return View["ticket.cshtml", model];
                }
            };

            //Edit Ticket
            Patch["/ticket/{id}"] = parameters => {
                if(Request.Form["open-status"] == "0")
                {
                    Ticket.CloseTicket(parameters.id);
                }

                if(Request.Form["open-status"] == "1")
                {
                    Ticket.OpenTicket(parameters.id);
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
                model.Add("ArticlesAttached", Article.GetAllAttached(parameters.id));

                return View["ticket.cshtml", model];
            };
            //Add Note
            Post["/ticket/{id}/add-note"] = parameters => {
                Note newNote = new Note(DateTime.Now, CurrentUser.currentUser.GetAdminId(), parameters.id, Request.Form["note"]);
                newNote.Save();

                Dictionary<string, object> model = ModelMaker();
                model.Add("Ticket", Ticket.Find(parameters.id));
                model.Add("User", User.Find(Ticket.Find(parameters.id).GetUserId()));
                model.Add("Department", Department.Find(Ticket.Find(parameters.id).GetDepartmentId()));
                model.Add("Notes", Note.GetAll(parameters.id));
                model.Add("ArticlesAttached", Article.GetAllAttached(parameters.id));

                return View["ticket.cshtml", model];
            };

            //Articles
            Get["/new-article"] = _ => {
                return View["new-article.cshtml"];
            };
            //Full list for browsing
            Get["/full-article-list"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>{{"Articles", Article.GetAll()}};
                return View["full-article-list.cshtml", model];
            };

            //Search with ticket assigned
            Post["/full-article-list/search"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>{{"Articles", Article.Search(Request.Form["search-field"])}};

                return View["full-article-list.cshtml", model];
            };
            //Specific article
            Get["/articles/{id}"] = parameters => {
                Article model = Article.Find(parameters.id);

                return View["browse-article.cshtml", model];
            };
            //Create article
            Post["/new-article"] = _ => {
                Article newArticle = new Article(Request.Form["title"], DateTime.Now, Request.Form["text"]);
                newArticle.Save();
                return View["index.cshtml", ModelMaker()];
            };
            //View articles and be associated with a ticket
            Get["/ticket/{id}/articles"] = parameters => {
                Dictionary<string, object> model = ModelMaker();
                model.Add("TicketId", parameters.id);
                model.Add("Articles", Article.GetAll());

                return View["article-list.cshtml", model];
            };
            //View specific article and be associated with a ticket
            Get["/ticket/{id}/articles/{articleId}"] = parameters => {

                Dictionary<string, object> model = ModelMaker();
                model.Add("TicketId", parameters.id);
                model.Add("Article", Article.Find(parameters.articleId));

                return View["article.cshtml", model];
            };
            //Assign article to ticket
            Post["/ticket/{id}/articles/{articleId}"] = parameters => {

                Article.Find(parameters.articleId).ArticleToTicket(parameters.id);

                Dictionary<string, object> model = ModelMaker();
                model.Add("Ticket", Ticket.Find(parameters.id));
                model.Add("User", User.Find(Ticket.Find(parameters.id).GetUserId()));
                model.Add("Department", Department.Find(Ticket.Find(parameters.id).GetDepartmentId()));
                model.Add("Notes", Note.GetAll(parameters.id));
                model.Add("ArticlesAttached", Article.GetAllAttached(parameters.id));

                return View["ticket.cshtml", model];
            };
            //Search with ticket assigned
            Post["/ticket/{id}/articles/search"] = parameters => {
                Dictionary<string, object> model = ModelMaker();
                model.Add("TicketId", parameters.id);
                model.Add("Articles", Article.Search(Request.Form["search-field"]));

                return View["article-list.cshtml", model];
            };
            //Get by department category
            Get["/category/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                model.Add("CategoryList", Ticket.GetAllByDept(parameters.id));
                model.Add("Category", Department.Find(parameters.id));
                return View["categorysearch.cshtml", model];
            };
        }

        // Model Maker
        public Dictionary<string, object> ModelMaker()
        {
            Dictionary<string, object> model = new Dictionary<string, object>{
                {"Departments", Department.GetAll()},
                {"Admins", Admin.GetAll()},
                {"Tickets", Ticket.GetAllOpen()}
            };

            return model;
        }
    }
}
