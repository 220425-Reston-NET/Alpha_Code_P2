from mcr.microsoft.com/dotnet/sdk:6.0 as build 
# #working directory docker => docker instruction
 workdir /app 

 # copying our files in docker image * => grabs all sln
copy *.sln ./ 
copy MedTrakApi/*.csproj MedTrakApi/
copy MedTrakBL/*.csproj MedTrakBL/
copy MedTrakDL/*.csproj MedTrakDL/
copy MedTrakModel/*.csproj MedtrakModel/
copy MedTrakTest/*.csproj MedTrakTest/

# copyin everthing expect things I ignore in dockerignore file
copy . ./

# cli command to restore our bin and object by building the app
run dotnet build 

# #new dotnet command (this creates a file caslled publish )
run dotnet publish -c Release -o publish

# ---------- End of build stage -> Run time stage --> multi stage
from mcr.microsoft.com/dotnet/aspnet:6.0 as runtime

workdir /app
#  this copy the publish file created in the build stage
copy --from=build /app/publish ./

# command to tell docker engine where the application start/run from
cmd ["dotnet", "MedTrakApi.dll"]

# default port is 80 
expose 80

