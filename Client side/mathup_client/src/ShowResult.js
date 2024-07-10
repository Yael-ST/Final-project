import React, { useState, useRef } from 'react';
import Webcam from 'react-webcam';
import axios from 'axios';
import './App.css';

function ShowResult() {
 
  const baseurl='https://localhost:7243/api/math';
  async function  postDataToserver ()  {
   await axios.post<Decision>(`${baseurl}`, selectedDecision)
      .then(() => {
          
         console.log('השליחה הצליחה')
          // Fetch new data after updating
      });
  };
 async function getData(){
 await axios.get<string>(baseurl+'/5')

    .catch(error => {
      console.error('Error fetching data:', error);
    });
  };

  return (
    <div className="ShowResult">
       <img src="logo2.png" alt="הלוגו של האתר" className="logoImage" />
        </div>
  );
}

export default ShowResult;
