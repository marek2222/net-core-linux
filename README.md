# net-core-linux

Project was created by https://www.blinkingcaret.com/2018/03/20/net-core-linux/ on VSCode

## Creating the projects and solution file

By this time you should have installed .Net Core and Visual Studio Code. 

### $ dotnet new mvc

If you run that as it is, a new MVC project will be created in the current folder, using the name of the current folder as the default namespace and project name.

First thing we need to do is to create a project folder named Reminders and then inside it do:

### $ dotnet new mvc --name Reminders.Web
### $ dotnet new console --name Reminders.Cli
### $ dotnet new classlib --name Reminders.Common
### $ dotnet new xunit --name Reminders.Tests

Let’s create our .sln file, name it Reminders.sln and add the four projects to it (inside the Reminders folder):

### $ dotnet new sln --name Reminders
### $ dotnet sln add Reminders.Web/Reminders.Web.csproj
### $ dotnet sln add Reminders.Cli/Reminders.Cli.csproj
### $ dotnet sln add Reminders.Common/Reminders.Common.csproj
### $ dotnet sln add Reminders.Tests/Reminders.Tests.csproj

## Adding project references

In full Visual Studio when you want to add a reference to a project you can just right click on references and select Add Reference, pick the project you want to add and you’re done.

VS Code does not have that functionality. To do this we need to resort to the command line, but it’s just as easy. For example, let’s reference Reminders.Common in Reminders.Web, Reminders.Cli and Reminders.Tests.

Navigate to the folder where the .sln file is (Reminders) and type:

### $ dotnet add Reminders.Web/Reminders.Web.csproj reference Reminders.Common/Reminders.Common.csproj
### $ dotnet add Reminders.Cli/Reminders.Cli.csproj reference Reminders.Common/Reminders.Common.csproj
### $ dotnet add Reminders.Tests/Reminders.Tests.csproj reference Reminders.Common/Reminders.Common.csproj

If you want to have a look at which other projects are referenced by a particular project you can either open the .csproj file and have a look at the ProjectReference entries or if you want to do it from the command line: dotnet list PathToCsProj reference.

You can also navigate to the folder where a particular project is and simply do 
### dotnet add reference pathToOtherProject.csprj


## Picking the startup project

Open the solution in VS Code. The easiest way to do that is to navigate to the Reminders folder (where the .sln file is) and type code ..

When you do that you should get a message in VS Code: “Required assets to build and debug are missing from ‘Reminders’. Add them?”

Click Yes.

That will create a .vscode folder.

In that folder there are two files: launch.json and tasks.json. launch.json configures what happens when you press F5 inside VS Code.

For me the project that will run is the Reminders.Cli console project:

{
// Use IntelliSense to find out which attributes exist for C# debugging
// Use hover for the description of the existing attributes
// For further information visit https://github.com/OmniSharp/omnisharp-vscode/blob/master/debugger-launchjson.md
"version": "0.2.0",
"configurations": [
    {
      "name": ".NET Core Launch (console)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      // If you have changed target frameworks, make sure to update the program path.
      "program": "${workspaceFolder}/Reminders.Cli/bin/Debug/netcoreapp2.0/Reminders.Cli.dll",
      "args": [],
      "cwd": "${workspaceFolder}/Reminders.Cli",
      // For more information about the 'console' field, see https://github.com/OmniSharp/omnisharp-vscode/blob/master/debugger-launchjson.md#console-terminal-window
      "console": "internalConsole",
      "stopAtEntry": false,
      "internalConsoleOptions": "openOnSessionStart"
    },
    {
      "name": ".NET Core Attach",
      "type": "coreclr",
      "request": "attach",
      "processId": "${command:pickProcess}"
    }
  ]
}

Also, the tasks.json will only compile the Reminders.Cli project:

{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build",
      "command": "dotnet",
      "type": "process",
      "args": [
        "build",
        "${workspaceFolder}/Reminders.Cli/Reminders.Cli.csproj"
      ],
      "problemMatcher": "$msCompile"
    }
  ]
}

It is possible to customize these two files so that you can pick which project to run. In this case we want to be able to run either Reminders.Web or Reminders.Cli.

