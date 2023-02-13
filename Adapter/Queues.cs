using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class RabbitMqQueues
    {
        public const string ProcessNewLead = "ProcessNewLead";
        public const string NotificationQueue = "Notifications";
        public const string LogQueue = "Logs";
    }
}
