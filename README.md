# net-core-linux

Project was created by https://www.blinkingcaret.com/2018/03/20/net-core-linux/ on VSCode

# Creating the projects and solution file

By this time you should have installed .Net Core and Visual Studio Code. 

$ dotnet new mvc

If you run that as it is, a new MVC project will be created in the current folder, using the name of the current folder as the default namespace and project name.

First thing we need to do is to create a project folder named Reminders and then inside it do:

$ dotnet new mvc --name Reminders.Web
$ dotnet new console --name Reminders.Cli
$ dotnet new classlib --name Reminders.Common
$ dotnet new xunit --name Reminders.Tests

Let’s create our .sln file, name it Reminders.sln and add the four projects to it (inside the Reminders folder):

$ dotnet new sln --name Reminders
$ dotnet sln add Reminders.Web/Reminders.Web.csproj
$ dotnet sln add Reminders.Cli/Reminders.Cli.csproj
$ dotnet sln add Reminders.Common/Reminders.Common.csproj
$ dotnet sln add Reminders.Tests/Reminders.Tests.csproj

