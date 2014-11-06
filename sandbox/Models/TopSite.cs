using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace sandbox.Models
{
    public class TopSite
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string CacheControl { get; set; }
        public string Expires { get; set; }
        public string Date { get; set; }
        public string Vary { get; set; }
        public string Etag { get; set; }

    }
}