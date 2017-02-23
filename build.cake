#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

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
        Configuration = configuration
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

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);