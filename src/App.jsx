import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
// import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Signup  from './Signup';
import Login from './Login';
import Register from './Register';
import { Route, createBrowserRouter, createRoutesFromElements, RouterProvider } from 'react-router-dom';
import Dashboard from './Dashboard';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Navbarbootstrap from './Components/Navbarbootstrap';


function App() {
  const router = createBrowserRouter([
   {
    path : "/",
    element:<Register/>
    
   },
   {
    path : "/login",
    element:<Login/>
   },
   {
    path : "/dashboard",
    element:<Dashboard/>
   }
    ])

  return (
    <div>
     
     
      <RouterProvider router={router}/>
      
      </div>

    
  )
}

export default App
