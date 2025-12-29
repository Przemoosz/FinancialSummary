import { useState } from 'react'

import './App.css'

function GetDataFrombackend() {
  var result = fetch('http://localhost:8080/Deposit/V1/c37eba8e-d899-4def-951a-11461eb2edc3').then(response => console.log(response));
  console.log(result);
  
}


function App() {
  const [count, setCount] = useState(0)

  GetDataFrombackend();
  return (
    <>
      <p>He {count}</p>
    <button onClick={() => setCount}>Btn</button>
    
    </>


  )
}

export default App
