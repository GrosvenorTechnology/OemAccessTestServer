using System;

namespace Itac.OemAccess.TestingServer.Model
{
    public enum StateNotificationType
    {
        StateChange,
        StateRequest,
        PeriodicUpdate,
        DeltaChangeUpdate
    }

    public class StateNotification
    {
        public override string ToString()
        {
            return $@"
        [Entity]       : {Entity}
        [StateName]    : {StateName}
        [StateType]    : {StateNotificationType}
        [StateValue]   : {StateValue}";
        }

        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid? PreviousMessageId { get; set; }
        public string Entity { get; set; }
        public string StateName { get; set; }
        public string StateValue { get; set; }
        public StateNotificationType StateNotificationType { get; set; }
        public bool DiagState { get; set; }
        public DateTime LastChanged { get; set; }
    }
}
