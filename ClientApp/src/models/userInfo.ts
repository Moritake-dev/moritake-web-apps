import { stringify } from "querystring";

export class UserInfo {
  userId: string;
  userName: string;
  fullName: string;
  firstName: string;
  lastName: string;
  jobTitle: string;
  address: string;
  address_PostCode: string;
  sex: string;
  phone: string;
  role: string[];
}

export class UserProfileEdit {
  UserId: string;
  FirstName: string;
  LastName: string;
  Sex: string;
  Phone: string;
  JobTitle: string;
  Address: string;
  Address_PostCode: string;
}
export class UserCreate {
  UserName: string;
  Password: string;
  ConfirmPassword: string;
  Email: string;
  FirstName: string;
  LastName: string;
  JobTitle: string;
  Address: string;
  Sex: string;
  RoleName: string;
  IsEmployeeProfile: boolean = true;
  IsActiveEmployee: boolean = true;
}
