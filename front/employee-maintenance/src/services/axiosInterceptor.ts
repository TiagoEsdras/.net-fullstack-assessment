import axios from 'axios';
import { ErrorMessage } from '../types/ErrorMessage';

export const setupAxiosInterceptors = (setModalError: (message: string, errors: ErrorMessage[]) => void) => {
  axios.interceptors.response.use(
    (response) => {
      return response;
    },
    (error) => {
      if (error.response && error.response.data) {
        const { errorMessage, errors } = error.response.data;
        const parsedErrors: ErrorMessage[] = errors || [];
        setModalError(errorMessage, parsedErrors);
      } else {
        console.error('Unexpected error', error);
      }
      return Promise.reject(error);
    }
  );
};
