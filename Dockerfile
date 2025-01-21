FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
WORKDIR /App
COPY --from=build /App/out .

# Expose port 80
EXPOSE 80

ENTRYPOINT ["dotnet", "Charity_Fundraising_DBMS.dll"]

FROM mcr.microsoft.com/mssql/server:2022-latest

# Create non-root user for SQL Server
USER root
RUN useradd -M -s /bin/bash -u 10001 mssql

# Set up directories and permissions
RUN mkdir -p /var/opt/mssql && \
    chown -R mssql:mssql /var/opt/mssql

# Switch to mssql user
USER mssql

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourSecurePassword123!
ENV MSSQL_PID=Express

EXPOSE 1433