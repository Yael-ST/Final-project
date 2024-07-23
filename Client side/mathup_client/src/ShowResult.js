// // import React, { useState, useRef } from 'react';
// // import axios from 'axios';
// // import './App.css';

// // function ShowResult() {
// //  //https://localhost:7243/api/math
// //   const baseurl='https://localhost:7243/api/math';
// //   // async function  postDataToserver ()  {
// //   //  await axios.get<string>(`${baseurl}`)
 
// //   //  .catch(error => {
// //   //   console.error('Error fetching data:', error);
// //   // });
// //   // };
// //  async function getData(){
// //  await axios.get(baseurl)
// // .then(ans=>{})
// //     .catch(error => {
// //       console.error('Error fetching data:', error);
// //     });
// //   };

// //   return (
// //     <div className="ShowResult">
// //           <button onClick={getData}>answer</button>
// //           </div>
// //   );
// // }

// // export default ShowResult;

// import React, { useEffect, useState } from 'react';
// import axios from 'axios';

// function ShowResult() {
//   const [data, setData] = useState(null);
//   const [loading, setLoading] = useState(true);
//   const [error, setError] = useState(null);

//   useEffect(() => {
//     // בצע קריאה ל-API כאן
//     axios.get('https://localhost:7243/api/math')
//       .then((response) => {
//         console.log(response.data)
//         setData(response.data);
//         setLoading(false);
//       })
//       .catch((error) => {
//         setError(error);
//         setLoading(false);
//       });
//   }, []); // השתמש במערך ריק כדי לוודא שהאפקט ירוץ רק פעם אחת

//   if (loading) {
//     return <div>Loading...</div>;
//   }

//   if (error) {
//     return <div>Error: {error.message}</div>;
//   }

//   return (
//     <div>
//       <h1>API Data</h1>
//       <pre>{JSON.stringify(data, null, 2)}</pre>
//     </div>
//   );
// }

// export default ShowResult;

