// include Fake libs
#r @"packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile

//Project config
let project                 = "msdyncrm.PluginAssemblyLoader"
let projectName             = "Dynamics CRM Plugin Assembly Loader"
let projectSummary          = "Updates Plugin Assemblies in Dynamics CRM"
let projectDescription      = "Updates Plugin Assemblies in Dynamics CRM"
let authors                 = ["Christoph Keller"]
let homepage                = "http://msdyncrm-contrib.github.io/PluginAssemblyLoader"

// Directories
let buildDir                = @".\build"
let testDir                 = @".\test"
let deployDir               = @".\Publish"
let nugetDir                = @".\nuget" 
let packagesDir             = @".\packages"
let installerDir            = @".\msi"

let productName             = @"PluginAssemblyLoader"

// version info
let mutable version         = "2.0"
let mutable build           = buildVersion 
let mutable nugetVersion    = ""
let mutable asmVersion      = ""
let mutable asmInfoVersion  = ""

let gitbranch               = Git.Information.getBranchName "."
let sha                     = Git.Information.getCurrentHash() 

// Targets
Target "Clean" (fun _ -> 

    CleanDirs [buildDir; testDir; deployDir; nugetDir]
    RestorePackages()
)

Target "BuildVersions" (fun _ ->

    asmVersion      <- version + "." + build
    asmInfoVersion  <- asmVersion + " - " + gitbranch + " - " + sha

    let nugetBuildNumber = if not isLocalBuild then build else "0"

    nugetVersion    <- version + "." + nugetBuildNumber

    match System.String.Equals(gitbranch, "master", System.StringComparison.CurrentCultureIgnoreCase) with
        | true -> ()
        | false -> (nugetVersion <- nugetVersion + "-" + "beta")
    
    SetBuildNumber nugetVersion   // Publish version to TeamCity
)

Target "AssemblyInfo" (fun _ ->

    ReplaceAssemblyInfoVersions (fun p ->
        {p with
            AssemblyVersion = asmVersion
            AssemblyInformationalVersion = asmInfoVersion
            OutputFileName = @".\src\app\PluginAssemblyLoader\Properties\AssemblyInfo.cs"
            })    
)

Target "BuildApp" (fun _ ->
    !! @"src\app\**\*.csproj"      
        |> MSBuildRelease buildDir "Build"
        |> Log "Build-Output: "
)


Target "CreateNuGet" (fun _ -> 
   
    let nugetToolsDir = nugetDir @@ "tools"

    CreateDir nugetToolsDir

    !! (buildDir @@ @"*.exe") 
      ++ (buildDir @@ @"*.dll")   
        |> CopyTo nugetToolsDir

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = project
            Description = projectDescription
            Version = nugetVersion                           
            OutputPath = nugetDir
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "PluginAssemblyLoader.nuspec"
)

Target "BuildZip" (fun _ ->     

    let deployZip = deployDir @@ sprintf "%s-%s.zip" project asmVersion

    !! (buildDir @@ @"*.exe") 
      ++ (buildDir @@ @"*.dll")   
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