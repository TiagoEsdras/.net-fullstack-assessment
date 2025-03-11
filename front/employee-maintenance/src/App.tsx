import { useEffect, useState } from 'react';
import { getAllEmployees } from './services/employeeService';
import { EmployeeResponse } from './types/EmployeeResponse';
import { PaginationHeaders } from './types/PaginationHeaders';
import Header from './components/Header';
import EmployeeList from './components/EmployeeList';
import Pagination from './components/Pagination';
import Footer from './components/Footer';

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
      const result = await getAllEmployees(pageNumber, pageSize);        

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
      <Header />
      <div className="relative isolate px-6 pt-14 lg:px-8">
        <EmployeeList employees={employees} />
        <Pagination
          pagination={pagination} 
          onNextPage={handleNextPage} 
          onPreviousPage={handlePreviousPage} 
        />
      </div>
      <Footer />
    </div> 
  );
};

export default App;