The first thing you should do is to remove the second item in tasks.json‘s args array: “${workspaceFolder}/Reminders.Cli/Reminders.Cli.csproj”. We don’t need it because we have the solution file (Reminders.sln). When dotnet build is executed in the folder that contains the sln file all the projects referenced in the solution get built.

After that we can now go to launch.json and click the “Add Configuration…” button in the bottom left corner.

Add Configuration... button

Select “.Net: Launch a local .NET Core Web Application” and in the generated json change “program” and “cwd” (current working directory) to:

### "program": "${workspaceRoot}/Reminders.Web/bin/Debug/netcoreapp2.0/Reminders.Web.dll",
### "cwd": "${workspaceRoot}/Reminders.Web"

You can now go to the Debug “section” of Visual Studio Code and see the Web and Console app configuration there:


Note that you can change the configuration names if you like, just change the “name” property in launch.json.

Now when you press F5 inside VS Code the project that will run is the one that is selected in the Debug section of VS Code.

This only affects VS Code though, the dotnet run command is unaffected by these changes. In the terminal, if you navigate to the solution’s folder and type dontet run you’ll get an error (Couldn’t find a project to run…). You need to use the --project argument and specify the path to the csproj file you want to run, e.g.: dotnet run --project Reminders.Web/Reminders.Web.csproj. Alternatively you can navigate to the project’s folder and execute dontet run there.




## Adding Classes and Interfaces

In full Visual Studio you have several templates available when adding new files to a project.

In VS Code the only out of the box option is to create a new file and use the VS Code snippets (e.g. typing class and then TAB).

