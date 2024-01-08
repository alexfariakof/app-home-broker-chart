FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs
RUN npm install -g npm@10.2.5 && \
    npm install -g @angular/core@15 @angular/cli@15

COPY ["package.json", "package-lock.json*", "npm-shrinkwrap.json*", "./"]
RUN npm install && mv node_modules ../
COPY . .
EXPOSE 44476
CMD ["npm", "start"]
