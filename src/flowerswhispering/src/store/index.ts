import { createStore } from 'vuex';
import axios from 'axios';

interface User {
  username: string;
  email: string;
  password: string;
  role: string;
}

interface State {
  isAuthenticated: boolean;
  currentUser: User | null;
  users: User[];
}

const store = createStore<State>({
  state: {
    currentUser: null,
    isAuthenticated: false,


    // 模拟的用户数据
    users: [
      {username: 'user', email: 'user1@example.com', password: 'password', role: 'user' },
      { username: 'admin', email: 'admin@example.com', password: 'admin', role: 'admin' }, // 管理员用户
      { username: 'guest', email: 'guest@example.com', password: '', role: 'guest' }, // 游客用户
      { username: '111', email: 'mingyanghao6@gmail.com', password: '12345678', role: 'admin' }, // 管理员用户
    ]

  },
  mutations: {
    SET_USER(state, user) {
      state.currentUser = user;
      state.isAuthenticated = true;
      localStorage.setItem('currentUser', JSON.stringify(user));
      localStorage.setItem('isAuthenticated', 'true');
    },
    CLEAR_USER(state) {
      state.currentUser = null;
      state.isAuthenticated = false;
      localStorage.removeItem('currentUser');
      localStorage.removeItem('isAuthenticated');
    },
    RESTORE_STATE(state) {
      const storedUser = localStorage.getItem('currentUser');
      const storedAuth = localStorage.getItem('isAuthenticated');
      if (storedUser && storedAuth === 'true') {
        state.currentUser = JSON.parse(storedUser);
        state.isAuthenticated = true;
      }
    }
  },

  // actions: {
  //   async login({ commit }, credentials) {
  //     try {
  //       const response = await axios.post('/api/login', credentials);
  //       if (response.data.success) {
  //         commit('SET_USER', response.data.user);
  //         return { success: true };
  //       } else {
  //         return { success: false, message: response.data.message || '登录失败' };
  //       }
  //     } catch (error) {
  //       console.error('Error logging in:', error);
  //       return { success: false, message: '登录时发生网络或服务器错误' };
  //     }
  //   },
  //   async register({ commit }, userData) {
  //     try {
  //       const response = await axios.post('/api/register', userData);
  //       if (response.data.success) {
  //         commit('SET_USER', response.data.user);
  //         return { success: true };
  //       } else {
  //         return { success: false, message: response.data.message || '注册失败' };
  //       }
  //     } catch (error) {
  //       console.error('Error registering:', error);
  //       return { success: false, message: '注册时发生网络或服务器错误' };
  //     }
  //   },
  //   async guestLogin({ commit }) {
  //     try {
  //       const response = await axios.post('/api/guest-login');
  //       if (response.data.success) {
  //         commit('setAuthenticated', true);
  //         commit('setCurrentUser', response.data.user);
  //         return { success: true };
  //       } else {
  //         return { success: false, message: response.data.message };
  //       }
  //     } catch (error) {
  //       return { success: false, message: '游客登录失败，请稍后再试。' };
  //     }
  //   },
  //   logout({ commit }) {
  //     commit('CLEAR_USER');
  //   }
  // },

  actions: {
    login({ commit, state }, { usernameOrEmail, password }) {
      // 检查模拟数据中的用户名/邮箱和密码
      const user = state.users.find(u => 
        (u.username === usernameOrEmail || u.email === usernameOrEmail) && u.password === password
      );
      if (user) {
        commit('SET_USER', user);
        return { success: true };
      } else {
        return { success: false, message: '用户名或密码错误' };
      }
    },
    guestLogin({ state, commit }) {
      const guestUser = state.users.find(user => user.username === 'guest');
      commit('SET_USER', guestUser);
      return { success: true };
    },
    register({ commit, state }, { username, email, password }) {
      // 检查用户名或邮箱是否已存在
      const userExists = state.users.some(u => 
        u.username === username || u.email === email
      );
      if (userExists) {
        return { success: false, message: '用户名或邮箱已被注册' };
      } else {
        // 创建新用户并添加到模拟数据中
        const newUser = { username, email, password ,role: 'user'};
        state.users.push(newUser);
        return { success: true };
      }
    },
    logout({ commit }) {
      commit('CLEAR_USER');
    },
    restoreState({ commit }) {
      commit('RESTORE_STATE');
    }
  },


  getters: {
    isAuthenticated: state => state.isAuthenticated,
    currentUser: state => state.currentUser,
    isAdmin: state => state.currentUser?.role === 'admin',
  }
});

export default store;