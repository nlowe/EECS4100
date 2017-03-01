#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Setup(ctx => 
{
    Information("Using dotnet core version: ");
    StartProcess("dotnet", new ProcessSettings().WithArguments(a => 
        a.Append("--version")
    ));
});

Task("Clean-Dist")
    .Does(() => 
{
    if(DirectoryExists("./dist"))
    {
        DeleteDirectory("./dist", true);
    }
});

Task("Clean")
    .Does(() =>
{
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore("./EECS4100.sln");
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetCoreBuild("./EECS4100.sln", new DotNetCoreBuildSettings {
        Configuration = configuration,
        Framework = "netcoreapp1.1"
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    foreach(var assembly in GetFiles("./test/**/*.csproj"))
    {
        DotNetCoreTest(assembly.FullPath, new DotNetCoreTestSettings {
            Configuration = configuration
        });
    }
});

Task("Dist")
    .IsDependentOn("Clean-Dist")
    .IsDependentOn("Test")
    .Does(() => 
{
    DotNetCorePublish("./src/AutomataConverter/AutomataConverter.csproj", new DotNetCorePublishSettings {
        Framework = "netcoreapp1.1",
        Configuration = configuration,
        OutputDirectory = "./dist"
    });
});

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);