import React, { useState } from 'react';
import { EmployeeResponse } from '../types/EmployeeResponse';
import { calculateEmploymentDuration } from '../utils/calculateEmploymentDuration';
import { formatHireDate } from '../utils/formatHireDate';
import { Link } from 'react-router-dom';
import ConfirmModal from './ConfirmModal';

interface EmployeeCardProps {
  employee: EmployeeResponse;
  onDelete: (id: string) => void;
}

const EmployeeCard: React.FC<EmployeeCardProps> = ({ employee, onDelete }) => {
  const [showConfirmModal, setShowConfirmModal] = useState<boolean>(false);
  const { years, months, days } = calculateEmploymentDuration(employee.hireDate);
  const formattedHireDate = formatHireDate(employee.hireDate);
  
   const handleDelete = () => {
    onDelete(employee.id);
    setShowConfirmModal(false);
  };

  return (
    <>
      <div className="flex items-center justify-between bg-white p-6 rounded-lg shadow-md space-x-4">
        <div className="flex items-center">
          <img
            src={employee.user.photoUrl ? `${import.meta.env.VITE_BASE_URL}/${employee.user.photoUrl}` : 'https://randomuser.me/api/portraits/lego/1.jpg'}
            alt={`${employee.user.firstName} ${employee.user.lastName}`}
            className="w-24 h-24 rounded-lg mr-6"
          />
          <div>
            <h2 className="text-xl font-bold text-gray-800">
              {employee.user.firstName} {employee.user.lastName}{' '}
              <span className="text-sm text-gray-400">({employee.department.name})</span>
            </h2>
            <p className="text-sm text-gray-400 mt-4">Hired Date</p>
            <p className="text-sm text-gray-400">{formattedHireDate} ({years}y, {months}m, {days}d)</p>
          </div>
        </div>
        <div className="flex space-x-3">
          <Link to={`/employees/${employee.id}`} className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">View Details</Link>
          <button
            onClick={() => setShowConfirmModal(true)}
            className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600"
          >
            Delete
          </button>
        </div>
      </div>

      {showConfirmModal && (
        <ConfirmModal
          title="Delete Employee"
          message={`Are you sure you want to delete ${employee.user.firstName} ${employee.user.lastName}?`}
          onConfirm={handleDelete} 
          onCancel={() => setShowConfirmModal(false)} 
        />
      )}
    </>
  );
};

export default EmployeeCard;
