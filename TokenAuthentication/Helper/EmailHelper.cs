using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;

namespace TokenAuthentication.Helper
{
    public class EmailHelper
    {

        public string SendForgotPasswordEmail(string toAddress, string appUrl)
        {
            var resetKey = RandomStringGenerator();
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("TokenAuthentication.Data.ResetPasswordMail.html"));
                var mailBody = reader.ReadToEnd();
                string resetUrl = appUrl + "?resetprocessid=" + resetKey;
                mailBody = mailBody.Replace("{{Link}}", resetUrl);
                var sendMail = SendMailMessage(toAddress, mailBody, "Reset Password Process");
            }
            catch (Exception ex)
            {
                return "";
            }
            return resetKey;
        }

        private bool SendMailMessage(string toAddress, string messageBody, string subject)
        {

            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(toAddress));
                message.Subject = subject;
                message.Body = messageBody;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }



        private string RandomStringGenerator()
        {
            byte[] bytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            string key = string.Join("", bytes.Select(b => b.ToString("X2")));
            return key;
        }

    }
}