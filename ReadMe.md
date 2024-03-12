This Project requires **Visual Studio 2022 version 17.8+** and git


# Task Instructions

To run the project, git clone the project and open it in Visual Studio.

In the package manager console run the following commands: 

* ```Install-Package Microsoft.EntityFrameworkCore.Tools```

* ```Update-Database```

This will download tools for entity framework and initialise the database.

Set both takehometest.client and TakeHomeTest.Sever as startup projects and run both projects.
Two brower windows should open:

* front end react application 
* swagger application for the backend

Once both have loaded you can click the refresh data button on the react app to fetch data from the back-end. Modifing the front-end is not required, 
the front-end is there for testing purposes, along with the swagger interface. **if both of these pages don't load please see the troubleshooting section**


The first part of the task is to implement an API controller with the endpoints that allow the following actions for an object called WeatherForecast:

* Create
* Get
* Update
* Delete

There are stubs for these in the project.

These endpoints should use a service to complete the required action and the service should perform the expected action on the database.
(eg. The update endpoint should call an update funciton from the service. The update function should update an entry in the WeatherForecast table in the database).

The Get endpoint is already completed as an example and unit tests have been added to assert expected behaviors.

After the above is complete:

* Create a new table called Location which consists of an PK GUID Id and a String Name
* Add an column to the WeatherForecast table called LocationId which is a foreign key to the Location table.
* Ensure that previous work considers this new column (eg update, create, get for WeatherForecastController all now include LocationId and tests now also check LocationId)
* Create a new Controller and endpoint to add Location entries to the Location Table
* Create a new API endpoint and service function for the WeatherForecast that will take a string parameter called locationName and fetch all of the weatherForecasts associated with that location name
* Add tests for the new service function

**Please note that updating the front end to reflect the above changes is not required**

Here is some documentation related to EF to get started:

* [Entity Framework Migrations](https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-8.0&tabs=visual-studio)
* [Linq query basics](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/)

***Once you have finished the task please create a pull request merging into the master branch.***

# Troubleshooting

* If the react front end isn't opening try changing the configuration of takehometest.client/.vscode/launch.json to browsers you have installed on your machine (by default it has edge and chrome)
* If the swagger page isn't opening try changing the first profile in TakeHomeTest.Server/Properties/launchSettings.json from https to http.