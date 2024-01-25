import * as Yup from 'yup';

export const signupschema = Yup.object({
    full_name:Yup.string().min(2).max(10).required("Name is required"),
    email:Yup.string().email().required("Email is required"),
    password: Yup.string()
       .min(4, "Password must be minimum 4 digits!")
       .required("Password Required!"),
    confirm_password: Yup.string()
       .oneOf([Yup.ref("password"), null], "Password must match!")
      .required("Confirm password is reqired!"),
})