import { Role } from '../../enums/auth/role';

export interface IAuthStatus{
    role: Role,
    primarySid: number,
    unique_name: string
}