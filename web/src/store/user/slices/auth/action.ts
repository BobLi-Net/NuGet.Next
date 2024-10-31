import { StateCreator } from 'zustand/vanilla';

import { UserStore } from '../../store';

export interface UserAuthAction {
  /**
   * universal logout method
   */
  logout: () => Promise<void>;
  /**
   * universal login method
   */
  openLogin: () => Promise<void>;
  openUserProfile: () => Promise<void>;
}

export const createAuthSlice: StateCreator<
  UserStore,
  [["zustand/devtools",never]],
  [],
  UserAuthAction
> = (set, get) => ({
  logout: async () => {
    localStorage.removeItem('token');
  },
  openLogin: async () => {
    // open login modal
    
  },

  openUserProfile: async () => {
    // open user profile modal
  },
});
