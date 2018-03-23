using System.Collections.Generic;

namespace Itac.OemAccess.TestingServer.Model
{
    public class AreaMovement
    {
        //
        //String format : 
        // NameSpace:Id
        // AccessControl.Area:Area-3



        //User Entity
        public string Entity { get; set; }
        //Area Entity
        public List<string> From { get; set; }
        //Area Entity
        public List<string> To { get; set; }

    }
}
