import * as yup from 'yup';

export const employeeValidationSchema = yup.object().shape({
  hireDate: yup
    .date()
    .required('Hire date is required')
    .max(new Date(), 'Hire date cannot be in the future'),
  department: yup.object().shape({
    departmentName: yup
      .string()
      .required('Department name is required'),
  }),
  user: yup.object().shape({
    firstName: yup
      .string()
      .required('First name is required')
      .min(1, 'First name must be at least 1 character')
      .max(100, 'First name must be less than 100 characters'),
    lastName: yup
      .string()
      .required('Last name is required')
      .min(1, 'Last name must be at least 1 character')
      .max(100, 'Last name must be less than 100 characters'),
    photoBase64: yup
      .string()
      .required('Photo is required'),
    phone: yup
      .string()
      .required('Phone number is required')
      .matches(
        /^[0-9]+$/,
        'Phone number must contain only numbers'
      ),
    address: yup.object().shape({
      street: yup
        .string()
        .required('Street is required'),
      city: yup
        .string()
        .required('City is required'),
      state: yup
        .string()
        .required('State is required'),
      zipCode: yup
        .string()
        .required('Zip code is required')
        .matches(/^[0-9]{5}$/, 'Zip code must be 5 digits'),
    }),
  }),
});
