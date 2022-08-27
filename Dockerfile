FROM mcr.microsoft.com/dotnet/aspnet:2.1

WORKDIR /App

COPY . /App

ENTRYPOINT ["dotnet", "SoloDevApp.Api.dll"]