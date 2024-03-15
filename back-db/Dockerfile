FROM mcr.microsoft.com/mssql/server:2019-latest
 
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=ODEGVUOVNU95pkoowvllgv20
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433 

WORKDIR /src


RUN mkdir app
RUN chmod 777 app
WORKDIR /src/app

COPY entrypoint.sh /src/app
COPY run-initialization.sh /src/app
COPY ajusteDatabase.sql /src/app


USER root
RUN chmod +x /src/app/run-initialization.sh
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" 
CMD /bin/bash ./entrypoint.sh