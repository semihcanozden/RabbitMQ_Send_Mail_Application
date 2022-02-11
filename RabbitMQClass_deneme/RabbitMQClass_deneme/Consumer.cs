using System;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;

namespace RabbitMQClass_deneme
{    
    public class Consumer
    {
        public static void main()
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

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "letterbox", autoAck: false, consumer: consumer);

            NetworkCredential login;
            SmtpClient client;
            MailMessage msg;

            ulong previousTag = 1;
            ulong newTag;

            var isGone = false;
            var justmail = false;

            while (isGone == false)
            {
                consumer.Received += (model, ea) =>
                {

                    if (previousTag == ea.DeliveryTag)
                    {
                        newTag = previousTag;
                        var body = ea.Body.ToArray();
                        string[] message = Encoding.UTF8.GetString(body).Split(" ");
                        var email = message[0];
                        var pass = message[1];
                        var to = message[2];
                        var subject = message[3];
                        var messagebody = message[4];
                        
                        if (justmail == false)
                        {
                            var getSmtp = email.Split("@");
                            var smtp = "";
                            if(getSmtp[1] == "gmail.com")
                            {
                                smtp = "smtp.gmail.com";
                            }
                            else if(getSmtp[1] == "hotmail.com")
                            {
                                smtp = "smtp-mail.outlook.com";
                            }
                            else if(getSmtp[1] == "yahoo.com")
                            {
                                smtp = "smtp.mail.yahoo.com";
                            }
                            login = new NetworkCredential(email, pass);
                            client = new SmtpClient(smtp);
                            client.Port = 587;
                            client.EnableSsl = true;
                            client.Credentials = login;
                            msg = new MailMessage { From = new MailAddress(email) };
                            msg.To.Add(to);
                            msg.Subject = subject;
                            msg.Body = messagebody;
                            msg.BodyEncoding = Encoding.UTF8;
                            msg.IsBodyHtml = true;
                            msg.Priority = MailPriority.Normal;
                            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                            string userstate = "Sending...";
                            client.SendAsync(msg, userstate);
                            justmail = true;
                        }                        
                        channel.BasicAck(newTag, false);
                        previousTag = ea.DeliveryTag;
                        isGone = true;
                    }
                };
            }
        }
    }
}