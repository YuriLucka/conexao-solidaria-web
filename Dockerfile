FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY global.json ./
COPY src/ src/
RUN dotnet publish src/ConexaoSolidaria.Web/ConexaoSolidaria.Web.csproj -c Release -o /app/publish

# Site estático (Blazor WebAssembly) servido pelo nginx.
FROM nginx:1.29-alpine AS final
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY seguranca.conf /etc/nginx/seguranca.conf
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
EXPOSE 8080
