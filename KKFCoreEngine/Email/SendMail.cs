﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KKFCoreEngine.Email
{
    public abstract class SendMail
    {
        public static string FROM { get; set; }
        public static string FROMNAME { get; set; }

        private static string SMTP_USERNAME { get; set; }
        private static string SMTP_PASSWORD { get; set; }
        private static string HOST { get; set; }
        private static int PORT = 587;

        public static string Send(string to, string subject, string body)
        {
            FROM = SMTP_USERNAME = "productregistration-noreply@kkfnets.com";
            SMTP_PASSWORD = "KKFabc12345#";
            FROMNAME = "KKF Product Registration";
            HOST = "imail.kkfnets.com";

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.Body = body;

            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            string msg = "Success.";
            using (var client = new SmtpClient(HOST, PORT))
            {
                // Pass SMTP credentials
                client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                client.EnableSsl = true;

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                    msg = ex.Message;
                }
            }
            return msg;
        }
    }
}