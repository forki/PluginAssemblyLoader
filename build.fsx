// include Fake libs
#I @"tools\FAKE\tools\"
#r @"tools\FAKE\tools\FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile

//Project config
let projectName = "Dynamics CRM 2011 Plugin Assembly Loader"
let projectSummary = "Updates Pluginassemblies in Dynamics CRM"
let projectDescription = ""
let authors = ["Christoph Keller"]

// Directories
let buildDir  = @".\build\"
let testDir   = @".\test\"
let deployDir = @".\Publish\"
let nugetDir = @".\nuget\" 
let packagesDir = @".\packages\"

// version info
let mutable version = "1.5." + buildVersion 
let mutable nugetVersion = version

let gitbranch = Git.Information.getBranchName "."
let sha = Git.Information.getCurrentHash() 

// Targets
Target "Clean" (fun _ -> 

    CleanDirs [buildDir; testDir; deployDir; nugetDir]
    RestorePackages()
)

Target "BuildVersions" (fun _ ->

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

//Target "Publish" (fun _ ->     
//   
//   
//)

// Dependencies
"Clean"
  ==> "BuildVersions"
  =?> ("AssemblyInfo", not isLocalBuild ) 
  ==> "BuildApp"
  //==> "BuildTest"
  //==> "NUnitTest"
 // ==> "Publish"
 
// start build
RunTargetOrDefault "BuildApp"