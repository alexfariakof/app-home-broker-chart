#!/bin/bash

projectTestPath=$(pwd)
projectPath=$(realpath ..)
sourceDirs="$projectPath/Business;$projectPath/Domain;$projectPath/Repository;$projectPath/HomeBrokerSPA;$projectPath/HomeBrokerSPA/HomeBrokerChart"
reportPath="$projectTestPath/TestResults"
coverageXmlPath="$projectTestPath/TestResults/coveragereport"

# Executa Testes Unitários sem restore e build e gera o relatório de cobertura
dotnet test  ./HomeBrokerXUnit.csproj --results-directory "$reportPath" -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore --no-build > /dev/null 2>&1
reportgenerator -reports:$projectTestPath/coverage.cobertura.xml -targetdir:$coverageXmlPath/coveragereport -reporttypes:'Html;lcov;' 
