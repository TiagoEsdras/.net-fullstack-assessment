import { DepartmentResponse } from "./DepartmentResponse";
import { UserResponse } from "./UserResponse";

export interface EmployeeResponse {
  id: string;
  hireDate: string;
  department: DepartmentResponse;
  departmentId: string;
  user: UserResponse;
  userId: string;
}