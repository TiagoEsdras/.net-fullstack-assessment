import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { getEmployeeById, getAllDepartments, updateEmployeeDepartment } from '../services/employeeService';
import { EmployeeResponse } from '../types/EmployeeResponse';
import { DepartmentResponse } from '../types/DepartmentResponse';
import { calculateEmploymentDuration } from '../utils/calculateEmploymentDuration';
import { formatHireDate } from '../utils/formatHireDate';
import LoadingSpinner from './LoadingSpinner';

const EmployeeDetails = () => {
  const { employeeId } = useParams<{ employeeId: string }>();
  const [employee, setEmployee] = useState<EmployeeResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [departments, setDepartments] = useState<DepartmentResponse[]>([]);
  const [selectedDepartment, setSelectedDepartment] = useState<string>('');

  useEffect(() => {
    const fetchAllDepartments = async (page: number) => {
      const pageSize = import.meta.env.VITE_PAGE_SIZE_DEFAULT;
      const departmentResult = await getAllDepartments(page, pageSize);
     
      setDepartments(prevDepartments => [...prevDepartments, ...departmentResult.data]);
      if (departmentResult.pagination.totalPages > departmentResult.pagination.currentPage) {        
        await fetchAllDepartments(page + 1);
      }
    };

    const fetchEmployee = async () => {
      if (employeeId) {
        setLoading(true);
        const result = await getEmployeeById(employeeId);
        setEmployee(result.data);
        setSelectedDepartment(result.data.department.name);
        setLoading(false);
      }
    };

    fetchEmployee();
    fetchAllDepartments(1);
  }, [employeeId]);

  const handleDepartmentChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedDepartment(e.target.value);
  };

  const handleUpdateDepartment = async () => {
    if (employee && selectedDepartment) {
      try {
        const updatedEmployee = await updateEmployeeDepartment(employee.id, selectedDepartment);
        setEmployee(updatedEmployee);
      } catch (error) {
        console.error('Failed to update department:', error);
      }
    }
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  if (!employee) {
    return <div>No employee found.</div>;
  }

  const { years, months, days } = calculateEmploymentDuration(employee.hireDate);
  const formattedHireDate = formatHireDate(employee.hireDate);

  return (
    <div className="relative isolate px-6 pt-14 lg:px-8">
      <div className="bg-white shadow-md rounded-lg p-8 flex justify-between items-start space-x-6">
        <div className="flex-shrink-0">
          <img
            src={employee.user.photoUrl ? `${import.meta.env.VITE_BASE_URL}/${employee.user.photoUrl}` : 'https://randomuser.me/api/portraits/lego/1.jpg'}
            alt={`${employee.user.firstName} ${employee.user.lastName}`}
            className="w-80 h-80 rounded-lg object-cover"
          />
        </div>
        <div className="flex-grow">
          <div className="flex justify-between items-center mb-4">
            <div>
              <h2 className="text-2xl font-bold text-gray-800 mb-5">
                {employee.user.firstName} {employee.user.lastName}
              </h2>
              <p className="text-lg font-semibold text-gray-500">Employee ID: {employee.id}</p>
              <p className="text-lg text-gray-600">Department: {employee.department.name}</p>
              <p className="text-lg text-gray-600">Phone: {employee.user.phone}</p>
              <p className="text-lg text-gray-600">
                Address: {employee.user.address.street}, {employee.user.address.city} - {employee.user.address.state}
              </p>
              <p className="text-lg text-gray-600">
                ZipCode: {employee.user.address.zipCode}
              </p>
            </div>
            <div className="text-right">
              <p className="text-sm text-gray-500">Hire Date</p>
              <p className="text-sm text-gray-500">{formattedHireDate}</p>
              <p className="text-sm text-gray-400">{years} years, {months} months, {days} days</p>
            </div>
          </div>
          <div className="flex justify-end items-end mt-6 space-x-4">
            <select
              value={selectedDepartment}
              onChange={handleDepartmentChange}
              className="px-4 py-2 border border-gray-300 rounded-md"
            >
              <option disabled value="">Select Department</option>
              {departments.map((dept) => (
                <option key={dept.id} value={dept.name}>
                  {dept.name}
                </option>
              ))}
            </select>
            <button
              onClick={handleUpdateDepartment}
              className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
            >
              Update Department
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EmployeeDetails;