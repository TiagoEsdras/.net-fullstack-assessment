import { Routes, Route } from 'react-router-dom';
import Home from '../components/Home';
import EmployeeDetails from '../components/EmployeeDetails';
import EmployeeForm from '../components/EmployeeForm';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/employees/:employeeId" element={<EmployeeDetails />} /> 
      <Route path="/create" element={<EmployeeForm />} />
    </Routes>
  );
};

export default AppRoutes;
