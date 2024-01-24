import { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS
import { Route, createBrowserRouter, createRoutesFromElements, RouterProvider } from 'react-router-dom';
import Login from './Login';
import Register from './Register';
import Dashboard from './Dashboard';

function App() {
  const router = createBrowserRouter([
    {
      path: '/',
      element: <Register />
    },
    {
      path: '/login',
      element: <Login />
    },
    {
      path: '/dashboard',
      element: <Dashboard />
    }
  ]);

  return (
    <>
      <>
        <RouterProvider router={router} />
      </>
    </>
  );
}

export default App;
