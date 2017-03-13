
[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]

namespace SDK
{
/// <summary>
/// Represents a source of entities bound to a CRM service. It tracks and manages changes made to the retrieved entities.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("KUMAVISON CODEGEN", "7.0.0000.3543")]
public partial class CrmContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
{

/// <summary>
/// Constructor.
/// </summary>
public CrmContext(Microsoft.Xrm.Sdk.IOrganizationService service) : base(service)
{
}

public System.Linq.IQueryable<PluginAssembly> PluginAssemblySet => this.CreateQuery<PluginAssembly>();

}
}
