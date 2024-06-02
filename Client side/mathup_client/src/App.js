import React, { useState, useRef } from 'react';
import Webcam from 'react-webcam';
import './App.css';

function App() {
  const [selectedImage, setSelectedImage] = useState(null);
  const [showCamera, setShowCamera] = useState(false);
  const [showControls, setShowControls] = useState(false);
  const webcamRef = useRef(null);

  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    if (file) setSelectedImage(URL.createObjectURL(file));
  };

  const handleButtonClick = () => {
    document.getElementById('fileInput').click();
  };

  const openCamera = () => {
    setShowCamera(true);
    setShowControls(true);
  };

  const closeCamera = () => {
    setShowCamera(false);
    setShowControls(false);
  };

  const captureImage = () => {
    const imageSrc = webcamRef.current.getScreenshot();
    setSelectedImage(imageSrc);
    closeCamera();
  };

  const retakePhoto = () => {
    setSelectedImage(null);
    openCamera();
  };

  const deletePhoto = () => {
    setSelectedImage(null);
  };

  const cancelSelection = () => {
    setSelectedImage(null);
  };

  return (
    <div className="App">
      <div className="imageContainer">
        <img src="board2.jpg" alt="Your Image" className="image" />
        <img src="logo2.png" alt="הלוגו של האתר" className="logoImage" />
        <div className="captionContainer">
          <h2 className="captionText">!שלום לכם</h2>
          <h1 className="captionText">בואו לאהוב גאומטריה</h1>
          <div className="buttons">
            <button className="uploadButton" onClick={openCamera}>לצילום תמונה</button>
            <button className="uploadButton" onClick={handleButtonClick}>להעלאת תמונה</button>
            <input
              id="fileInput"
              type="file"
              accept="image/*"
              onChange={handleImageUpload}
              style={{ display: 'none' }}
            />
          </div>
        </div>
      </div>
      {selectedImage && (
        <div className="selectedImage3">
          <img src={selectedImage} alt="Selected Image" className="SelectedImage2" />
          <div>
          <button onClick={cancelSelection}>ביטול</button>
          </div>
        </div> 
      )}
      {showCamera && (
        <div>
          <Webcam
            audio={false}
            ref={webcamRef}
          />
          <div>
            {showControls && (
              <div>
                <button onClick={captureImage}>צלם תמונה</button>
                <button onClick={closeCamera}>סגור מצלמה</button>
              </div>
            )}
          </div>
        </div>
        
      )}
    </div>
  );
}

export default App;
