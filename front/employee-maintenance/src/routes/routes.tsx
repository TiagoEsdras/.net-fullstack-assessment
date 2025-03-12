import { Routes, Route } from 'react-router-dom';
import Home from '../components/Home';
import EmployeeDetails from '../components/EmployeeDetails';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/employees/:employeeId" element={<EmployeeDetails />} /> 
    </Routes>
  );
};

export default AppRoutes;
