import { type components } from "api/base-reservation/api";

export type Branch = components['schemas']['ResponseBranchDto']
export type BranchRequest = components['schemas']['RequestBranchDto']

export type Province = components['schemas']['ResponseProvinceDto']
export type Canton = components['schemas']['ResponseCantonDto']
export type District = components['schemas']['ResponseDistrictDto']

export type LoginUserRequest = components['schemas']['RequestUserLoginDto']
export type UserTokenRefreshRequest = components['schemas']['TokenModel']
export type Authentication = components['schemas']['AuthenticationResult']

export type BaseReservationErrorDetails = components['schemas']['ErrorDetailsBaseReservation']