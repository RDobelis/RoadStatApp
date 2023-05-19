const express = require('express');
const path = require('path');

const app = express();
const port = process.env.PORT || 3000;

// Serve static files from the 'build' folder
app.use(express.static(path.join(__dirname, 'build')));

// Handle any API routes or other server logic here if needed

// Serve the index.html file for all other requests
app.get('*', (req, res) => {
  res.sendFile(path.join(__dirname, 'build', 'index.html'));
});

// Start the server
app.listen(port, () => {
  console.log(`Server listening on port ${port}`);
});
