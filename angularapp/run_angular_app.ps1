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


# Parar processos relacionados ao projeto
Stop-ProcessesByName -processName $nodeProcessName
Stop-ProcessesByName -processName $npmProcessName
Stop-ProcessesByName -processName $ngProcessName

$projectDirectory = ".\"
Start-Process "npm" -ArgumentList "run start:serve" -WorkingDirectory $projectDirectory -WindowStyle Hidden
