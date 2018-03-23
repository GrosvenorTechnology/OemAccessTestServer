using System;

namespace Itac.OemAccess.TestingServer.Model
{
    public enum CommandOutcomeCategory
    {
        Success,
        FailedOnPermissions,
        FailedBecauseOfError,
        UnknownCommand,
        TargetNotFound,
        CommandArgumentError,
        Accepted,
        InProgress
    }


    public class CommandResponse
    {
        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid? PreviousMessageId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public CommandOutcomeCategory Result { get; set; }
        public string Explaination { get; set; }

        public override string ToString()
        {
            return $@"
        [Message ID]      : {MessageId}
        [Prev Message Id] : {PreviousMessageId}
        [Result]          : {Result}
        [Error Code]      : {Result}";
        }
    }
}
