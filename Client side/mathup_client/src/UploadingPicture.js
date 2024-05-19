// UploadingPicture.js
import React from 'react';
import './App.css';

const UploadingPicture = ({ setSelectedImage }) => {
  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    if (file) setSelectedImage(URL.createObjectURL(file));
  };

  const handleButtonClick = () => {
    document.getElementById('fileInput').click();
  };

  return (
    <div>
      <button className="uploadButton" onClick={handleButtonClick}>להעלאת תמונה</button>
      <input
        id="fileInput"
        type="file"
        accept="image/*"
        onChange={handleImageUpload}
        style={{ display: 'none' }}
      />
    </div>
  );
};

export default UploadingPicture;
