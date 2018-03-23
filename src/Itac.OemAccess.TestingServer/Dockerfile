FROM microsoft/dotnet-framework:latest
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["C:\\app\\Itac.OemAccess.TestingServer.exe"]
