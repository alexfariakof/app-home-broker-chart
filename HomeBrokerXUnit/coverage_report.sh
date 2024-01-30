#!/bin/bash

#clear
projectTestPath=$(pwd)
projectPath=$(realpath ..)
projectAngular="$projectPath/HomeBrokerSPA/HomeBrokerChart"
sourceDirs="$projectPath/Business;$projectPath/Domain;$projectPath/Repository;$projectPath/HomeBrokerSPA;$projectPath/HomeBrokerSPA/HomeBrokerChart"
reportPath="$projectTestPath/TestResults"
coverageXmlPath="$projectTestPath/TestResults/coveragereport"

# Excuta Teste Unitarios sem restore e build e gera o relatório de cobertura do Backend
dotnet test  ./HomeBrokerXUnit.csproj --results-directory "$reportPath" -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore --no-build 
reportgenerator -reports:$projectTestPath/coverage.cobertura.xml -targetdir:$coverageXmlPath/coveragereport -reporttypes:'Html;lcov;' > /dev/null 2>&1

cd "$projectAngular"

# Verifica se a pasta node_modules existe e, se não existir, executa npm install
if [ ! -d "node_modules" ]; then
    npm install
fi

# Executa Testes Unitários e gera o relatório de cobertura do Frontend
npm run test:coverage

#cd "$projectTestPath