export interface EmployeeRequest {
  hireDate: string;
  department: {
    departmentName: string;
  };
  user: {
    firstName: string;
    lastName: string;
    phone: string;
    photoBase64: string;
    address: {
      street: string;
      city: string;
      state: string;
      zipCode: string;
    };
  };
}
