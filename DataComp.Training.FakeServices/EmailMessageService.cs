using DataComp.Training.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataComp.Training.FakeServices
{
    public class EmailMessageServiceOptions
    {
        public string Smtp { get; set; }
        public int Port { get; set; }
    }


    public class EmailMessageService : IMessageService
    {
        private readonly EmailMessageServiceOptions options;

        public EmailMessageService(EmailMessageServiceOptions options)
        {
            this.options = options;
        }

        public void Send(string message)
        {
            Console.WriteLine($"Sending {message} on {options.Smtp}:{options.Port}");
        }
    }
}
