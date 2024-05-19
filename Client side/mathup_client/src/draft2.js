import CustomWebcam from "./CustomWebcam"; // import it

import React, { useState } from 'react';
import './App.css';

function App() {
  const [selectedImage, setSelectedImage] = useState(null); // משתנה זמני לשמירת התמונה שנבחרה
  const [showCamera, setShowCamera] = useState(false);

  // פונקציה שמטפלת בבחירת תמונה ושמירתה במשתנה זמני
  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    if (file)
      setSelectedImage(URL.createObjectURL(file));
  };

  // פונקציה שמפתחת את חלון סייר הקבצים לבחירת תמונה
  const handleButtonClick = () => {
    document.getElementById('fileInput').click();
  };
    // פונקציה שמבטלת תמונה
    const cancle = () => {
      setSelectedImage(null);

    };

  return (

    <div className="App">

      <div className="imageContainer">
        {/* תמונה */}
        <img src="board2.jpg" alt="Your Image" className="image" />
        {/* כיתוב */}
        <img src="logo2.png" alt="הלוגו של האתר" className="logoImage" />

        <div className="captionContainer">

          <h1 className="captionText">!שלום לכם</h1>
          <h2 className="captionText">בואו לאהוב גאומטריה</h2>

          <div className="buttons">
            <button className="uploadButton" onClick={openCamera}>לצילום תמונה</button>

            {/* העלאת תמונה */}
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
              <button  onClick={cancle}>ביטול</button>
              </div>

            )}
     /* {showCamera&& <CustomWebcam />}
    </div>

  );
}

export default App;
