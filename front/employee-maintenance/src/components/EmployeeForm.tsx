import { useState } from 'react';
import { createEmployee } from '../services/employeeService';
import LoadingSpinner from './LoadingSpinner';
import { convertToBase64 } from '../utils/convertFileToBase64';
import { EmployeeRequest } from '../types/CreateEmployeeRequest ';
import { useNavigate } from 'react-router-dom';
import { employeeValidationSchema } from '../validation/employeeValidationSchema';
import * as yup from 'yup';

const EmployeeForm = () => {
  const [employee, setEmployee] = useState<EmployeeRequest>({
    hireDate: '',
    department: { departmentName: '' },
    user: {
      firstName: '',
      lastName: '',
      phone: '',
      photoBase64: '',
      address: {
        street: '',
        city: '',
        state: '',
        zipCode: '',
      },
    },
  });

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [fileUploaded, setFileUploaded] = useState(false);
  const navigate = useNavigate();

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setEmployee((prevEmployee: EmployeeRequest) => ({
      ...prevEmployee,
      user: {
        ...prevEmployee.user,
        [name]: value,
      },
    }));
  };

  const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setEmployee((prevEmployee: EmployeeRequest) => ({
      ...prevEmployee,
      user: {
        ...prevEmployee.user,
        address: {
          ...prevEmployee.user.address,
          [name]: value,
        },
      },
    }));
  };

  const handleDepartmentChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEmployee((prevEmployee: EmployeeRequest) => ({
      ...prevEmployee,
      department: { departmentName: e.target.value },
    }));
  };

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    const maxSizeInBytes = 2 * 1024 * 1024;
    if (file) {
      if (file.size > maxSizeInBytes) {
        alert("File must be lower than 2MB.");
        return;
      }
      const base64Image = await convertToBase64(file);
      setEmployee((prevEmployee: EmployeeRequest) => ({
        ...prevEmployee,
        user: {
          ...prevEmployee.user,
          photoBase64: base64Image as string,
        },
      }));
      setFileUploaded(true); 
    }
  };

  const handleRemoveImage = () => {
    setEmployee((prevEmployee: EmployeeRequest) => ({
      ...prevEmployee,
      user: {
        ...prevEmployee.user,
        photoBase64: '',
      },
    }));
    setFileUploaded(false);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      await employeeValidationSchema.validate(employee, { abortEarly: false });
      await createEmployee(employee);
      setLoading(false);
      navigate('/');
    } catch (error) {
      if (error instanceof yup.ValidationError) {       
        const errorMessages = error.errors.join(', ');
        setError(errorMessages);
      } else {       
        console.error(error);
        setError('Failed to create employee');
      }
      setLoading(false);
    }
  };

  return (
    <div className="relative min-h-screen isolate px-6 pt-14 lg:px-8 flex flex-col">
      <div className="space-y-8 p-4">
        <div className="bg-white p-8 shadow-md rounded-lg max-w-lg mx-auto">
          <h2 className="text-2xl font-bold mb-4">Create New Employee</h2>
          {loading && <LoadingSpinner />}
          {error && <p className="text-red-500">{error}</p>}
          <form onSubmit={handleSubmit}>
            <div className="grid grid-cols-2 gap-4 mb-4">
              <div>
                <label className="block text-gray-700">First Name</label>
                <input
                  type="text"
                  name="firstName"
                  value={employee.user.firstName}
                  onChange={handleInputChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
              <div>
                <label className="block text-gray-700">Last Name</label>
                <input
                  type="text"
                  name="lastName"
                  value={employee.user.lastName}
                  onChange={handleInputChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
            </div>

            <div className="grid grid-cols-2 gap-4 mb-4">
              <div>
                <label className="block text-gray-700">Phone</label>
                <input
                  type="text"
                  name="phone"
                  value={employee.user.phone}
                  onChange={handleInputChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
              <div>
                <label className="block text-gray-700">Department</label>
                <input
                  type="text"
                  name="departmentName"
                  value={employee.department.departmentName}
                  onChange={handleDepartmentChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
            </div>

            <div className="grid grid-cols-2 gap-4 mb-4">
              <div>
                <label className="block text-gray-700">Street</label>
                <input
                  type="text"
                  name="street"
                  value={employee.user.address.street}
                  onChange={handleAddressChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
              <div>
                <label className="block text-gray-700">City</label>
                <input
                  type="text"
                  name="city"
                  value={employee.user.address.city}
                  onChange={handleAddressChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
            </div>

            <div className="grid grid-cols-2 gap-4 mb-4">
              <div>
                <label className="block text-gray-700">State</label>
                <input
                  type="text"
                  name="state"
                  value={employee.user.address.state}
                  onChange={handleAddressChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
              <div>
                <label className="block text-gray-700">Zip Code</label>
                <input
                  type="text"
                  name="zipCode"
                  value={employee.user.address.zipCode}
                  onChange={handleAddressChange}
                  className="w-full px-3 py-2 border rounded-md"
                  required
                />
              </div>
            </div>

            <div className="mb-4">
              <label className="block text-gray-700">Profile Picture</label>
              <div className="flex justify-center items-center w-full">
                <label className="flex flex-col items-center justify-center w-full h-32 border-2 border-dashed border-gray-300 rounded-lg cursor-pointer bg-gray-50 hover:bg-gray-100">
                  {!fileUploaded ? (
                    <div className="flex flex-col items-center justify-center pt-5 pb-6">
                      <p className="mb-2 text-sm text-gray-500">
                        <span className="font-semibold">Click to upload</span>
                      </p>
                      <p className="text-xs text-gray-500">PNG, JPG, or JPEG (Max 2MB)</p>
                    </div>
                  ) : (
                    <div className="flex flex-col items-center justify-center pt-5 pb-6">
                      <p className="mb-2 text-sm text-green-600">File uploaded successfully!</p>
                      <button
                        type="button"
                        onClick={handleRemoveImage}
                        className="text-red-500 underline text-xs"
                      >
                        Remove file
                      </button>
                    </div>
                  )}
                  <input
                    id="profilePic"
                    type="file"
                    accept=".png,.jpeg,.jpg"
                    className="hidden"
                    onChange={handleImageUpload}
                  />
                </label>
              </div>
            </div>

            <div className="mb-4">
              <label className="block text-gray-700">Hire Date</label>
              <input
                type="date"
                name="hireDate"
                max={new Date().toISOString().split('T')[0]}
                value={employee.hireDate}
                onChange={(e) =>
                  setEmployee((prevEmployee: EmployeeRequest) => ({
                    ...prevEmployee,
                    hireDate: e.target.value,
                  }))
                }
                className="w-full px-3 py-2 border rounded-md"
                required
              />
            </div>
            {error && <p className="text-red-500 mb-4">{error}</p>}
            <div className="flex justify-end">
              <button
                type="submit"
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
              >
                Create Employee
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default EmployeeForm;
