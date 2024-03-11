This is a take home test designed for back-end interview candidates.

This requires **Visual Studio 2022 version 17.8+** and git

To run the project, git clone the project and open it in Visual Studio.

In the package manager console run the following commands: 

* ```Install-Package Microsoft.EntityFrameworkCore.Tools```

* ```Update-Database```

This will download tools for entity framework and initialise the database.

Set both takehometest.client and TakeHomeTest.Sever as startup projects and run both projects.
Two brower windows should open, one front end react application and one swagger application for the backend.
Once both have loaded you can click the refresh data button on the react app to fetch data from the back-end. Modifing the front-end is not required, 
the front-end is there for testing purposes, along with the swagger interface.


The goal of this test is to implement an API controller with the endpoints that allow the following actions for an object called WeatherForecast:

* Create
* Get
* Update
* Delete

These endpoints should use a service to complete the required action and the service should perform the expected action on the database 
(eg. The update endpoint should call an update funciton from the service. The update function should update an entry in the WeatherForecast table in the database).
The Get endpoint is already completed.

Unit tests have been added to assert expected behaviors.