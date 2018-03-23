using System;
using System.Collections.Generic;

namespace Itac.OemAccess.TestingServer.Model
{
    public abstract class EntityMessage : IMessageWithId
    {
        public Guid MessageId { get; } = Guid.NewGuid();
        public string Body { get; }

        public EntityMessage(string body)
        {
            Body = body;
        }

        public override string ToString()
        {
            return "Change Request";
        }
    }

    public class EntityReconcileMessage
    {
        public class EntityReconcile
        {
            public string Type { get; set; }
            public List<string> Keys { get; set; }
        }

        public EntityReconcile Reconcile { get; set; }
    }

    public abstract class Entity 
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class TimeTableEntity : Entity
    {
        public class TimeTableSpan
        {
            public TimeSpan Start { get; set; }
            public TimeSpan End { get; set; }
        }

        public Dictionary<string, List<TimeTableSpan>> Transitions { get; set; }
    }

    

    public class UserEntity : Entity
    {
        public class UserIdentifier
        {
            public string Type { get; set; }
            public string Id { get; set; }
            public string Description { get; set; }
            public string Data { get; set; }
        }

        public class UserVerifier
        {
            public string Type { get; set; }
            public string Id { get; set; }
            public string Description { get; set; }
            public string Data { get; set; }
            public bool Duress { get; set; }
        }

        public class UserPermission
        {
            public string Type { get; set; }
            public string Data { get; set; }
        }
        
        public List<string> Attributes { get; set; }
        public List<UserIdentifier> Identifiers { get; set; }
        public List<UserVerifier> Verifiers { get; set; }
        public Dictionary<string, List<UserPermission>> Permissions { get; set; }
    }
}
