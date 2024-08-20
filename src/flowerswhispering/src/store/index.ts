// src/store/index.ts
import { createStore } from 'vuex';
import axios from 'axios';

export default createStore({
  state: {
    currentUser: null,
    isAuthenticated: false,


    // 模拟的用户数据
    mockUsers: [
        { username: 'user1', email: 'user1@example.com', password: 'password1' },
        { username: 'user2', email: 'user2@example.com', password: 'password2' }
      ]

  },
  mutations: {
    SET_USER(state, user) {
      state.currentUser = user;
      state.isAuthenticated = true;
    },
    CLEAR_USER(state) {
      state.currentUser = null;
      state.isAuthenticated = false;
    }
  },
//   actions: {
//     async login({ commit }, credentials) {
//       try {
//         const response = await axios.post('/api/login', credentials);
//         if (response.data.success) {
//           commit('SET_USER', response.data.user);
//           return { success: true };
//         } else {
//           return { success: false, message: response.data.message || '登录失败' };
//         }
//       } catch (error) {
//         console.error('Error logging in:', error);
//         return { success: false, message: '登录时发生网络或服务器错误' };
//       }
//     },
//     async register({ commit }, userData) {
//       try {
//         const response = await axios.post('/api/register', userData);
//         if (response.data.success) {
//           commit('SET_USER', response.data.user);
//           return { success: true };
//         } else {
//           return { success: false, message: response.data.message || '注册失败' };
//         }
//       } catch (error) {
//         console.error('Error registering:', error);
//         return { success: false, message: '注册时发生网络或服务器错误' };
//       }
//     },
//     logout({ commit }) {
//       commit('CLEAR_USER');
//     }
//   },

  actions: {
    login({ commit, state }, { usernameOrEmail, password }) {
      // 检查模拟数据中的用户名/邮箱和密码
      const user = state.mockUsers.find(u => 
        (u.username === usernameOrEmail || u.email === usernameOrEmail) && u.password === password
      );
      if (user) {
        commit('SET_USER', user);
        return { success: true };
      } else {
        return { success: false, message: '用户名或密码错误' };
      }
    },
    register({ commit, state }, { username, email, password }) {
      // 检查用户名或邮箱是否已存在
      const userExists = state.mockUsers.some(u => 
        u.username === username || u.email === email
      );
      if (userExists) {
        return { success: false, message: '用户名或邮箱已被注册' };
      } else {
        // 创建新用户并添加到模拟数据中
        const newUser = { username, email, password };
        state.mockUsers.push(newUser);
        return { success: true };
      }
    },
    logout({ commit }) {
      commit('CLEAR_USER');
    }
  },


  getters: {
    isAuthenticated(state) {
      return state.isAuthenticated;
    },
    currentUser(state) {
      return state.currentUser;
    }
  }
});
