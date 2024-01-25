import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Login = () => {
  const user = JSON.parse(sessionStorage.getItem('userdata'));
  const [formdata, setFormdata] = useState({
    email: '',
    password: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormdata((data) => ({
      ...data,
      [name]: value,
    }));
  };

  const navigate = useNavigate();

  const onClick = () => {
    console.log(user.email);
    if (formdata.email === user.email && formdata.password === user.password) {
      navigate('/dashboard');
    } else {
      alert('You are not registered. Please sign up.');
      navigate('/');
    }
  };

  return (
    <div className="container-fluid">
      <form>
        <div className="mb-3">
          <label htmlFor="email" className="form-label">
            Email:
          </label>
          <input
            type="email"
            name="email"
            value={formdata.email}
            onChange={handleChange}
            className="form-control"
          />
        </div>

        <div className="mb-3">
          <label htmlFor="password" className="form-label">
            Password:
          </label>
          <input
            type="password"
            name="password"
            value={formdata.password}
            onChange={handleChange}
            className="form-control"
          />
        </div>

        <button type="button" className="btn btn-primary" onClick={onClick}>
          Login
        </button>
      </form>
    </div>
  );
};

export default Login;
