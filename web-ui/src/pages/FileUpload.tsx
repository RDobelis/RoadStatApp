import React, { ChangeEvent, useState } from "react";
import "../styles/App.scss";
import "../styles/FileUpload.scss";

function FileUpload() {
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
    const uploadedFile = event.target.files && event.target.files[0];
    setFile(uploadedFile);
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!file) return;

    const formData = new FormData();
    formData.append("file", file);

    const response = await fetch("http://localhost:5242/api/FileUpload", {
      method: "POST",
      body: formData,
      credentials: "include",
    });

    if (response.ok) {
      alert("File uploaded successfully.");
    } else {
      alert("File upload failed.");
    }
  };

  return (
    <div className="nav-container">
      <nav>
        <h1>File Upload</h1>
        <div className="file-upload-container">
          <form onSubmit={handleSubmit}>
            <label htmlFor="file-upload">Upload</label>
            <input id="file-upload" type="file" onChange={handleFileChange} />
            <button type="submit">Upload</button>
          </form>
        </div>
      </nav>
    </div>
  );
}

export default FileUpload;
