import React from 'react';
import { useFormik } from 'formik';
import {  useNavigate } from 'react-router-dom';
import * as Yup from "yup";
import { signupschema } from './schemas';


const Register = () => {

  const intialValues = Yup.object({
    full_name:Yup.string().min(2).max(10).required("Name is required"),
    email:Yup.string().email().required("Email is required"),
    password: Yup.string()
       .min(4, "Password must be minimum 4 digits!")
       .required("Password Required!"),
    confirm_password: Yup.string()
       .oneOf([Yup.ref("password"), null], "Password must match!")
      .required("Confirm password is reqired!"),
})



  const navigate = useNavigate();
    const formik = useFormik({
        initialValues: {
          full_name: "",
          email: "",
          password: "",
          confirm_password: ""
        },
        validationSchema: intialValues,
        onSubmit: values => {
           const data =  JSON.stringify(values, null, 2);
           sessionStorage.setItem('userdata',data);
          alert(data);
          console.log(data);
          navigate("/login");

        }
      });
  return (
    <div className="App">
      <h1>Validation with Formik + Yup</h1>

      <form onSubmit={formik.handleSubmit}>
        <div>
          <label>Full Name</label>
          <input
            type="text"
            name="full_name"
            value={formik.values.full_name}
            onChange={formik.handleChange}
          />
          {formik.errors.full_name && formik.touched.full_name && (
            <p>{formik.errors.full_name}</p>
          )}
        </div>
        <div>
          <label>Email</label>
          <input
            type="email"
            name="email"
            value={formik.values.email}
            onChange={formik.handleChange}
          />
          {formik.errors.email && formik.touched.email && (
            <p>{formik.errors.email}</p>
          )}
        </div>
        <div>
          <label>Password</label>
          <input
            type="password"
            name="password"
            value={formik.values.password}
            onChange={formik.handleChange}
          />
          {formik.errors.password && formik.touched.password && (
            <p>{formik.errors.password}</p>
          )}
        </div>
        <div>
          <label>Confirm Password</label>
          <input
            type="password"
            name="confirm_password"
            value={formik.values.confirm_password}
            onChange={formik.handleChange}
          />
          {formik.errors.confirm_password &&
            formik.touched.confirm_password && (
              <p>{formik.errors.confirm_password}</p>
            )}
        </div>
        <div>
          <button type="submit" onSubmit={formik.onSubmit}>Submit</button>
        </div>
      </form>
    </div>
  );
}

export default Register