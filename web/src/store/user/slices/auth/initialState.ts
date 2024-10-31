
import { User } from '@/types/user';

export interface UserAuthState {
  clerkOpenUserProfile?: (props?: any) => void;
  clerkSession?: any;
  clerkSignIn?: (props?: any) => void;
  clerkSignOut?: any;
  clerkUser?: any;
  isLoaded?: boolean;
  isSignedIn?: boolean;
  user?: User;
}

export const initialAuthState: UserAuthState = {
  isSignedIn: localStorage.getItem('token') ? true : false,
};
