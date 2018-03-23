using System;
using System.Collections.Generic;

namespace Itac.OemAccess.TestingServer.Model
{
    public class Command : IMessageWithId
    {
        public override string ToString()
        {
            return "Command Requests";
        }

        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid? PreviousMessageId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public TimeSpan Ttl { get; set; }
        public string Entity { get; set; }
        public string CommandName { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public string RequestingEntity { get; set; }
        public string PermissionedEntity { get; set; }
    }
}