Fortunately there’s a VS Code extension named “[C# Extensions](https://marketplace.visualstudio.com/items?itemName=jchannon.csharpextensions)”  which adds two options to the context menu on VS Code’s explorer view: “New C# Class” and “New C# Interface”.

This extension also adds the ability to generate a class’ constructor from properties, generate properties from the constructor and add read-only properties and initialize them (there are demos of this in the extension’s page).

First thing you need to do is install the “C# Extensions extension”. Then right click on Reminders.Common in the explorer view and select New Class:

Name it [Reminder.cs] and create the three properties. In the end it should look like this:

  using System;

  namespace Reminders.Common
  {
    public class Reminder
    {
      public int Id { get; set; }
      public string Description { get; set; }
      public DateTime Date { get; set; }
      public bool IsFinished { get; set; }
    }
  }

Now put the cursor on the opening “{” after class Reminders and click the light bulb and select “Initialize ctor from properties”:

You should end up with this:

using System;

namespace Reminders.Common
{
    public class Reminder
    {
        public Reminder(int id, string description, DateTime date, bool isFinished)
        {
            this.Id = id;
            this.Description = description;
            this.Date = date;
            this.IsFinished = isFinished;

        }
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsFinished { get; set; }
    }
}

Although the constructor isn’t really necessary (and it won’t play well with Entity Framework if you are planning to use it) , it’s just an example of some nice features you get from the extension. Also, when defining properties you can use the same snippets that are available in full Visual Studio (i.e. type prop and press TAB).



## Razor views

Razor pages (.cshtml) is definitely an area where VS Code is lacking. There’s no intellisense or even auto-indent.

There’s an extension in the marketplace named [ASP.NET Helper](https://marketplace.visualstudio.com/items?itemName=schneiderpat.aspnet-helper) which will get you intellisense, however you have to import the namespaces for your models in _ViewImports.chtml (the extension’s requirements are at the bottom of the extensions page). Also in its current version it only seems to work if the model you use is in the same project as the view.

This extension can be useful in some situations but it’s very limited.
** Install ASP.NET Helper extension.**



## Nuget packages

In the full version of Visual Studio there’s an option to Manage NuGet packages. You get an UI where you can search, update and install NuGet packages. In VS Code there’s no support for NuGet out of the box.

Thankfully there’s an extension you can install that will enable searching, adding and removing NuGet packages from VS Code. The extension is named NuGet Package Manager.

Alternatively you can use the command line. To add a package to a project the syntax is: dotnet add PathToCsproj package PackageName. Or, if you navigate to the project you want to add the NuGet package to, you can omit the path to the csproj file: dotnet add package PackageName.

Imagine you wanted to add the Microsoft.EntityFrameworkCore package to Reminders.Common class library. After navigating to it:

### $ dotnet add package Microsoft.EntityFrameworkCore



## Adding tools

Let’s imagine we want to use the Entity Framework tooling in our web project. Navigate to [Reminders.Web] and open the csproj file.

Make note of the version of Microsoft.AspNetCore.All package. For example, let’s imagine it’s “2.0.0”.

Create a new ItemGroup (or find the ItemGroup that already has DotNetCliToolReferences) and inside add the DotNetCliToolReference for the Microsoft.EntityFrameworkCore.Tools.DotNet with Version="2.0.0".

  <ItemGroup>
      <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.0" />            
  </ItemGroup>
  <ItemGroup>
      <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" /> <!-- ADD THIS -->
      <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0" />        
  </ItemGroup>

You should now be able to run dotnet ef in the Reminders.Web project.



## Running tests


One thing your development workflow probably involves is writing unit tests.

VS Code comes with out of the box support for running unit tests. For example if you open UnitTest1.cs you’ll notice that over the test method there are two links, run test and debug test:

This will allow you to run a unit test at a time, there’s no way inside VS Code of running all the tests in a project. You can however navigate to a test project in a terminal and type:

### $ dotnet test

This is the output you should get if you do it for Reminders.Tests:

Starting test execution, please wait...
[xUnit.net 00:00:00.4275346]   Discovering: Reminders.Tests
[xUnit.net 00:00:00.5044862]   Discovered:  Reminders.Tests
[xUnit.net 00:00:00.5549595]   Starting:    Reminders.Tests
[xUnit.net 00:00:00.6980509]   Finished:    Reminders.Tests

Total tests: 1. Passed: 1. Failed: 0. Skipped: 0.
Test Run Successful.

Alternatively you can specify the path to the test project you want to run, for example dotnet test Reminders.Tests/Reminders.Tests.csproj.

It is also possible to specify which tests to run in the command line, for example say you only want to run the tests in class MyClassTests. Here’s how you can do that:

$ dotnet test --filter MyClassTests

The filter functionality has several more options. The example above is short for dotnet test --filter FullyQualifiedName~MyClassTests (~ means contains).

There are other handy options, for example in xUnit it’s possible to specify “traits” for tests, for example:

[Trait("TraitName", "TraitValue")]
public class UnitTest1
{
    [Fact]        
    public void Test1()
    {
        //...
    }
}

You can then run:

$ dotnet test --filter TraitName=TraitValue

And only tests that have that trait will be run. You can specify Trait on a method or class level.

Although trait is specific to xUnit, there are equivalent constructs on other testing frameworks. For example, mstest has TestCategory.

If you prefer to do everything inside VS Code you can create tasks for running tests. First go to tasks.json and make a copy of the build task, change its label to test (or whatever you want to call it) and update the args so that instead of build, test is executed with the path to the test project, for example for Reminders.Tests:

    {
        "label": "test",
        "command": "dotnet",
        "type": "process",
        "args": [
            "test",
            "${workspaceFolder}/Reminders.Tests/Reminders.Tests.csproj"
        ],
        "problemMatcher": "$msCompile"
    } 

You can run tasks in VS Code by using the command palette (Ctrl + Shift + P) and typing “Run Task” and then selecting the task you want to run. You can even set a task as the “test task” and assign a keyboard shortcut for it (the same way you can do Ctrl + Shift + B to trigger a task configured as the “build task”).

To set a task as the “test task” open the command palette and select “Tasks: Configure Default Test Task”, choose the test task and you’re done.


## One last tip

Sometimes VS Code (actually Omnisharp) seems to go haywire, for example intellisense stops working. When that happens you can either reload VS Code by using the command palette and selecting “Reload Window” or the less aggressive option is to restart Omnisharp (the command name is: Omnisharp: Restart Omnisharp).

Hope this guide was helpful, let me know in the comments.

Filed Under: .Net, .Net Core, ASP.NET Core
Primary Sidebar
Rui Figueiredo

