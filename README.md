- simple command/query pattern by defining input class, results class and handler
  https://github.com/dzolnjan/simple-cqrs/blob/master/Core/Command/AddOrEditBlog.cs

- using entity framework dbcontext as unit of work, not fighting it with repositories 
  http://rob.conery.io/2014/03/04/repositories-and-unitofwork-are-not-a-good-idea/
  http://lostechies.com/jimmybogard/2012/10/08/favor-query-objects-over-repositories/

- using IoC (autofac) for decopling commands handlers from controllers by exposing simple command dispatcher
  https://github.com/dzolnjan/simple-cqrs/blob/master/Core/Infrastructure/ICommand.cs
  https://github.com/dzolnjan/simple-cqrs/blob/master/Cqrs.Console/Program.cs

- unit and integration testing is simple as it gets, define inputs > execute command > verify outputs
  https://github.com/dzolnjan/simple-cqrs/blob/master/Core.IntegrationTests/Command/AddOrEditBlogTests.cs
