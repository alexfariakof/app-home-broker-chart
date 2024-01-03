function Stop-ProcessesByName {
    param(
        [string]$processName
    )

    $processes = Get-Process | Where-Object { $_.ProcessName -like $processName }

    if ($processes.Count -gt 0) {
        $processes | ForEach-Object { Stop-Process -Id $_.Id -Force }
    }
}

$npmProcessName = 'npm*'
$ngProcessName = 'ng*'
$nodeProcessName = 'node*'
$HomeBrokerApiProcess = "HomeBrokerApi*"

# Parar processos relacionados ao projeto 
Stop-ProcessesByName -processName $npmProcessName
Stop-ProcessesByName -processName $ngProcessName
Stop-ProcessesByName -processName $nodeProcessName
Stop-ProcessesByName -processName $HomeBrokerApiProcess

$projectDirectoryBackEnd = ".\webapi"
$projectDirectoryFrontend = ".\angularapp"
dotnet clean > $null 2>&1
dotnet build --restore

# Verifique se o parâmetro -w foi passado para roda aplicação dotnet em modo watch
if ($args -contains "-w") {
    $startCommand = "dotnet watch run --launch-profile https --project $projectDirectoryBackEnd"
}
else {
    $startCommand = "dotnet run --launch-profile https --project $projectDirectoryBackEnd"
}
if ($LASTEXITCODE -eq 0) {
    Start-Process "npm" -ArgumentList "run start:serve" -WorkingDirectory $projectDirectoryFrontend -WindowStyle Hidden
    Start-Process "https://localhost:7288/swagger";     
    Start-Process powershell -ArgumentList "-NoProfile -Command & { $startCommand }"    
    Start-Process "https://localhost:4200/";       
}
else {
    Write-Host "Falha ao construir a aplicação."
}