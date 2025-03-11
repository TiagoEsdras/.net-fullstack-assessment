import { useEffect, useState } from 'react';
import { getAllEmployees } from './services/employeeService';
import { EmployeeResponse } from './types/EmployeeResponse';
import { PaginationHeaders } from './types/PaginationHeaders';

const App = () => {
  const [employees, setEmployees] = useState<EmployeeResponse[]>([]);
  const [pagination, setPagination] = useState<PaginationHeaders>();
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>(); 

  const pageSize = 10;
  const [currentPage, setCurrentPage] = useState(1);

  const fetchEmployees = async (pageNumber: number, pageSize: number): Promise<void> => { 
    try {
      setLoading(true);
      const result: { data: EmployeeResponse[]; pagination: PaginationHeaders } = await getAllEmployees(pageNumber, pageSize);        

      if (result.data.length > 0) {
        setEmployees(result.data);  
        setPagination(result.pagination);
      } else {
        setError('Error'); 
      }
    } catch (error) {
      console.error('An error occurred while fetching employees:', error);
      setError('Error');
    } finally {
      setLoading(false);  
    }
  };

  useEffect(() => {
    fetchEmployees(currentPage, pageSize); 
  }, [currentPage]);

  const handleNextPage = () => {
    if (pagination && currentPage < pagination.totalPages) {
      setCurrentPage(currentPage + 1);
    }
  };

  const handlePreviousPage = () => {
    if (pagination && currentPage > 1) {
      setCurrentPage(currentPage - 1);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  };

  if (error) {
    return <div>Error: {error}</div>;
  };

  return (
    <div>
      <h1>Employees</h1>
      <ul>
        {employees.map((employee) => (
          <li key={employee.id}>
            {employee.user.firstName} {employee.user.lastName} - {employee.department.name}
          </li>
        ))}
      </ul>
      
      <div>
        <button onClick={handlePreviousPage} disabled={currentPage === 1}>
          Previous 
        </button>
        <span>
          Page {pagination?.currentPage} of {pagination?.totalPages}
        </span>
        <button onClick={handleNextPage} disabled={currentPage === pagination?.totalPages}>
          Next
        </button>
      </div>
    </div>
  );
};

export default App;