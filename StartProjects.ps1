Start-Process powershell -ArgumentList "-File .\LaunchBackend.ps1"
Start-Process powershell -ArgumentList "-File .\LaunchFrontend.ps1"

# Delay to allow the server to start
Start-Sleep -Seconds 10

# Open localhost:3000 in the default web browser
Start-Process "http://localhost:3000"
