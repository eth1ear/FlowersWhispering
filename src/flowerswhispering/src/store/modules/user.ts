import { Module } from 'vuex';
import apiClient from '@/axios/apiClient'; // 确保你有一个 apiClient 文件来处理 Axios 请求

interface UserState {
  userInfo: Record<string, any> | null;
  isAuthenticated: boolean;
}

const userModule: Module<UserState, any> = {
  state: {
    userInfo: null,
    isAuthenticated: false,
  },
  mutations: {
    SET_USER_INFO(state, payload) {
      state.userInfo = payload;
      state.isAuthenticated = true;
    },
    CLEAR_USER_INFO(state) {
      state.userInfo = null;
      state.isAuthenticated = false;
    },
  },
  actions: {
    async login({ commit }, { usernameOrEmail, password }) {
      try {
        const response = await apiClient.post('/api/UserAccount/login', { username: usernameOrEmail, password });
        if (response.data) {
          commit('SET_USER_INFO', response.data);
          return true;
        }
        return false;
      } catch (error:any) {
        throw new Error(error.response?.data.message || '登录失败');
      }
    },
    async register({ commit }, { username, email, password }) {
      const languagePreference = 'zh-CN'
      try {
        const response = await apiClient.post('/api/UserAccount/register', { username, email, password ,languagePreference});
        if (response.data.success) {
          commit('SET_USER_INFO', response.data);
          return true;
        } else {
          return false;
        }
      } catch (error:any) {
        throw new Error(error.response?.data.message || '注册失败');
      }
    },
    async guestLogin({ commit }) {
      // 创建默认的游客用户信息
      const guestUserInfo = {
        userId: 'guest', 
        username: '游客',
        email: 'guest@example.com',
        languagePreference: 'zh-CN',
        userState: 'guest', // 状态为游客
        userRole: 'guest',  // 角色为游客
      };
      commit('SET_USER_INFO', guestUserInfo);
      return true;
    },
    logout({ commit }) {
      commit('CLEAR_USER_INFO');
    }
  },
  getters: {
    getUserInfo(state) {
      return state.userInfo;
    },
    isAuthenticated(state) {
      return state.isAuthenticated;
    },
    isAdmin(state) {
      return state.userInfo?.userRole === 'admin';
    },
    isGuest(state) {
      return state.userInfo?.userState === 'guest';
    }
  },
};

export default userModule;
