using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireScheduleJobDemo
{
    public class EmailService : IEmailService //Dependency injection için yapıldı
    {
        public void SentMail()
        {
            Console.WriteLine($"recurring");
        }
    }
}
