
import Webcam from "react-webcam";
import { useCallback, useRef, useState } from "react"; // import useCallback

/*const videoElement = useRef(null);*/


const CustomWebcam = () => {
  const [isShowVideo, setIsShowVideo] = useState(true);

  const webcamRef = useRef(null);
  const [imgSrc, setImgSrc] = useState(null);

  // create a capture function
  const capture = useCallback(() => {
    const imageSrc = webcamRef.current.getScreenshot();
    setImgSrc(imageSrc);
  }, [webcamRef]);

  const retake = () => {
    setImgSrc(null);
  };
  
  const stopCamera = () => {
    setIsShowVideo(false);
  };

  return (
    <div className="container">
      {imgSrc &&
        <img src={imgSrc} alt="webcam" />
      }
      {isShowVideo &&    
        <Webcam height={600} width={700} ref={webcamRef} />
      }
      

      <div className="btn-container">
        {imgSrc &&
          <div>
            <button onClick={retake}>לצילום חוזר</button>
            <button onClick={stopCamera}>ביטול</button>
          </div>
        }
        {isShowVideo&&
         <div>
         <button onClick={capture}>צלם</button>
         <button onClick={stopCamera}>ביטול</button>
         </div>

        }
      </div>
    </div>
  );
};

export default CustomWebcam;

