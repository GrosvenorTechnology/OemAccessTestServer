using System;
using System.Collections.Generic;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.Helper
{
    public class TimeTableBuilder
    {
        public static TimeTableEntity NewTimeTableEntity(
            string description = "TimeTableDescription",
            string id = "TimeTableId",
            string type = "TimeTableType",
            Dictionary<string,List<TimeTableEntity.TimeTableSpan>> transitions = null)
        {
            return new TimeTableEntity()
            {
                Description = description,
                Id = id,
                Type = type,
                Transitions = GetTransitions(transitions)
            };
        }

        private static Dictionary<string, List<TimeTableEntity.TimeTableSpan>> GetTransitions(Dictionary<string, List<TimeTableEntity.TimeTableSpan>> transitions)
        {
            if (transitions == null)
            {
                transitions = new Dictionary<string, List<TimeTableEntity.TimeTableSpan>>();
                var list = new List<TimeTableEntity.TimeTableSpan>()
                {
                    new TimeTableEntity.TimeTableSpan()
                    {
                        Start = TimeSpan.FromHours(8),
                        End = TimeSpan.FromHours(18)
                    }
                };
                transitions.Add("Transition",list);
            }
            return transitions;
        }
    }
}
