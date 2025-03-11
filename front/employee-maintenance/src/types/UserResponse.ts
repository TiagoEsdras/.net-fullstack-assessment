import { AddressResponse } from "./AddressResponse";

export interface UserResponse {
  id: string;
  firstName: string;
  lastName: string;
  phone: string;
  photoUrl: string;
  address: AddressResponse;
  addressId: string;
}