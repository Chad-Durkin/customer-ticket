// using Ticketizer.Models;
// using System.Net;
// using System.Net.Mail;
//
// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<ActionResult> Contact(EmailFormModel model)
// {
//     if (ModelState.IsValid)
//     {
//         var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
//         var message = new MailMessage();
//         message.To.Add(new MailAddress("gmkhanna@gmail.com"));  // replace with valid value
//         message.From = new MailAddress("gmkhanna@gmail.com");  // replace with valid value
//         message.Subject = "Testing";
//         message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
//         message.IsBodyHtml = true;
//
//         using (var smtp = new SmtpClient())
//         {
//             var credential = new NetworkCredential
//             {
//                 UserName = "gmkhanna@gmail.com",  // replace with valid value
//                 Password = "ticketizer"  // replace with valid value
//             };
//             smtp.Credentials = credential;
//             smtp.Host = "Smtp.gmail.com";
//             smtp.Port = 587;
//             smtp.EnableSsl = true;
//             await smtp.SendMailAsync(message);
//             return RedirectToAction("Sent");
//         }
//     }
//     return View(model);
// }
//
// public ActionResult Sent()
// {
//     return View();
// }
