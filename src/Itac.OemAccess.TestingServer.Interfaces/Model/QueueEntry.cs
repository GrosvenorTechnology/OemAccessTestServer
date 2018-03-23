using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Itac.OemAccess.TestingServer.Model
{
    public class QueueEntry<T> : IMessageWithId
    {
        public Guid MessageId { get; } = Guid.NewGuid();
        public T Body { get; }

        public QueueEntry(T body)
        {
            Body = body;
        }
    }
}
