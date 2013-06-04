using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Mono.Options;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace PluginAssemblyLoader
{
    class Program
    {
        static bool _verboseLogging;
        private static Logger _logger;

        static void Main(string[] args)
        {
            ConfigureLogging(_verboseLogging);

            _logger = LogManager.GetCurrentClassLogger();

            var filePath = "";
            var connectionString = "";

            var showHelp = false;

            var p = new OptionSet
            {
                { "f|file=",    "The assembly which should be uploaded",    v => { if (v != null) filePath = v; } },
                { "c|connection=",  "The target for the upload",            v => { if (v != null) connectionString = v; } },
                { "v|verbose",  "Verbose log output",                       v => { if (v != null) _verboseLogging = true; } },
                { "h|help",     "show this message and exit",               v => showHelp = v != null },
            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                var name = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
                Console.Write("{0}: ", name);
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", name);
                return;
            }

            if (_verboseLogging)
            {
                ConfigureLogging(_verboseLogging);
            }

            if (showHelp)
            {
                ShowHelp(p);
                return;
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.Log(LogLevel.Error, () => "Missing filePath.");

                ShowHelp(p);
                Environment.Exit(1);
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.Log(LogLevel.Error, () => "Missing connectionstring.");

                ShowHelp(p);
                Environment.Exit(2);
            }

            var assembly = Assembly.ReflectionOnlyLoadFrom(filePath); // TODO catch NotFound
            _logger.Log(LogLevel.Info, () => "Loaded assembly.");

            var con = CrmConnection.Parse(connectionString);
            var service = new OrganizationService(con);

            var pluginAssemblyId = FindPluginAssembly(service, assembly.GetName().Name);

            if (pluginAssemblyId != Guid.Empty)
            {
                UpdatePluginAssembly(service, pluginAssemblyId, filePath);
            }
            else
            {
                _logger.Log(LogLevel.Error, () => "Sorry, I am unable to find the appropriate assembly.");
                Environment.Exit(3);
            }
        }

        private static void ConfigureLogging(bool enableVerboseLogging)
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            consoleTarget.Layout = "> ${message}";

            var rule1 = new LoggingRule("*", LogLevel.Info, consoleTarget);

            if (enableVerboseLogging)
            {
                rule1.EnableLoggingForLevel(LogLevel.Trace);
                rule1.EnableLoggingForLevel(LogLevel.Debug);
            }

            config.LoggingRules.Add(rule1);

            LogManager.Configuration = config;
        }

        private static void UpdatePluginAssembly(IOrganizationService service, Guid pluginAssemblyId, string filePath)
        {
            var assemblyContentBytes = File.ReadAllBytes(filePath);
            var assemblyContent = Convert.ToBase64String(assemblyContentBytes);

            _logger.Log(LogLevel.Trace, () => "Created assembly content");

            var pluginAssembly = new Entity
            {
                LogicalName = "pluginassembly",
                Id = pluginAssemblyId
            };

            pluginAssembly.SetAttributeValue<string>("content", assemblyContent);

            service.Update(pluginAssembly);

            _logger.Log(LogLevel.Info, () => "Update was successful");
        }

        private static Guid FindPluginAssembly(OrganizationService service, string assemblyName)
        {
            var query = new QueryExpression
            {
                EntityName = "pluginassembly",
                ColumnSet = null,
                Criteria = new FilterExpression()
            };
            query.Criteria.AddCondition("name", ConditionOperator.Equal, assemblyName);

            var request = new RetrieveMultipleRequest
            {
                Query = query
            };

            var response = (RetrieveMultipleResponse)service.Execute(request);

            if (response.EntityCollection.Entities.Count == 1)
            {
                var id = response.EntityCollection[0].GetAttributeValue<Guid>("pluginassemblyid");
                _logger.Log(LogLevel.Debug, () => string.Format("Found id {0} for assembly", id));

                return id;
            }

            return Guid.Empty;
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: {0} [OPTIONS]", Path.GetFileName(Assembly.GetExecutingAssembly().Location));
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
