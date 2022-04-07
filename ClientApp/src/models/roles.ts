export enum UserRole{
    Admin = 'Admin',
    User = 'User',
    Shacho = 'Shacho',
    Management = 'Management'
}

export class RoleCreate {
    Name: String
}

export class AssignUserToRolesModel {
  UserId: string;
  RolesToAssign: string[] = [];
}