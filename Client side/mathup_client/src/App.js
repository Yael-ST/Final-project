import React, { useState, useRef } from 'react';
import Webcam from 'react-webcam';
// import axios from 'axios';
import './App.css';
import UploadingPicture from './UploadingPicture';

function App() {
 
  return (
    <div className="App">
       <img src="logo2.png" alt="הלוגו של האתר" className="logoImage" />
       <UploadingPicture></UploadingPicture>
        </div>
  );
}

export default App;
