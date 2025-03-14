import { BaseResponse } from "../types/BaseResponse";
import { EmployeeRequest } from "../types/CreateEmployeeRequest ";
import { DepartmentResponse } from "../types/DepartmentResponse";
import { EmployeeResponse } from "../types/EmployeeResponse";
import { PaginationHeaders } from "../types/PaginationHeaders";
import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL;

export const getAllEmployees = async (  pageNumber: number,  pageSize: number):
  Promise<{ data: EmployeeResponse[]; pagination: PaginationHeaders }> => {
  try {
    const response = await axios.get<BaseResponse<EmployeeResponse[]>>(`${API_BASE_URL}/Employee`, {
      params: {
        PageNumber: pageNumber,
        PageSize: pageSize,
      },
    });
   
    const paginationHeaders: PaginationHeaders = {
      currentPage: parseInt(response.headers['x-current-page']),
      pageSize: parseInt(response.headers['x-page-size']),
      totalCount: parseInt(response.headers['x-total-count']),
      totalPages: parseInt(response.headers['x-total-pages']),
    };
    
    return {
      data: response.data.data!,
      pagination: paginationHeaders,
    };
  } catch (error) {
    console.error('Error', error);
    throw error;
  }
};

export const getEmployeeById = async (employeeId: string): 
  Promise<{ data: EmployeeResponse }> => {
  try {
    const response = await axios.get<BaseResponse<EmployeeResponse>>(
      `${API_BASE_URL}/Employee/${employeeId}`
    );    
    return {
      data: response.data.data!,
    };
  } catch (error) {
    console.error('Error fetching employee:', error);
    throw error; 
  }
};

export const getAllDepartments  = async (  pageNumber: number,  pageSize: number):
  Promise<{ data: DepartmentResponse[]; pagination: PaginationHeaders }> => {
  try {
    const response = await axios.get<BaseResponse<DepartmentResponse[]>>(`${API_BASE_URL}/Department`, {
      params: {
        PageNumber: pageNumber,
        PageSize: pageSize,
      },
    });
   
    const paginationHeaders: PaginationHeaders = {
      currentPage: parseInt(response.headers['x-current-page']),
      pageSize: parseInt(response.headers['x-page-size']),
      totalCount: parseInt(response.headers['x-total-count']),
      totalPages: parseInt(response.headers['x-total-pages']),
    };
    
    return {
      data: response.data.data!,
      pagination: paginationHeaders,
    };
  } catch (error) {
    console.error('Error', error);
    throw error;
  }
};

export const updateEmployeeDepartment = async (  employeeId: string,   departmentName: string):
 Promise<EmployeeResponse> => {
  try {
    const response = await axios.put<BaseResponse<EmployeeResponse>>(
      `${API_BASE_URL}/Employee/${employeeId}`, 
      { departmentName }
    );
    return response.data.data!;
  } catch (error) {
    console.error("Error updating employee department:", error);
    throw error; 
  }
};

export const deleteEmployee = async (employeeId: string):
  Promise<boolean> => {
  try {
    const response = await axios.delete<BaseResponse<boolean>>(
      `${API_BASE_URL}/Employee/${employeeId}`
    );
    
    return response.data.data!;
  } catch (error) {
    console.error("Error deleting employee:", error);
    throw error;
  }
};

export const createEmployee = async (
  employeeData: EmployeeRequest
): Promise<EmployeeResponse> => {
  try {
    const response = await axios.post<BaseResponse<EmployeeResponse>>(
      `${API_BASE_URL}/Employee`,
      employeeData
    );
    
    return response.data.data!;
  } catch (error) {
    console.error("Error creating employee:", error);
    throw error;
  }
};
