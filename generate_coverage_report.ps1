cls

# Função para matar processos com base no nome do processo que estajam em execução 
function Stop-ProcessesByName {
    $processes = Get-Process | Where-Object { $_.ProcessName -like 'dotnet*' } | Where-Object { $_.MainWindowTitle -eq '' }
    if ($processes.Count -gt 0) {
        $processes | ForEach-Object { Stop-Process -Id $_.Id -Force }
    }
}

$reportPath = Join-Path -Path (Get-Location) -ChildPath "HomeBrokerXUnit\TestResults"
function Remove-TestResults {    
    if (Test-Path $reportPath) {
        Remove-Item -Recurse -Force $reportPath
    }
}

# Encerra qualquer processo em segundo plano relacionado
Stop-ProcessesByName
# Exclui todo o conteúdo da pasta TestResults, se existir
Remove-TestResults

dotnet clean slnPixCharge.sln > $null 2>&1
if ($args -contains "-w") {

    $watchProcess = Start-Process "dotnet" -ArgumentList "watch", "test", "--project ./HomeBrokerXUnit/HomeBroker.XUnit.csproj", "--collect:""XPlat Code Coverage;Format=opencover""", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=cobertura" -PassThru
    $watchProcess.WaitForExit()
}
else {
    dotnet test ./HomeBrokerXUnit/HomeBroker.XUnit.csproj 
}  

 Stop-ProcessesByName; 
 Exit 