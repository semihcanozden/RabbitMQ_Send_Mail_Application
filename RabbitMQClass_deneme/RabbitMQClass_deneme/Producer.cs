using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQClass_deneme
{
    public class Producer
    {
        public static void getMessage(string MailAdress, string Password, string To, string Subject, string MessageBody)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            IConnection connection = factory.CreateConnection();

            IModel channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "letterbox",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            string message = MailAdress+" "+ Password + " " + To + " " + Subject + " " + MessageBody;
            var encodedMessage = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", "letterbox", null, encodedMessage);

            // Consumer.main();

        }



        public static void Mail(string MailAdress, string Password, string To, string MessageBody, string Subject)
        {

            string mail_body = MailAdress + Password + To + MessageBody + Subject;
            /*
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(MailAdress);
                message.To.Add(To);
                message.Body = MessageBody;
                message.Subject = Subject;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(MailAdress, Password);
                client.Send(message);
                message = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata");
            }
        }*/
        }
    }
}

