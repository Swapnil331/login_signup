import React from 'react';
import { useFormik } from 'formik';
import {  useNavigate } from 'react-router-dom';
import * as Yup from "yup";
import { signupschema } from './schemas';

const Signup = () => {
  const navigate = useNavigate();
  const onClick =()=>{
     navigate('/login');
  }
 
  const validateSchema = Yup.object({
    Name: Yup.string().min(2).required("Firstname Required!"),
    email: Yup.string().email("Email is invalid!").required("Email Required!"),
    // password: Yup.string()
    //   .min(4, "Password must be minimum 4 digits!")
    //   .required("Password Required!"),
    // repass: Yup.string()
    //   .oneOf([Yup.ref("password"), null], "Password must match!")
    //   .required("Confirm password is reqired!"),
    // mobile_no: Yup.number()
    //   .min(14, "Age must be minimum 14 Years!")
    //   .required("Age must be required!"),
  })
  

  const {errors,touched , values,handleChange,handleSubmit} = useFormik({
    initialValues: {
      Name:'',
      email: '',
      password:'',
      mobile_no:'',
      repass:'',

    },
    
    validationSchema: signupschema,
    onSubmit: values => {
      const value = JSON.stringify(values, null, 2);
      alert(value);
      console.log(values);

      navigate('/login');
      
    },
  });
  return (
    <>
    <h1>Signup</h1>
    <form onSubmit={handleSubmit}>
    <label htmlFor="Name">Name : </label>
       <input
         id="Name"
         name="Name" 
         type="Name"
         onChange={handleChange}
         value={values.Name}
         
       />
      { errors.Name && touched.Name ? ( <p className='form-error' >{errors.Name}</p> ) : null }
       <br></br>
       <label htmlFor="email">Email Address</label>
       <input
         id="email"
         name="email"
         type="email"
         onChange={handleChange}
         value={values.email}
         e
       /><br></br>

      <label htmlFor="mobile_no">Mobile Number</label>
       <input
         id="mobile_no"
         name="mobile_no"
         type="mobile_no"
         onChange={handleChange}
         value={values.mobile_no}
         helperText={errors.mobile_no ? errors.mobile_no : "Name is required"}
       /><br></br> 

<label htmlFor="password">Password : </label>
       <input
         id="password"
         name="password"
         type="password"
         onChange={handleChange}
         value={values.password}
       /><br></br>

<label htmlFor="repass">RePassword : </label>
       <input
         id="repass"
         name="repass"
         type="password"
         onChange={handleChange}
         value={values.repass}
       /><br></br>


 
       <button type="submit">Submit</button>
     </form>
    </>

   
  );
};

export default Signup;