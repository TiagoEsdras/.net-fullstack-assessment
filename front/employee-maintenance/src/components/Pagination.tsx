import React from "react";
import { PaginationHeaders } from '../types/PaginationHeaders';

interface PaginationProps {
  pagination: PaginationHeaders | undefined;
  onNextPage: () => void;
  onPreviousPage: () => void;
}

const Pagination: React.FC<PaginationProps> = ({ pagination, onNextPage, onPreviousPage }) => {
  if (!pagination) return null;

  const { currentPage, totalPages, totalCount, pageSize } = pagination;

  return (
    <div className="flex items-center justify-between border-t border-gray-200 bg-white px-4 py-3 sm:px-6">  
      <div className="hidden sm:flex sm:flex-1 sm:items-center sm:justify-between">
        <div>
          <p className="text-sm text-gray-700">
            Showing
            <span className="font-medium"> {(currentPage - 1) * pageSize + 1} </span>
            to
            <span className="font-medium"> {Math.min(currentPage * pageSize, totalCount)} </span>
            of
            <span className="font-medium"> {totalCount} </span>
            results
          </p>
        </div>
        <div>
          <nav className="isolate inline-flex -space-x-px rounded-md shadow-xs" aria-label="Pagination">
            <button
              onClick={onPreviousPage}
              className="relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-gray-300 ring-inset hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
              disabled={currentPage === 1}
            >
              <span className="sr-only">Previous</span>
              <svg className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path
                  fillRule="evenodd"
                  d="M11.78 5.22a.75.75 0 0 1 0 1.06L8.06 10l3.72 3.72a.75.75 0 1 1-1.06 1.06l-4.25-4.25a.75.75 0 0 1 0-1.06l4.25-4.25a.75.75 0 0 1 1.06 0Z"
                  clipRule="evenodd"
                />
              </svg>
            </button>
            {[...Array(totalPages)].map((_, index) => {
              const pageNumber = index + 1;
              return (
                <button
                  key={pageNumber}
                  onClick={() => onNextPage()}
                  className={`relative inline-flex items-center px-4 py-2 text-sm font-semibold ${
                    pageNumber === currentPage
                      ? "bg-white text-gray-900 ring-1 ring-gray-200 ring-inset" 
                      : "text-gray-900 ring-1 bg-gray-100 ring-gray-300 ring-inset hover:bg-gray-350 focus:z-20"
                  }`}
                >
                  {pageNumber}
                </button>
              );
            })}
            <button
              onClick={onNextPage}
              className="relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-gray-300 ring-inset hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
              disabled={currentPage === totalPages}
            >
              <span className="sr-only">Next</span>
              <svg className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                <path
                  fillRule="evenodd"
                  d="M8.22 5.22a.75.75 0 0 1 1.06 0l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.75.75 0 0 1-1.06-1.06L11.94 10 8.22 6.28a.75.75 0 0 1 0-1.06Z"
                  clipRule="evenodd"
                />
              </svg>
            </button>
          </nav>
        </div>
      </div>
    </div>
  );
};

export default Pagination;
