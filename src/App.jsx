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
    <>
     <>
      <RouterProvider router={router}/>
    </>

    </>
  )
}

export default App
