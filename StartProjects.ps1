# Navigate to the directory of your .NET app and start the process
Set-Location -Path "..\RoadStatApp\RoadStat\bin\Release\net7.0"
Start-Process -FilePath "RoadStat.exe"

# Pause to ensure backend server is fully running before starting frontend server
Start-Sleep -Seconds 2

# Start a new PowerShell process to run the frontend server
Start-Process powershell -ArgumentList {
    Set-Location -Path "../RoadStatApp/web-ui"
    node server.js
}

# Open localhost:3000 in the default web browser
Start-Process "http://localhost:3000"
