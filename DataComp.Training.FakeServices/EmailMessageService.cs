using DataComp.Training.IServices;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmailMessageService> logger;
        private readonly EmailMessageServiceOptions options;

        public EmailMessageService(ILogger<EmailMessageService> logger, EmailMessageServiceOptions options)
        {
            this.logger = logger;
            this.options = options;
        }

        public void Send(string message)
        {
            logger.LogInformation($"Sending {message} on {options.Smtp}:{options.Port}");

            logger.LogError("Smtp error");

            throw new ApplicationException("Smtp error");
        }
    }
}
