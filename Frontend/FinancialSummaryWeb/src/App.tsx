import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function GetDataFrombackend(){
  var result = fetch('http://localhost:8080/Deposit/V1/c37eba8e-d899-4def-951a-11461eb2edc3').then(response => console.log(response));

  
}


function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <div>
      <p>Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
    </div>
    </>
  )
}

export default App
