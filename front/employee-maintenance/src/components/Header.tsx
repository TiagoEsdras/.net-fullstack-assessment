import React from 'react';
import { Link, useLocation } from 'react-router-dom';

const Header: React.FC = () => {
  const location = useLocation();
  const showNewEmployeeButton = location.pathname === "/";

  return (
    <header className="absolute inset-x-0 top-0 z-50 lg:px-8">
      <nav className="flex items-center justify-between p-6 lg:px-8" aria-label="Global">
        <div className="flex lg:flex-1">
          <span className="text-2xl/6 font-semibold text-gray-900">Employee Maintenance</span>
        </div>
        {showNewEmployeeButton && (
          <div className="hidden lg:flex lg:flex-1 lg:justify-end">
            <Link to={`/create`} className="text-sm/6 font-semibold text-gray-900">New Employee <span aria-hidden="true" className="font-semibold">+</span></Link>
          </div>
        )}
      </nav>
    </header>
  );
};

export default Header;
