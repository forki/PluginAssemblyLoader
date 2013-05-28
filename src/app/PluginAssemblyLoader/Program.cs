using System;
using System.IO;
using System.Reflection;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Mono.Options;

namespace PluginAssemblyLoader
{
    class Program
    {
        static int _verbosity;

        static void Main(string[] args)
        {
            var filePath = "";
            var connectionString = "";

            var showHelp = false;

            var p = new OptionSet
            {
                { "f|file=",    "The assembly which should be uploaded",    v => { if (v != null) filePath = v; } },
                { "c|connection=",  "The target for the upload",            v => { if (v != null) connectionString = v; } },
                { "v|verbose",  "Verbose log output",                       v => { if (v != null) ++_verbosity; } },
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

            if (showHelp)
            {
                ShowHelp(p);
                return;
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine();
                Console.WriteLine("Missing filePath");
                ShowHelp(p);
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.WriteLine();
                Console.WriteLine("Missing connectionstring");
                ShowHelp(p);
            }

            var assembly = Assembly.ReflectionOnlyLoadFrom(filePath);

            var con = new CrmConnection(connectionString);
            var service = new OrganizationService(con);

            var pluginAssemblyId = FindPluginAssembly(service, assembly.FullName);
            UpdatePluginAssembly(service, pluginAssemblyId, filePath);
        }

        private static void UpdatePluginAssembly(OrganizationService service, Guid pluginAssemblyId, string filePath)
        {
            var assemblyContentBytes = File.ReadAllBytes(filePath);
            var assemblyContent = Convert.ToBase64String(assemblyContentBytes);

            var pluginAssembly = new Entity()
            {
                LogicalName = "pluginassembly",
                Id = pluginAssemblyId
            };

            pluginAssembly.SetAttributeValue<string>("content", assemblyContent);

            service.Update(pluginAssembly);
        }

        private static Guid FindPluginAssembly(OrganizationService service, string assemblyFullName)
        {
            var query = new QueryExpression
            {
                EntityName = "pluginassembly",
                ColumnSet = null,
                Criteria = new FilterExpression()
            };
            query.Criteria.AddCondition("name",ConditionOperator.Equal,assemblyFullName);

            var request = new RetrieveMultipleRequest()
            {
                Query = query
            };

            var response = (RetrieveMultipleResponse)service.Execute(request);

            if (response.EntityCollection.TotalRecordCount == 1)
            {
                var id = response.EntityCollection[0].GetAttributeValue<Guid>("pluginassemblyid");

                return id;
            }

            throw new InvalidOperationException("Could not find plugin assembly");
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
