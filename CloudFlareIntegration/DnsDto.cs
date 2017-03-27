using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFlareIntegration
{

    public class RootDnsObject
    {
        public DnsDto[] result { get; set; }
        public DnsResult_Info result_info { get; set; }
        public bool success { get; set; }
        public object[] errors { get; set; }
        public object[] messages { get; set; }
    }

    public class DnsResult_Info
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total_pages { get; set; }
        public int count { get; set; }
        public int total_count { get; set; }
    }

    public class DnsDto
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public bool proxiable { get; set; }
        public bool proxied { get; set; }
        public int ttl { get; set; }
        public bool locked { get; set; }
        public string zone_id { get; set; }
        public string zone_name { get; set; }
        public DateTime modified_on { get; set; }
        public DateTime created_on { get; set; }
        public DnsMeta meta { get; set; }
    }

    public class DnsMeta
    {
        public bool auto_added { get; set; }
    }

}
