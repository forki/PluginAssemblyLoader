namespace SDK
{
[System.Runtime.Serialization.DataContractAttribute()]
[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("pluginassembly")]
[System.CodeDom.Compiler.GeneratedCodeAttribute("KUMAVISON CODEGEN", "7.0.0000.3543")]
public partial class PluginAssembly: Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{

    /// <summary>
    /// Default Constructor.
    /// </summary>
public PluginAssembly() :base(EntityLogicalName)
{
}

public const string EntityLogicalName = "pluginassembly";

public const int EntityTypeCode = 4605;

public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

private void OnPropertyChanged(string propertyName)
{
    if ((this.PropertyChanged != null))
   {
       this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
   }
}

private void OnPropertyChanging(string propertyName)
{
    if ((this.PropertyChanging != null))
    {
       this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
    }
}


[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("pluginassemblyid")]
public override System.Guid Id
{
  get
  {
    return base.Id;
  }
  set
  {
   this.PluginAssemblyId = value;
  }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyyominame")]
public string CreatedOnBehalfByYomiName
{
    get
    {
        return this.GetAttributeValue<string>("createdonbehalfbyyominame");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("introducedversion")]
public string IntroducedVersion
{
    get
    {
        return this.GetAttributeValue<string>("introducedversion");
    }
    set
    {
        this.OnPropertyChanging("IntroducedVersion");
        this.SetAttributeValue("introducedversion", value);
        this.OnPropertyChanged("IntroducedVersion");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdbyname")]
public string CreatedByName
{
    get
    {
        return this.GetAttributeValue<string>("createdbyname");
    }
}


[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("description")]
public string Description
{
    get
    {
        return this.GetAttributeValue<string>("description");
    }
    set
    {
        this.OnPropertyChanging("Description");
        this.SetAttributeValue("description", value);
        this.OnPropertyChanged("Description");
    }
}



[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
    }
}


[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("name")]
public string Name
{
    get
    {
        return this.GetAttributeValue<string>("name");
    }
    set
    {
        this.OnPropertyChanging("Name");
        this.SetAttributeValue("name", value);
        this.OnPropertyChanged("Name");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("content")]
public string Content
{
    get
    {
        return this.GetAttributeValue<string>("content");
    }
    set
    {
        this.OnPropertyChanging("Content");
        this.SetAttributeValue("content", value);
        this.OnPropertyChanged("Content");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("culture")]
public string Culture
{
    get
    {
        return this.GetAttributeValue<string>("culture");
    }
    set
    {
        this.OnPropertyChanging("Culture");
        this.SetAttributeValue("culture", value);
        this.OnPropertyChanged("Culture");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("major")]
public System.Nullable<int> Major
{
    get
    {
        return this.GetAttributeValue<System.Nullable<int>>("major");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
public Microsoft.Xrm.Sdk.EntityReference OrganizationId
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("organizationid");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("customizationlevel")]
public System.Nullable<int> CustomizationLevel
{
    get
    {
        return this.GetAttributeValue<System.Nullable<int>>("customizationlevel");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("minor")]
public System.Nullable<int> Minor
{
    get
    {
        return this.GetAttributeValue<System.Nullable<int>>("minor");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("publickeytoken")]
public string PublicKeyToken
{
    get
    {
        return this.GetAttributeValue<string>("publickeytoken");
    }
    set
    {
        this.OnPropertyChanging("PublicKeyToken");
        this.SetAttributeValue("publickeytoken", value);
        this.OnPropertyChanged("PublicKeyToken");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("version")]
public string Version
{
    get
    {
        return this.GetAttributeValue<string>("version");
    }
    set
    {
        this.OnPropertyChanging("Version");
        this.SetAttributeValue("version", value);
        this.OnPropertyChanged("Version");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sourcetype")]
public Microsoft.Xrm.Sdk.OptionSetValue SourceType
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("sourcetype");
    }
    set
    {
        this.OnPropertyChanging("SourceType");
        this.SetAttributeValue("sourcetype", value);
        this.OnPropertyChanged("SourceType");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("pluginassemblyidunique")]
public System.Nullable<System.Guid> PluginAssemblyIdUnique
{
    get
    {
        return this.GetAttributeValue<System.Nullable<System.Guid>>("pluginassemblyidunique");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedbyname")]
public string ModifiedByName
{
    get
    {
        return this.GetAttributeValue<string>("modifiedbyname");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
public System.Nullable<int> VersionNumber
{
    get
    {
        return this.GetAttributeValue<System.Nullable<int>>("versionnumber");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
public Microsoft.Xrm.Sdk.EntityReference CreatedBy
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ismanaged")]
public System.Nullable<bool> IsManaged
{
    get
    {
        return this.GetAttributeValue<System.Nullable<bool>>("ismanaged");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("path")]
public string Path
{
    get
    {
        return this.GetAttributeValue<string>("path");
    }
    set
    {
        this.OnPropertyChanging("Path");
        this.SetAttributeValue("path", value);
        this.OnPropertyChanged("Path");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overwritetime")]
public System.Nullable<System.DateTime> OverwriteTime
{
    get
    {
        return this.GetAttributeValue<System.Nullable<System.DateTime>>("overwritetime");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("solutionid")]
public System.Nullable<System.Guid> SolutionId
{
    get
    {
        return this.GetAttributeValue<System.Nullable<System.Guid>>("solutionid");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
public System.Nullable<System.DateTime> ModifiedOn
{
    get
    {
        return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("componentstate")]
public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("componentstate");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("isolationmode")]
public Microsoft.Xrm.Sdk.OptionSetValue IsolationMode
{
    get
    {
        return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("isolationmode");
    }
    set
    {
        this.OnPropertyChanging("IsolationMode");
        this.SetAttributeValue("isolationmode", value);
        this.OnPropertyChanged("IsolationMode");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyyominame")]
public string ModifiedOnBehalfByYomiName
{
    get
    {
        return this.GetAttributeValue<string>("modifiedonbehalfbyyominame");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("pluginassemblyid")]
public System.Nullable<System.Guid> PluginAssemblyId
{
    get
    {
        return this.GetAttributeValue<System.Nullable<System.Guid>>("pluginassemblyid");
    }
    set
    {
        this.OnPropertyChanging("PluginAssemblyId");
       this.SetAttributeValue("pluginassemblyid", value);
       base.Id = value ?? System.Guid.Empty;
        this.OnPropertyChanged("PluginAssemblyId");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
public System.Nullable<System.DateTime> CreatedOn
{
    get
    {
        return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyname")]
public string CreatedOnBehalfByName
{
    get
    {
        return this.GetAttributeValue<string>("createdonbehalfbyname");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyname")]
public string ModifiedOnBehalfByName
{
    get
    {
        return this.GetAttributeValue<string>("modifiedonbehalfbyname");
    }
}

[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sourcehash")]
public string SourceHash
{
    get
    {
        return this.GetAttributeValue<string>("sourcehash");
    }
    set
    {
        this.OnPropertyChanging("SourceHash");
        this.SetAttributeValue("sourcehash", value);
        this.OnPropertyChanged("SourceHash");
    }
}


}


[System.Runtime.Serialization.DataContractAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("KUMAVISON CODEGEN", "7.0.0000.3543")]
public enum pluginassembly_sourcetype
{

[System.Runtime.Serialization.EnumMemberAttribute()]
Datenbank = 0,
[System.Runtime.Serialization.EnumMemberAttribute()]
Datenträger = 1,
[System.Runtime.Serialization.EnumMemberAttribute()]
Normal = 2,
}

[System.Runtime.Serialization.DataContractAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("KUMAVISON CODEGEN", "7.0.0000.3543")]
public enum componentstate
{

[System.Runtime.Serialization.EnumMemberAttribute()]
Veröffentlicht = 0,
[System.Runtime.Serialization.EnumMemberAttribute()]
Unveröffentlicht = 1,
[System.Runtime.Serialization.EnumMemberAttribute()]
Gelöscht = 2,
[System.Runtime.Serialization.EnumMemberAttribute()]
Nicht_veröffentlicht_gelöscht = 3,
}

[System.Runtime.Serialization.DataContractAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("KUMAVISON CODEGEN", "7.0.0000.3543")]
public enum pluginassembly_isolationmode
{

[System.Runtime.Serialization.EnumMemberAttribute()]
Keine = 1,
[System.Runtime.Serialization.EnumMemberAttribute()]
Sandkasten = 2,
}


}
