# Bug Tracker
As I ran out of time I was unable to complete the React UI project that would have completed the task. Due to my lack of experience with the framework, I struggled in getting npm to play nice with Visual Studio.
However the experience has reignited my interest in adopting the React framework.

This was the first time I've used Git as a repo. Thankfully it very familiar to Dev Ops so it didn't take long build get started.

I've seperated the Bug Tracker solution into smaller several projects which can work in isolation. This not only makes them easier to manage, it allows them to be modular. If I was making a Core Web App, I could just attach the Service layer without the need to amend existing code.

I worked in order of the dependencies starting with Database layer and was supposed to finish with the UI.

## Core
This library contains the abstract classes that the entity models and DTOs inherit. If this was a Core Web Application, the View Models would also inherit the abstract classes.
This makes the solution easier maintain as a new data item can be introduced to various superclasses with a single line of code.

Its quite possible to use the same model for the entity as the DTO but I believe its bad practice to expose the database layer via an open API.

## Database
I've used Entity Framework for my database layer as this is something I familiar with. It does have its limitations particuarly when it comes to generating more complicated queries or using other databases/linked servers.
These limtiations can be easily be mitigated using Views or Stored Procedures

## Repository
This logic in this layer is for the interation with the database.

I experimented with CRUD Interface as I wanted to demonstrate a use of Generics and code re-use. Normally I would have created a seperate interface as I found some entities can have queries that sit outside the typical CRUD pattern for example, a Soft Delete.

## Service
The logic in this layer contains the Business Logic.

The beauty of seperating the service layer from the Repostiory layer means that the Service can be hooked up to another repository if the database changes without amending any of the business logic. It could be that repository calls make a HTTP call to an API rather than utilise database which would have no impact on the Service layer.

## API
The purpose of the API is to handle HTTP requests bridge that request with the Service Layer. No Business Logic is to be found in this layer.

I've also included Swagger with the API which allows the some testing to be perfomed though I prefer using a solution such as Postman for API testing.
The main reason I use Swagger is that it produced a web front-end in a development environment that I can point colleagues to without having to explain how to use the API.

## Testing
This contains examples of Integration and Unit Testing. Like with the UI, I was running out of time so I didn't get chance to make good testing coverage over the rest of the solution.

The Integration Tests work from end to end using the API and interact with a live database. This will expose any compatability issues that sits on the border of the solution like entity frameworks interactons with the database.
It can also identify bottlenecks with the application effiency.

The Unit Tests are testing each component in isolation. This works by creating a mock of the any dependancies. In the example the database is mocked with 

I'm also aware of the UI Testing using Selenium but I've never had the opportunity to try it. I believe Integration Tests can also perform UI Testing.
