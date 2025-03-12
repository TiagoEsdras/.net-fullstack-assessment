import React from 'react';
import EmployeeCard from './EmployeeCard';
import { EmployeeResponse } from '../types/EmployeeResponse';

interface EmployeeListProps {
  employees: EmployeeResponse[];
  onDelete: (id: string) => void;
}

const EmployeeList: React.FC<EmployeeListProps> = ({ employees, onDelete }) => {
  return (
    <div className="space-y-8 p-4">
      {employees.map((employee) => (
        <EmployeeCard key={employee.id} employee={employee} onDelete={onDelete} />
      ))}
    </div>
  );
};

export default EmployeeList;
