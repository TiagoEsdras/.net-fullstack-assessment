import { BaseResponse } from "../types/BaseResponse";
import { EmployeeResponse } from "../types/EmployeeResponse";
import { PaginationHeaders } from "../types/PaginationHeaders";
import axios from 'axios';

const API_BASE_URL = process.env.REACT_APP_API_URL;

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