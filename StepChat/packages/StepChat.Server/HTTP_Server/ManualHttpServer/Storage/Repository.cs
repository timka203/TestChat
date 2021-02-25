using ManualHttpServer.Resources.Api;
using System.Collections.Generic;

namespace ManualHttpServer.Storage
{
    public static class Repository
    {
        public static List<ApplicationAccount> ApplicationAccounts { get; set; }
            = new List<ApplicationAccount>();
        public static Dictionary<string, ApplicationAccount> ActiveSessions { get; set; }
            = new Dictionary<string, ApplicationAccount>();
    }
}
