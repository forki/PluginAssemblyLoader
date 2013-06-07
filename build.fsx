// include Fake libs
#I @"tools\FAKE\tools\"
#r @"tools\FAKE\tools\FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile

//Project config
let project                 = "PluginAssemblyLoader"
let projectName             = "Dynamics CRM 2011 Plugin Assembly Loader"
let projectSummary          = "Updates Pluginassemblies in Dynamics CRM"
let projectDescription      = "Updates Pluginassemblies in Dynamics CRM"
let authors                 = ["Christoph Keller"]
let homepage                = "http://msdyncrm-contrib.github.io/PluginAssemblyLoader"

// Directories
let buildDir                = @".\build"
let testDir                 = @".\test"
let deployDir               = @".\Publish"
let nugetDir                = @".\nuget" 
let packagesDir             = @".\packages"

// version info
let mutable version         = "1.0." + buildVersion 
let mutable nugetVersion    = version

let gitbranch               = Git.Information.getBranchName "."
let sha                     = Git.Information.getCurrentHash() 

// Targets
Target "Clean" (fun _ -> 

    CleanDirs [buildDir; testDir; deployDir; nugetDir]
    RestorePackages()
)

Target "BuildVersions" (fun _ ->

    match System.String.Equals(gitbranch, "develop", System.StringComparison.CurrentCultureIgnoreCase) with
        | true -> (nugetVersion <- version + "-" + "beta")
        | false -> ()

    SetBuildNumber version
)

Target "AssemblyInfo" (fun _ ->

    ReplaceAssemblyInfoVersions (fun p ->
        {p with
            AssemblyVersion = version
            AssemblyInformationalVersion = version + " - " + gitbranch + " - " + sha
            OutputFileName = @".\src\app\PluginAssemblyLoader\Properties\AssemblyInfo.cs"
            })    
)

Target "BuildApp" (fun _ ->
    !+ @"src\app\**\*.csproj"      
        |> Scan
        |> MSBuildRelease buildDir "Build"
        |> Log "Build-Output: "
)

Target "BuildTest" (fun _ ->
    !! @"src\test\**\*.csproj"
      |> MSBuildDebug testDir "Build"
      |> Log "TestBuild-Output: "
)

Target "NUnitTest" (fun _ ->  

    !! (testDir + @"*.Tests.dll")
        |> NUnit (fun p -> 
            {p with 
                ToolPath = @".\tools\NUnit.Runners\tools\"; 
                Framework = "net-4.0";
                DisableShadowCopy = true; 
                OutputFile = testDir + @"TestResults.xml"})
)

Target "CreateNuGet" (fun _ -> 
   
    let nugetToolsDir = nugetDir @@ "tools"

    CreateDir nugetToolsDir

    !+ (buildDir @@ @"*.exe") 
      ++ (buildDir @@ @"*.dll")   
        |> Scan
        |> CopyTo nugetToolsDir

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = project
            Description = projectDescription
            //Version = nugetVersion                           
            OutputPath = nugetDir
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "PluginAssemblyLoader.nuspec"
)

Target "BuildZip" (fun _ ->     

    let deployZip = deployDir @@ sprintf "%s-%s.zip" project buildVersion

    !+ (buildDir @@ @"*.exe") 
      ++ (buildDir @@ @"*.dll")   
       |> Scan
       |> Zip buildDir deployZip
)

// Dependencies
"Clean"
  ==> "BuildVersions"
  =?> ("AssemblyInfo", not isLocalBuild ) 
  ==> "BuildApp"
  ==> "CreateNuGet"
  ==> "BuildZip"
 
// start build
RunTargetOrDefault "BuildApp"