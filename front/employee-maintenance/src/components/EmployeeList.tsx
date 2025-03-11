import React from 'react';
import EmployeeCard from './EmployeeCard';
import { EmployeeResponse } from '../types/EmployeeResponse';

interface EmployeeListProps {
  employees: EmployeeResponse[];
}

const EmployeeList: React.FC<EmployeeListProps> = ({ employees }) => {
  return (
    <div className="space-y-8 p-4">
      {employees.map((employee) => (
        <EmployeeCard key={employee.id} employee={employee} />
      ))}
    </div>
  );
};

export default EmployeeList;
