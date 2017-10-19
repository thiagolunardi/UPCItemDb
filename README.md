# UPCItemDb SDK .NET

#### A .NET SDK client library for the [upcitemdb.com](http://www.upcitemdb.com/) API

- Visit <https://devs.upcitemdb.com/docs> for full API documentation.

This is a fully object-oriented implementation of the UPCItemDb API for .NET. 

It is compatible with .NET Standard 2.0, so contempling .NET Core 2.0 and .NET Frameowrk 4.6.1. [Visit here to see other supported platforms](https://docs.microsoft.com/en-us/dotnet/articles/standard/library#net-platforms-support).

XML comments are used extensively throughout, so it should be pretty discoverable if you're using an IDE that supports intellisense.

## Building

Run the build script with [.NET Core](https://www.microsoft.com/net/download/core).

    dotnet build UPCItemDb.sln

You can also build using the solution in Visual Studio 2017 or later.

## Basic Usage

You can install [UPCITemDb SDK from Nuget](https://nuget.org/packages/upcitemdb):

    PM> Install-Package UPCItemDb    

All of the requests can be accessed through a `UPCItemDb` client instance. Instantiate one client with empty API key for testing purpose - their will be limitation, and 100 reqs/day/IP, [check here for more](http://www.upcitemdb.com/wp/docs/main/plan/#free-vs-paid) - or with your API dev/prod key:

    var upcItemDbClient = new UpcItemDbClient();
    
And then consume as the most friendly way ever:

### Looking Up

	var items = await upcItemDbClient.LookupAsync("4002293401102");

### Searching

	var items = await upcItemDbClient.SearchAsync(new SearchParameters("tablet"));
  
## More Samples

[Check the unit test project for all samples](https://github.com/thiagolunardi/UPCItemDb/blob/master/src/UPCItemDB.Tests/UPCItemDbTests.cs).

## License

Licensed under the [MIT](http://www.opensource.org/licenses/mit-license.html) license. See LICENSE.txt.
