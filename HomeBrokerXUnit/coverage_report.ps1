$projectTestPath = Get-Location
$projectPath =  (Resolve-Path -Path ..).Path
$projectAngular = (Resolve-Path -Path "$projectPath\HomeBrokerSPA\HomeBrokerChart");
$sourceDirs = "$projectPath\Business;$projectPath\Domain;$projectPath\Repository;$projectPath\HomeBrokerSPA;$projectPath\HomeBrokerSPA\HomeBrokerChart"
$reportPath = Join-Path -Path $projectTestPath -ChildPath "TestResults"
$coverageXmlPath = Join-Path -Path (Join-Path -Path $projectTestPath -ChildPath "TestResults") -ChildPath "coveragereport"


 function Wait-TestResults {
    $REPEAT_WHILE = 0
    while (-not (Test-Path $reportPath)) {
        echo "Agaurdando TestResults..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }

    $REPEAT_WHILE = 0
    while (-not (Test-Path $coverageXmlPath)) {
        echo "Agaurdando Coverage Report..."
        Start-Sleep -Seconds 10        
        if ($REPEAT_WHILE -eq 6) { break }
        $REPEAT_WHILE = $REPEAT_WHILE + 1
    }   

 } 

# Excuta Teste Unitarios sem restore gera o relatório de cobertura do Backend
dotnet test ./HomeBroker.XUnit.csproj --configuration Staging --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover" --no-restore > $null 2>&1
reportgenerator -reports:$projectTestPath\coverage.cobertura.xml  -targetdir:$coverageXmlPath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs > $null 2>&1
Wait-TestResults
Start-Process "explorer.exe" $coverageXmlPath\index.html

# Verifica se existe a pasta node_module, e sem não existir executa npm install 
if (-not (Test-Path $projectAngular\node_modules)) {
	cd $projectAngular
	npm install
	cd $projectTestPath 
}

# Executa Teste Unitários e gera o relatório de cobertura do Frontend 
Start-Process npm -ArgumentList "run", "test:coverage" -WorkingDirectory $projectAngular -NoNewWindow