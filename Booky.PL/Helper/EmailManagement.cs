using System.Net;
using System.Net.Mail;
using Booky.ADL.Models;

namespace Booky.PL.Helper;

public static class EmailManagement
{
    public static void SendEmail(Email email)
    {
        try
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("marselwork29@gmail.com", "pplvzulifgdunpcq");
            client.Send("marselwork29@gmail.com", email.To, email.Subject, email.Body);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            // Log to a file or use a logging library for more extensive logging.
            // You can use ILogger or another logging framework for better logging in a real-world scenario.
        }
    }

}