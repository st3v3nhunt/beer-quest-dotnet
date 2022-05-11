# Beer Quest API

> A .NET 6
> [Minimal API](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
> implementation for an X-Lab Engineering interview
> challenge

## Pre-requisites

* [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Running the application

If you just want to start the application execute `dotnet run` in the project's
directory.

If you want to hot reload the application as it is being changed (very useful
for development) execute `dotnet watch` in the project's directory.

## Running the application with Docker

The application has been containerised and can be run with Docker. Running the
following commands from the repo's root directory will move into the project
directory, build an image and then run the image (with the name
`beer-quest-dotnet`), mapping the local machine's port 5000 to the container's
port 80. When the container exits it will be removed.

If all commands are successful the application will be available at
[http://localhost:5000](http://localhost:5000) from where the `/venues`
endpoint can be explored.

```
cd BeerQuestApi
docker build -t beer-quest-dotnet:latest .
docker run --rm --name beer-quest-dotnet -p 5000:80 beer-quest-dotnet:latest
```

If port `5000` is not available it can be changed to another available port.

## Testing the application

The application can be tested once by running `dotnet test` in the test project
directory. If you want to run tests every time a file changes run `dotnet watch
test` in the test project directory.

## API Documentation

Swagger has been included to document the API. When the application is first
run, this is the default page that will open. From the swagger the APIs can be
explored.

## Some thoughts, considerations and next steps

* Refactor the data loading so the tests can create specific test data and
  tests cases
* Containerise the app
* Consider additional middleware such as authentication
* Consider other DB providers, particularly ones that support geospatial
  searches
* Having Swagger be as easy as it is, is awesome!
* Add CI pipeline followed by CD pipeline, possibly to Azure
