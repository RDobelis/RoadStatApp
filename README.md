# RoadStat

RoadStat is a web application that allows users to analyze and visualize car speed data. It provides features for uploading car speed data files, filtering the data based on various criteria, and generating average speed charts.

## Functionality

The RoadStat application offers the following functionality:

1. **File Upload**: Users can upload car speed data files, which are then processed and stored in a database for analysis.

2. **Filtered Results**: Users can apply filters to the car speed data, such as minimum speed, date range, and more, to retrieve specific records that meet the criteria.

3. **Average Speed Chart**: Users can select a specific date and view an average speed chart that visualizes the hourly average speed of cars for that day.

## Launching Instructions

To run the RoadStat application locally, follow these steps:

### Backend Setup

1. Launch the backend by running the `RoadStat.exe` executable file from the build folder.

### Frontend Setup

1. Make sure you have Node.js installed on your machine.

2. Open a terminal or PowerShell window and navigate to the root folder of the `web-ui` directory.

3. Run `npm install` to install all the necessary dependencies.

4. After the installation is complete, run `node server.js` to run the frontend.

5. Access the application by opening your web browser and navigating to `localhost:3000`.

**Note**: The backend of the RoadStat application is already configured to connect to an AWS MySQL database, where the car speed data is stored. You don't need to make any changes related to the database connection.

## Author

This RoadStat application was developed by Rihards Dobelis.
