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

