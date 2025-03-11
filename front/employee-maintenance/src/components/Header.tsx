import React from 'react';

const Header: React.FC = () => {
  return (
    <header className="absolute inset-x-0 top-0 z-50 lg:px-8">
      <nav className="flex items-center justify-between p-6 lg:px-8" aria-label="Global">
        <div className="flex lg:flex-1">
          <span className="text-2xl/6 font-semibold text-gray-900">Employee Maintenance</span>        
        </div>
        <div className="hidden lg:flex lg:flex-1 lg:justify-end">
          <a href="#" className="text-sm/6 font-semibold text-gray-900">
            New Employee <span aria-hidden="true" className="font-semibold">+</span>
          </a>
        </div>
      </nav>
    </header>
  );
};

export default Header;
