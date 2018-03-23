using System.Collections.Generic;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.Helper
{
    public class EntityReconcileBuilder
    {
        public static EntityReconcileMessage NewEntityReconcileMessage(List<string> keys = null, string type = "DefaultType")
        {
            return new EntityReconcileMessage()
            {
                Reconcile = new EntityReconcileMessage.EntityReconcile()
                {
                    Keys = GetKeys(keys),
                    Type = type
                }
            };
        }

        private static List<string> GetKeys(List<string> keys)
        {
            if (keys == null)
            {
                keys = new List<string>()
                {
                    "c011501f-eae6-4b65-9c51-fbfd02c79ac0",
                    "1eb3e364-caa7-432a-9d1e-e2f9dd5c6155",
                    "4b95393f-fc1e-4f8f-9184-be6f07f1a48f"
                };
            }
            return keys;
        }
    }
}
