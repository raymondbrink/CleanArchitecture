# NetActive.CleanArchitecture - Examples
This folder contains some examples showcasing the usage of the NetActive.CleanArchitecture libraries.
The NetActive.CleanArchitecture libraries heavily rely on Dependency Injection (DI).
They used to rely on Autofac, but for simplification the libraries have been updated to use Microsoft's Dependency Injection framework.
With some tweaking other dependency inversion frameworks can still be used.

## Clear Architecture layer projects
Some projects here are examples of Clean Architecture layer projects.
You can think of them as examples that follow some convention based guidelines.
You need one project for each layer of the Clean Architecture model ([more info here](SolutionStructure.md)).

## Examples

Please find below an overview of all example application projects provided.
Each example project is separately documented. 
Click the links below to read about them in more detail.
If you'd like an example to be added for a specific scenario not listed here, please let us know and we'll be more than happy to add it.

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
How do you store such data efficiently and how do you query it maintaining optimal performance?  
TODO: Document this project
<!--More info: [Example.Console.QueryMultilingual](Example.Console.QueryMultilingual)-->

### 5. Example.Console.QueryPaged

This example showcases a Query to retrieve a **paged** list of entities from the database. 
It goes into detail on the components in play, how to manipulate the query parameters and how it all ties together.  
More info: [Example.Console.QueryPaged](Example.Console.QueryPaged)

### 6. Example.Web.API

This example showcases Commands and a Queries in a web API project.
TODO: Document this project
<!--More info: [Example.Web.API](Example.Web.API)-->

### 7. Example.Minimal.API

This example showcases the usage of Minimal APIs.
TODO: Document this project
<!--More info: [Example.Minimal.API](Example.Minimal.API)-->

Happy coding!

