using System;
using System.Collections.Generic;
using System.Text;

namespace Itac.OemAccess.TestingServer.Model
{
    public enum EventType
    {
        Plain,
        CommandResponse,
        StateChange
    }

    public class Event
    {
        public override string ToString()
        {
            return $@"
        [SourceTime] : {TimeStamp:O}
        [Entity]     : {Entity}
        [EventName]  : {EventName}
        [EventType]  : {EventType}
        [Content]    : {Print(Contents)}";
        }

        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid? PreviousMessageId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Entity { get; set; }
        public string EventName { get; set; }
        public bool DiagEvent { get; set; }
        public EventType EventType { get; set; }
        public Dictionary<string,string> Contents { get; set; } = new Dictionary<string, string>();

        private static string Print(Dictionary<string, string> source)
        {
            var keyValueSeparator = ":";
            var sequenceSeparator = ", ";

            if (source == null)
                throw new ArgumentException("Parameter source can not be null.");

            if (source.Count == 0)
            {
                return "";
            }

            var str = new StringBuilder();
            foreach (var keyvaluepair in source)
            {
                str.Append($"{keyvaluepair.Key}{keyValueSeparator}{keyvaluepair.Value}{sequenceSeparator}");
            }
            var retval = str.ToString();
            return retval.Substring(0, retval.Length - sequenceSeparator.Length); //remove last  seq_separator
        }
    }
}
