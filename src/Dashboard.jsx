import React from 'react';
import './App.css';  // Import your custom styles

function Dashboard() {
  // Retrieve data from sessionStorage
  const userData = JSON.parse(sessionStorage.getItem('userdata'));

  return (
    <div className="container">
      <div className="dashboard-container">
        <h2 className="dashboard-welcome">Dashboard</h2>
        {userData ? (
          <div>
            <p className="dashboard-details">Welcome, {userData.full_name}!</p>
            <p className="dashboard-details">Email: {userData.email}</p>
            {/* Add additional fields as needed */}
          </div>
        ) : (
          <p>No user data found. Please log in.</p>
        )}
      </div>
    </div>
  );
}

export default Dashboard;
