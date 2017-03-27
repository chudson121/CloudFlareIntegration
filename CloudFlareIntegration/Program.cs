using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudFlareIntegration.CommandLine;
using CloudFlareIntegration.Logging.Interfaces;
using CloudFlareIntegration.Logging.Services;

namespace CloudFlareIntegration
{
    class Program
    {
        private static readonly ILogger Logger = LoggingService.GetLoggingService();
        private static CommandLineProcessor _clp;
        static void Main(string[] args)
        {
            Logger.Info("Program startup");
            ConfigureCommandLIne(args);

            var appConfiguration = new AppConfig()
            {
                BaseUrl = System.Configuration.ConfigurationManager.AppSettings["cloudflare:BaseUrl"],
                api_key = System.Configuration.ConfigurationManager.AppSettings["cloudflare:apikey"],
                email = System.Configuration.ConfigurationManager.AppSettings["cloudflare:email"],
            };

            var processor = new CloudFlareProcessor(appConfiguration);
            var zonelist = processor.GetZones();
            var dnslist = new List<DnsDto>();
            foreach (var zone in zonelist)
            {
                dnslist.AddRange(processor.GetDnsRecordsFromZone(zone.id));
            }


            WriteReport(dnslist);

            Console.ReadLine();
        }

        private static void WriteReport(IEnumerable<DnsDto> dnsDto)
        {
            var sb = new StringBuilder();
            sb.AppendLine("|Domain | Dns | Record | Type| Proxied| TTL|");
            sb.AppendLine("| ---- | ---- | ---- | ---- | ---- |");
            foreach (var d in dnsDto.OrderBy(x => x.zone_name).ThenBy(y => y.name))
            {
                sb.AppendLine($"|{d.zone_name}|{d.name}|{d.content}|{d.type}|{d.proxied}| {d.ttl}|");
            }

            System.IO.File.WriteAllText($"CloudFlareReport-{DateTime.Now:yyyy-MM-dd_hh-mm-ss-tt}.md", sb.ToString());
        }

        private static void ConfigureCommandLIne(string[] args)
        {
            Logger.Info("Configure Command Line Settings");
            _clp = new CommandLineProcessor(args, Logger);
            Logger.Info("Debug Mode:{0}", _clp.Options.IsDebug);

        }
    }
}
