FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash && \
    apt-get update && \
    apt-get install -y nodejs
    
WORKDIR /Birdy

# copy everything else and build app
COPY Birdy /Birdy

# build asp.net core backend
RUN dotnet restore
RUN dotnet publish -c Release -o out

# build angular frontend
WORKDIR /Birdy/ClientApp
RUN npm install
RUN npm -g config set user root && \
    npm install -g @angular/cli && \
    ng build --base-href="/"

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS runtime
WORKDIR /Birdy
COPY --from=build /Birdy/out ./
COPY --from=build /Birdy/ClientApp/dist/ClientApp ./ClientApp/dist
ENTRYPOINT ["dotnet", "Birdy.dll"]

