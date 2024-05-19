// Camera.js
import React, { useState, useRef } from 'react';
import Webcam from 'react-webcam';
import './App.css';

const Camera = ({ setSelectedImage }) => {
  const [showCamera, setShowCamera] = useState(false);
  const [showControls, setShowControls] = useState(false);
  const webcamRef = useRef(null);

  const openCamera = () => {
    setShowCamera(true);
    setShowControls(true);
  };

  const closeCamera = () => {
    if (webcamRef.current) {
      webcamRef.current.video.srcObject.getTracks().forEach(track => track.stop());
    }
    setShowCamera(false);
    setShowControls(false);
  };

  const captureImage = () => {
    const imageSrc = webcamRef.current.getScreenshot();
    setSelectedImage(imageSrc);
    closeCamera();
  };

  return (
    <div>
      <button className="uploadButton" onClick={openCamera}>לצילום תמונה</button>
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
};

export default Camera;
