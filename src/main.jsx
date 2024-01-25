import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import Navbarbootstrap from './Components/Navbarbootstrap';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Navbarbootstrap/>
    <App />
  </React.StrictMode>,
)
