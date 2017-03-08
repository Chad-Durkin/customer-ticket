// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Web;
// using System.Web.UI;
// using System.Web.UI.WebControls;
// using System.Net;
// using System.Net.Mail;
//
// namespace Ticketizer_Email
// {
//     public partial class WebForm1 : System.Web.UI.Page
//     {
//
//         // private void btnbrowse_Click(object sender, EventArgs exc)
//         // {
//         //     if (openFileDialog1.ShowDialog() == DialogResult.OK)
//         //     {
//         //         txtAttachement.Text = openFileDialog1.FileName.ToString();
//         //     }
//         // }
//
//         protected void Email_Form_Load(object sender, EventArgs e)
//         {
//
//         }
//
//         protected void Send_Button_Click(object sender, EventArgs e)
//         {
//             var fromAddress = new MailAddress("gmkhanna@gmail.com", "Grinil Khanna");
//             var toAddress = new MailAddress("gmkhanna@gmail.com", "Customer");
//             const string fromPassword = "ticketizer";
//             const string subject = "Thanks - now fill out this questionnaire.";
//             const string body = "This is where we thnk you for your business.";
//
//             var smtp = new SmtpClient
//             {
//                 Host = "Smtp.gmail.com";
//                 Port = 587;
//                 EnableSsl = true;
//                 DeliveryMethod = SmtpDeliveryMethod.Network;
//                 UseDefaultCredentials = false;
//                 Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
//                 smtp.Timeout = 10000;
//             }
//
//             using (var message = new MailMessage(fromAddress, toAddress)
//             {
//                 Subject = subject,
//                 Body = body
//                 })
//                 {
//                     smtp.Send(message);
//                     message = null;
//
//                     MessageBox.Show("Perfect. Message Sent");
//                 }
//
//                 catch (Exception e)
//                 {
//                     MessageBox.Show("Ugh. Failed to Send");
//                 }
//             }
//         }
//     }
