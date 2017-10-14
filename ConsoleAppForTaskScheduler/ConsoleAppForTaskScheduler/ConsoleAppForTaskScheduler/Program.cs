using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using SeriesClientConsoleApp.ServiceReference1;



namespace ConsoleAppForTaskScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Program CallServices = new Program();

            //CallServices.SendMail();
           // CallServices.WriteLogFile();
            try 
            {
                CallServices.UpdateLAbs();
 
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            //Console.ReadKey();
            Environment.Exit(0);
        }

        public void SendMail(string Message) 
        {
            var fromAddress = new MailAddress("seriescentralnode@gmail.com", "SERIES Central Node");
            var toAddress = new MailAddress("ioannid6@gmail.com", "Ioannis Ioannidis");
           // var toCCAddress = new MailAddress("ioannid@ceid.upatras.gr", "Ioannis Ioannidis");
            const string fromPassword = "adm1n!str@t0r";
            const string subject = "SERIES scheduled task";
            
            string body;

            body = Message;
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body})
            {
                message.CC.Add(new MailAddress("ioannid@ceid.upatras.gr", "Ioannis Ioannidis"));
                
                smtp.Send(message);
            }

        }

        public void WriteLogFile(string message) 
        {
            string DatetimeFilename = string.Format("SERIES_LOG_FILE-{0:yyyy-MM-dd}.txt", DateTime.Now);
            string path = "C:\\serieslog\\";
            string FullDateTime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}",DateTime.Now);
            //using (StreamWriter sw = File.AppendText(path + DatetimeFilename))

            using (StreamWriter sw = (File.Exists(path + DatetimeFilename)) ? File.AppendText(path + DatetimeFilename) : File.CreateText(path + DatetimeFilename))                 
            {
                sw.WriteLine(message + FullDateTime);
                
            }

            
        }

        public void UpdateLAbs()
        {
            using (MySqlConnection conn2 = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;"))
            {
                MySqlCommand selectLabs = new MySqlCommand("Select * from laboratory", conn2);
                string message = "";

                MySqlDataAdapter labsAdapter = new MySqlDataAdapter(selectLabs);
                System.Data.DataSet labs = new System.Data.DataSet();
                labsAdapter.Fill(labs);
                if (labs.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow Labrow in labs.Tables[0].Rows)
                    {
                        try
                        {

                            
                            SeriesClientConsoleApp.UOXFGetData callUoxfServices = new SeriesClientConsoleApp.UOXFGetData(Labrow["Name"].ToString());
                            callUoxfServices.Initialization(Labrow["Name"].ToString());
                            message += Labrow["Name"].ToString() + " ok";
                            WriteLogFile(Labrow["Name"].ToString() + " ok");
                            
                        }
                        catch(Exception e)
                        {
                            message += Labrow["Name"].ToString() + "not ok " + e.ToString();
                            WriteLogFile(Labrow["Name"].ToString() + "not ok");
                            SendMail(message);
                        }

                    }
                    

                }
            }

        }
    }

   
}
