##PluginAssemblyLoader
Command line tool for updating plugin or workflow assemblies in Dynamics CRM

### What it does

- Updates assemblies which are registered in Dynamics CRM 

### What it does _not_

- Register new plugin steps, images, ...
- Configuration of existing plugin steps, images, ...

### How to use
The application has only two parameters:



    PluginAssemblyLoader -f <path_to_assembly> -c "CRM ConnectionString"

If you would like to update a plugin assembly on an On-Premise instance with  integrated authentication

	PluginAssemblyLoader -f "C:\MyPlugin.dll" -c "Url=http://crmserver/org;"

For CRM Online (North America) it would be

	PluginAssemblyLoader -f "C:\MyPlugin.dll" - c "Url=https://contoso.crm.dynamics.com; Username=usr@contoso.onmicrosoft.com; Password=password;"

### How to build

If you want to restore the project after you cloned it please run:

	00_boot.bat
This will restore all necessary packages for building the project.
After that you could run a new build with:

	02_build.bat