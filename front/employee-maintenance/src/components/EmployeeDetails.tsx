import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { getEmployeeById, getAllDepartments } from '../services/employeeService';
import { EmployeeResponse } from '../types/EmployeeResponse';
import { DepartmentResponse } from '../types/DepartmentResponse';
import { calculateEmploymentDuration } from '../utils/calculateEmploymentDuration';
import { formatHireDate } from '../utils/formatHireDate';

const EmployeeDetails = () => {
  const { employeeId } = useParams<{ employeeId: string }>();
  const [employee, setEmployee] = useState<EmployeeResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [departments, setDepartments] = useState<DepartmentResponse[]>([]); 
  const [selectedDepartment, setSelectedDepartment] = useState<string>(''); 
  const [page, setPage] = useState<number>(1);
  const [hasMoreDepartments, setHasMoreDepartments] = useState<boolean>(true);
  
  useEffect(() => {
    const fetchEmployee = async () => {
      if (employeeId) {
        const result = await getEmployeeById(employeeId);
        setEmployee(result.data);
        setSelectedDepartment(result.data.departmentId);
      }
      setLoading(false);
    };
    
    const pageSize = import.meta.env.VITE_PAGE_SIZE_DEFAULT;

    const fetchDepartments = async (page: number) => {
      const departmentResult = await getAllDepartments(page, pageSize); 
      const newDepartments = departmentResult.data;      
     
      setDepartments(prevDepartments => [...prevDepartments, ...newDepartments]);

      
      if (!(departmentResult.pagination.totalPages > departmentResult.pagination.currentPage)) {
        setHasMoreDepartments(false);
      }
    };

    fetchEmployee();
    fetchDepartments(page);
  }, [employeeId, page]);

  const handleDepartmentChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedDepartment(e.target.value);
  };

  const handleUpdateDepartment = async () => {
    if (employee && selectedDepartment) {     
      console.log(`Updating department to: ${selectedDepartment}`);
    }
  };

  const loadMoreDepartments = () => {
    if (hasMoreDepartments) {
      setPage(prevPage => prevPage + 1);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
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
          onScroll={(e) => {
            const { scrollTop, scrollHeight, clientHeight } = e.currentTarget;
            if (scrollTop + clientHeight >= scrollHeight) {
              loadMoreDepartments();
            }
          }}
        >
          <option disabled value="">Select Department</option>
          {departments.map((dept) => (
            <option key={dept.id} value={dept.id}>
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

      {hasMoreDepartments && <p>Loading more departments...</p>}
    </div>
  </div>
  </div>
  );
};

export default EmployeeDetails;
