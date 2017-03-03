# EECS4100 [![Build status](https://ci.appveyor.com/api/projects/status/kyad5qu6y1y8d6jk/branch/master?svg=true)](https://ci.appveyor.com/project/techwiz24/eecs4100/branch/master)
This repository contains projects and sample code for EECS4100 - Theory of Computation
for Spring 2017 at the University of Toledo

## Building
You ***will*** need an updated version of .net core. Unfortunately this isn't exactly simple right now as
the project is still in a lot of flux. In general, you need something that can build and run applications
that use `netcoreapp1.1`. On my linux machine, I'm using `1.0.1-rc4-004947`. I couldn't find this download
for windows, but `1.0.0-rc4-004771` seems to work as long as I rebuild.

You can find downloads [here](https://www.microsoft.com/net/download/core#/current).
You'll probably want to use something from the `current` series.

You can build all projects by using the cake script. There are convenient powershell and bash wrappers for this:

```powershell
# On Windows, use powershell:
./build.ps1 -t dist
```

```bash
# On Linux or macOS, use bash:
./build.sh -t dist
```

This task will restore packages, build all projects, run tests, and output executables in `./dist`. In dotnet core,
you can run these applications using `dotnet`. For example, to run the automata converter project:

```bash
dotnet ./dist/Automataconverter.dll Test\ Files/Simple.nfa
```

## License
All projects are licensed under the MIT license unless otherwise stated. See
`LICENSE` at the root of this repository for details.
