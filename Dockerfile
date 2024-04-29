FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

COPY output .
COPY CDN/embed embed

RUN chmod +x Cord.CDN
ENTRYPOINT ["./Cord.CDN"]
