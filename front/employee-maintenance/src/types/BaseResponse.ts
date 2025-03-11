import { ErrorMessage } from "./ErrorMessage";

export interface BaseResponse<T> {
  data?: T; 
  message: string;
  errors: ErrorMessage[];
}