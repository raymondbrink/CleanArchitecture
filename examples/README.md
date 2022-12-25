# NetActive.CleanArchitecture - Examples
This folder contains some examples showcasing the usage of the NetActive.CleanArchitecture libraries.
The NetActive.CleanArchitecture libraries heavily rely on Dependency Injection (DI) based on Autofac.
With some tweaking other dependency inversion frameworks can be used as well.

## Clear Architecture layer projects
A few project here are examples of Clean Architecture layer projects.
You can think of them as examples that follow some convention based guidelines.
You need one project for each layer of the Clean Architecture model ([more info here](SolutionStructure.md)).

## Examples

All example applications in this repo use the same `Example.Shared` project. 
Besides that, please find below an overview of all example application projects provided.
Each example project is separately documented. 
Click the links below to read about them in more detail.
If you'd like an example to be added for a specific scenario not listed here, please let us know and we'll be more than happy to add it.

### 0. Example.Shared

This project is just for our convinience. To not repreat ourselves in every example application project, we've combined all DI container configuration in one separate project.
Basically each example project mentioned below uses the same Dependency Injection container from this project, configured with the same registered components.
This is where the DI "magic" happens. It ties all the layers together by registering the required modules en repositories.
It also holds a reference to the settings file and the connection string used in [\Constants\Settings.cs](Example.Shared/Constants/Settings.cs).

### 1. Example.Console.CommandAdd

This example showcases a Command example on how to add a new entity to the database. 
It goes into detail about a typical Command structure, usage of entity factory, implementing input validation and working with a unit of work.  
More info: [Example.Console.CommandAdd](Example.Console.CommandAdd)

### 2. Example.Console.QueryExists

This example showcases a Query to determine if a specific entity exists in the database. 
It goes into detail on the components in play and how they all ty together.  
More info: [Example.Console.QueryExists](Example.Console.QueryExists)

### 3. Example.Console.QueryList

This example showcases a Query to retrieve a list of entities from the database. 
It goes into detail on the components in play and how they all ty together.  
More info: [Example.Console.QueryList](Example.Console.QueryList)

### 4. Example.Console.QueryMultilingual

This example goes into working with multi lingual data. 
How do you store such data efficiently and how do you query it maintaining fast performance?  
TODO: Document this project
<!--More info: [Example.Console.QueryMultilingual](Example.Console.QueryMultilingual)-->

### 5. Example.Console.QueryPaged

This example showcases a Query to retrieve a **paged** list of entities from the database. 
It goes into detail on the components in play, how to manipulate the query parameters and how it all ties together.  
More info: [Example.Console.QueryPaged](Example.Console.QueryPaged)

### 6. Example.Web.API

This example showcases Commands and a Queries with and without the use of MediatR.
The `ComponyController` directly executes queries.
The `ManufacturerController` uses MediatR to send commands and a query to the respecive handlers.  
TODO: Document this project
<!--More info: [Example.Web.API](Example.Web.API)-->

Happy coding!

