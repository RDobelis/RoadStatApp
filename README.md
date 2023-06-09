# RoadStat

RoadStat is a web application that allows users to analyze and visualize car speed data. It provides features for uploading car speed data files, filtering the data based on various criteria, and generating average speed charts.

## Functionality

The RoadStat application offers the following functionality:

1. **File Upload**: Users can upload car speed data files, which are then processed and stored in a database for analysis.

2. **Filtered Results**: Users can apply filters to the car speed data, such as minimum speed and date range, to retrieve specific records that meet the criteria.

3. **Average Speed Chart**: Users can select a specific date and view an average speed chart that visualizes the hourly average speed of cars for that day.

## Problems Encountered

During the development of the RoadStat project, the following problem was encountered:

1. **Deployment on Online Services**: There were difficulties deploying the application on online services such as Azure and AWS. However, a PowerShell script, `StartProjects.ps1`, was created to simplify the process of running the build. This script automates the commands for creating builds for both the backend and frontend, as well as running `npm install` to install the required dependencies. By running the `StartProjects.ps1` script, users can easily launch the RoadStat application without manual configuration.

## Future Improvements

Here are some areas of improvement for the RoadStat project:

1. **Modular Frontend Components**: The frontend React components could be further split into smaller and more reusable components. This would enhance code organization, maintainability, and reusability.

2. **Storybook Integration**: Integrating Storybook into the frontend would be a valuable addition. Storybook allows for interactive component development and documentation. Although it couldn't be implemented within the project's timeline, it is recommended as a future improvement.

These improvements would enhance the overall development experience, code quality, and maintainability of the RoadStat application.

## Launching Instructions

To run the RoadStat application locally, follow these steps:

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org)

### Setup

1. Make sure you have both the .NET SDK and Node.js installed on your machine.

2. Download the RoadStat project and navigate to the root folder of the solution.

3. Run the `StartProjects.ps1` PowerShell script by executing the following command in PowerShell:

   ```powershell
   .\StartProjects.ps1
   ```

   This script will automatically create builds for both the backend and frontend, install the required dependencies, and launch the RoadStat application. It will also open the application in your default web browser at `localhost:3000`.

**Note**: The RoadStat application is connected to an AWS MySQL database, where the car speed data is stored. The database connection is already configured in the backend, so you don't need to make any changes related to the database setup.

## Author

This RoadStat application was developed by Rihards Dobelis.

## Screenshots

<p align="center">
  <b>File Upload Page</b><br>
  <img src="./Page-FileUpload.png" alt="File Upload">
</p>

<p align="center">
  <b>Filtered Results Page</b><br>
  <img src="./Page-FilteredResults.png" alt="Filtered Results">
</p>

<p align="center">
  <b>Average Speed Chart Page</b><br>
  <img src="./Page-AverageSpeedChart.png" alt="Average Speed Chart">
</p>
