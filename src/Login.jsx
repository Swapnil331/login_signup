import React, { useState } from 'react'
import { Navigate, useNavigate } from 'react-router-dom';
const Login = () => {
   
    const user = JSON.parse(sessionStorage.getItem('userdata'));
    
    const [formdata ,setFormdata] = useState({
        email:'',
        password:''
    })
    
    const handleChange =(e)=>{
        const{name,value} = e.target;
        
        setFormdata((data) => ({
            ...data,
            [name] : value
        }));
    }
    const navigate = useNavigate();

    const onClick = () =>{
        
         console.log(user.email);
          if(formdata.email==user.email && formdata.password==user.password){
            navigate('/dashboard');
          }
          else{
            alert("You are not registerd pls signup ");
            navigate('/');
          }
          
    }
  return (
    <>
    <div>Login </div>
    
    <form> 
		<div>
            <label> Email : </label>
            <input
            type = "email"
            name = "email"
            value = {formdata.email}
            onChange={handleChange}
            />
        </div>	

        <div>
            <label>Password : </label>
            <input 
            type = "password"
            name = "password"
            value = {formdata.password}
            onChange={handleChange}
            
            />
        </div>
	</form>

    <button onClick = {onClick}>Login</button>
    </>
  )
}

export default Login