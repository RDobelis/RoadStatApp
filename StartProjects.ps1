# Build the .NET backend
Set-Location -Path "..\RoadStatApp\RoadStat"
dotnet build --configuration Release

# Navigate to the directory of your .NET app and start the process
Set-Location -Path "..\RoadStatApp\RoadStat\bin\Release\net7.0"
Start-Process -FilePath "RoadStat.exe"

# Build the React frontend
Set-Location -Path "..\RoadStatApp\web-ui"
npm install
npm run build

# Start a new PowerShell process to run the frontend server
Start-Process powershell -ArgumentList {
    Set-Location -Path "../RoadStatApp/web-ui"
    node server.js
}

# Pause to ensure frontend server is fully running before opening the web browser
Start-Sleep -Seconds 2

# Open localhost:3000 in the default web browser
Start-Process "http://localhost:3000"
