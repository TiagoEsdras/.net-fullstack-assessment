import React, { useEffect } from 'react';
import { ErrorMessage } from '../types/ErrorMessage';

interface ErrorModalProps {
  message: string;
  errors: ErrorMessage[];
  onClose: () => void;
}

const ErrorModal: React.FC<ErrorModalProps> = ({ message, errors, onClose }) => {
  useEffect(() => {
    document.body.classList.add('overflow-hidden');
    return () => {
      document.body.classList.remove('overflow-hidden');
    };
  }, []);

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full mx-4">
        <h2 className="text-xl font-semibold text-gray-800">{message}</h2>
        <ul className="list-disc pl-5 text-gray-600 mt-4 mb-6">
          {errors.map((error, index) => (
            <li key={index}>
              <strong>{error.code}: </strong> {error.message}
            </li>
          ))}
        </ul>
        <div className="flex justify-end">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
          >
            Close
          </button>
        </div>
      </div>
    </div>
  );
};

export default ErrorModal;
