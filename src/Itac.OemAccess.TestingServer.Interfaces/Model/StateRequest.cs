using System;

namespace Itac.OemAccess.TestingServer.Model
{
    public interface IMessageWithId
    {
        Guid MessageId { get; }
    }
    public class StateRequest : IMessageWithId
    {
        public override string ToString()
        {
            return "State Requests";
        }

        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public string Entity { get; set; }
        public string StateName { get; set; }
    }
}
