import { useEffect, useState } from 'react';
import { getAllEmployees, deleteEmployee } from '../services/employeeService';
import { EmployeeResponse } from '../types/EmployeeResponse';
import { PaginationHeaders } from '../types/PaginationHeaders';
import EmployeeList from './EmployeeList';
import Pagination from './Pagination';
import LoadingSpinner from './LoadingSpinner';

const Home = () => {
  const [employees, setEmployees] = useState<EmployeeResponse[]>([]);
  const [pagination, setPagination] = useState<PaginationHeaders>();
  const [loading, setLoading] = useState<boolean>(true);
  const pageSize = import.meta.env.VITE_PAGE_SIZE_DEFAULT || 10;
  const [currentPage, setCurrentPage] = useState(1);

  const fetchEmployees = async (pageNumber: number, pageSize: number): Promise<void> => { 
    try {
      setLoading(true);
      const result = await getAllEmployees(pageNumber, pageSize);        

      if (result.data.length > 0) {
        setEmployees(result.data);  
        setPagination(result.pagination);
      } else {
        setEmployees([]);
        setPagination(result.pagination);
      }
    } catch (error) {
      console.error('An error occurred while fetching employees:', error);
    } finally {
      setLoading(false);  
    }
  };

  useEffect(() => {
    fetchEmployees(currentPage, pageSize); 
  }, [currentPage, pageSize]);

  const handleDeleteEmployee = async (employeeId: string) => {
    try {
      const success = await deleteEmployee(employeeId);
      if (success) {
        fetchEmployees(currentPage, pageSize);
      } else {
        console.error('Failed to delete employee.');
      }
    } catch (error) {
      console.error('Error while deleting employee:', error);
    }
  };

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
    return <LoadingSpinner />;
  }

  return (       
    <div className="relative min-h-screen isolate px-6 pt-14 lg:px-8 flex flex-col">
      {employees.length === 0 ? (
        <div className="text-center text-gray-500 mt-40 flex-grow">
          <p className="text-xl font-semibold">No employees found</p>
          <p className="text-gray-400">Please add new employees.</p>
        </div>
      ) : (
        <EmployeeList
          employees={employees}
          onDelete={handleDeleteEmployee}
        />
      )}
       <div className="mt-auto">
        <Pagination
        pagination={pagination} 
        onNextPage={handleNextPage} 
        onPreviousPage={handlePreviousPage} 
        />
       </div>
    </div>
  );
};

export default Home;
