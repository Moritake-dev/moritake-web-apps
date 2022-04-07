export class ChangePassword {
  OldPassword: string;
  NewPassword: string;
  ConfirmPassword: string;
}

export class ResetPassword {
  UserId: string;
  NewPassword: string;
}
