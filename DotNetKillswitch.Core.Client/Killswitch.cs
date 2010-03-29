using System;
using System.Net;
using System.Net.Mime;
using System.Web;

namespace DotNetKillswitch.Core.Client
{
    public class Killswitch
    {
        private static Killswitch _instance;
        private static readonly object Padlock = new object();
        private Uri _server;
        private Guid? _clientId;

        private Killswitch()
        {}

        private static Killswitch Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new Killswitch());
                }
            }
        }

        public static Killswitch Set()
        {
            return Instance;
        }

        public Killswitch WithServer(Uri uri)
        {
            Instance._server = uri;
            return Instance;
        }

        public Killswitch WithClientId(Guid id)
        {
            Instance._clientId = id;
            return Instance;
        }

        public static string Css()
        {
            return Instance._server != null && Instance._clientId.HasValue
                       ? string.Format( Constants.CssLink, Instance._server.AbsoluteUri, Instance._clientId)
                       : string.Empty;
        }
    }
}
