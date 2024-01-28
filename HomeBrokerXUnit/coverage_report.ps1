$projectTestPath = Get-Location
$projectPath =  (Resolve-Path -Path ..).Path
$sourceDirs = "$projectPath\Business;$projectPath\Domain;$projectPath\Repository;$projectPath\HomeBrokerSPA;$projectPath\HomeBrokerSPA\HomeBrokerChart"
$reportPath = Join-Path -Path $projectTestPath -ChildPath "TestResults"
$coverageXmlPath = Join-Path -Path (Join-Path -Path $projectTestPath -ChildPath "TestResults") -ChildPath "coveragereport"


# Excuta Teste Unitarios sem restore e build e dera o relatório de cobertura 
dotnet test ./HomeBrokerXUnit.csproj --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore --no-build > $null 2>&1
reportgenerator -reports:$projectTestPath\coverage.cobertura.xml -targetdir:$coverageXmlPath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs