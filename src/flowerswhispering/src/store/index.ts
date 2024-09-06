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
  posts: Post[];
  comments: Comment[];
  plants: Plant[];
  feedbacks: Feedbacks[];
  announcements: Announcement[];
  userAvatar: string; // 新增的头像状态
}

interface Post {
  id: number;
  title: string;
  content: string;
}

interface Comment {
  id: number;
  postId: number;
  content: string;
}

interface Plant {
  id: number;
  name: string;
  category: string;
  description: string;
  image: string;
  approved: boolean;
}

interface Feedbacks {
  id: number;
  title: string;
  content: string;
}

interface Announcement {
  id: number;
  title: string;
  content: string;
  date: string;//
}

const store = createStore<State>({
  state: {
    currentUser: null,
    isAuthenticated: false,
    userAvatar: require('@/userprofile/images/user-avatar.jpg'), // 使用 require 语句
    // 模拟的用户数据
    users: [
      { username: 'user', email: 'user1@example.com', password: 'password', role: 'user' },
      { username: 'admin', email: 'admin@example.com', password: 'admin', role: 'admin' }, // 管理员用户
      { username: 'guest', email: 'guest@example.com', password: '', role: 'guest' }, // 游客用户
      { username: '111', email: 'mingyanghao6@gmail.com', password: '12345678', role: 'admin' }, // 管理员用户
    ],
    posts: [
      { id: 1, title: 'First Post', content: 'This is the content of the first post.' },
      { id: 2, title: 'Second Post', content: 'This is the content of the second post.' }
    ],
    comments: [
      { id: 1, postId: 101, content: '这是第一条评论' },
      { id: 2, postId: 102, content: '这是第二条评论' }
    ],

    plants: [
      { id: 1, name: '植物1', category: '花卉', description: '描述1', image: '/images/mudan.png', approved: true },
      { id: 2, name: '植物2', category: '树木', description: '描述2', image: '/images/rose.png', approved: false },
    ],

    feedbacks: [
      { id: 1, title: '反馈1', content: '这是第一条用户反馈，包含较多内容以供测试。' },
      { id: 2, title: '反馈2', content: '这是第二条用户反馈，包含较多内容以供测试。' }
    ],
    announcements: [
      { id: 1, title: '公告1', content: '这是第一条公告的内容。', date: '2024-01-01' },
      { id: 2, title: '公告2', content: '这是第二条公告的内容。', date: '2024-02-01' }
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
    },
    SET_AVATAR(state, avatar: string) {  // 新增的mutation
      state.userAvatar = avatar;
    },
    // 用户管理相关的 mutation
    ADD_USER(state, user) {
      state.users.push(user);
    },
    DELETE_USER(state, username) {
      state.users = state.users.filter(user => user.username !== username);
    },

    //userprofile
    UPDATE_USER(state, updatedUser) {
      const index = state.users.findIndex(user => user.username === updatedUser.username);
      if (index !== -1) {
        state.users.splice(index, 1, updatedUser); // 更新用户信息
        if (state.currentUser && state.currentUser.username === updatedUser.username) {
          state.currentUser = updatedUser; // 同时更新 currentUser
        }
      } else if (state.currentUser) {
        // 如果 currentUser 存在，但 username 发生了变化（例如用户修改了用户名）
        state.currentUser = updatedUser;
      }
    },
    SET_USER_AVATAR(state, avatar) {
      state.userAvatar = avatar;
    },
    // 帖子管理相关的 mutation
    /*
    ADD_POST(state, post) {
      state.posts.push(post);
    },
    
    UPDATE_POST(state, updatedPost) {
      const index = state.posts.findIndex(post => post.id === updatedPost.id);
      if (index !== -1) {
        state.posts.splice(index, 1, updatedPost);
      }
    },
  */
    DELETE_POST(state, postId) {
      state.posts = state.posts.filter(post => post.id !== postId);
    },
    DELETE_COMMENT(state, commentId) {
      state.comments = state.comments.filter(comment => comment.id !== commentId);
    },
    DELETE_ANNOUNCEMENT(state, announcementId) {
      state.announcements = state.announcements.filter(announcement => announcement.id !== announcementId);
    },
    ADD_ANNOUNCEMENT(state, announcement) {
      state.announcements.push(announcement);
    },
    APPROVE_PLANT(state, plantId) {
      const plant = state.plants.find(p => p.id === plantId);
      if (plant) {
        plant.approved = true;
      }
    },
    DELETE_PLANT(state, plantId) {
      state.plants = state.plants.filter(plant => plant.id !== plantId);
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
        const newUser = { username, email, password, role: 'user' };
        state.users.push(newUser);
        return { success: true };
      }
    },
    logout({ commit }) {
      commit('CLEAR_USER');
    },
    restoreState({ commit }) {
      commit('RESTORE_STATE');
    },
    // 管理用户操作
    addUser({ commit }, user) {
      commit('ADD_USER', user); // 确保这里传递的 user 是有效的
    },
    deleteUser({ commit }, username: string) {
      commit('DELETE_USER', username);
    },
    /*
      async deleteUser({ commit }, user) {
        try {
          // 发送 DELETE 请求到后端 API
          const response = await axios.post(`/api/deleteUsers`, user);

          if(response.data.success) {
            // 如果后端返回成功，提交 mutation 从 Vuex state 中删除用户
            commit('DELETE_USER', username);
          } else {
            // 处理后端返回的错误信息
            console.error('删除用户失败:', response.data.message);
            return { success: false, message: response.data.message };
          }
        } catch (error) {
          // 捕获并处理请求中的错误
          console.error('请求失败:', error.message);
          return { success: false, message: '删除用户时发生网络或服务器错误' };
        }
      }
     */
    updateUser({ commit }, updatedUser: User) {
      commit('UPDATE_USER', updatedUser);
    },
    setAvatar({ commit }, avatar) {
      commit('SET_USER_AVATAR', avatar);
    },
    /**
    async updateUser({ commit }, user) {
      try {
        const reponse = await axios.post
      } catch (error){

      }
    },
     */
    updateAdmin({ commit }, { username, role }) {
      commit('UPDATE_USER_ROLE', { username, role });
    },

    /*
    async updateAdmin({ commit }, { username, role }) {
      try {
        // 假设后端 API 的 URL 为 `/api/updateUserRole`
        const response = await axios.post('/api/updateUserRole', { username, role });

        if (response.data.success) {
          // 如果后端返回成功，提交 mutation 更新 Vuex 状态
          commit('UPDATE_USER_ROLE', { username, role });
        } else {
          // 处理后端返回的错误信息
          console.error('更新用户角色失败:', response.data.message);
          return { success: false, message: response.data.message };
        }
      } catch (error) {
        // 捕获并处理请求中的错误
        console.error('请求失败:', error.message);
        return { success: false, message: '更新用户角色时发生网络或服务器错误' };
      }
    },
    */


    // 管理帖子操作
    /*
    addPost({ commit }, post: Post) {
      post.id = Date.now(); // 简单生成唯一 ID
      commit('ADD_POST', post);
    },
    updatePost({ commit }, updatedPost: Post) {
      commit('UPDATE_POST', updatedPost);
    },*/
    deletePost({ commit }, postId: number) {
      commit('DELETE_POST', postId);
    },
    /*
    // 删除帖子
    async deletePost({ commit }, postId: number) {
      try {
        // 假设后端 API 的 URL 为 `/api/deletePost`
        const response = await axios.delete(`/api/deletePost/${postId}`);
  
        if (response.data.success) {
          // 如果后端返回成功，提交 mutation 从 Vuex state 中删除帖子
          commit('DELETE_POST', postId);
        } else {
          // 处理后端返回的错误信息
          console.error('删除帖子失败:', response.data.message);
          return { success: false, message: response.data.message };
        }
      } catch (error) {
        // 捕获并处理请求中的错误
        console.error('请求失败:', error.message);
        return { success: false, message: '删除帖子时发生网络或服务器错误' };
      }
    },
    */
    deleteComment({ commit }, commentId) {
      commit('DELETE_COMMENT', commentId);
    },
    /*
    // 删除评论
    async deleteComment({ commit }, commentId: number) {
      try {
        // 假设后端 API 的 URL 为 `/api/deleteComment`
        const response = await axios.delete(`/api/deleteComment/${commentId}`);
  
        if (response.data.success) {
          // 如果后端返回成功，提交 mutation 从 Vuex state 中删除评论
          commit('DELETE_COMMENT', commentId);
        } else {
          // 处理后端返回的错误信息
          console.error('删除评论失败:', response.data.message);
          return { success: false, message: response.data.message };
        }
      } catch (error) {
        // 捕获并处理请求中的错误
        console.error('请求失败:', error.message);
        return { success: false, message: '删除评论时发生网络或服务器错误' };
      }
    },
    */
    deleteAnnouncement({ commit }, announcementId) {
      commit('DELETE_ANNOUNCEMENT', announcementId);
    },
    /*
    // 删除公告
    async deleteAnnouncement({ commit }, announcementId: number) {
      try {
        // 假设后端 API 的 URL 为 `/api/deleteAnnouncement`
        const response = await axios.delete(`/api/deleteAnnouncement/${announcementId}`);
  
        if (response.data.success) {
          // 如果后端返回成功，提交 mutation 从 Vuex state 中删除公告
          commit('DELETE_ANNOUNCEMENT', announcementId);
        } else {
          // 处理后端返回的错误信息
          console.error('删除公告失败:', response.data.message);
          return { success: false, message: response.data.message };
        }
      } catch (error) {
        // 捕获并处理请求中的错误
        console.error('请求失败:', error.message);
        return { success: false, message: '删除公告时发生网络或服务器错误' };
      }
    },
    */
    addAnnouncement({ commit }, announcement) {
      commit('ADD_ANNOUNCEMENT', announcement);
    },
    /*
    // 添加公告
    async addAnnouncement({ commit }, announcement) {
      try {
        // 假设后端 API 的 URL 为 `/api/addAnnouncement`
        const response = await axios.post('/api/addAnnouncement', announcement);
  
        if (response.data.success) {
          // 如果后端返回成功，提交 mutation 将公告添加到 Vuex state 中
          commit('ADD_ANNOUNCEMENT', response.data.announcement);
          return { success: true };
        } else {
          // 处理后端返回的错误信息
          console.error('添加公告失败:', response.data.message);
          return { success: false, message: response.data.message };
        }
      } catch (error) {
        // 捕获并处理请求中的错误
        console.error('请求失败:', error.message);
        return { success: false, message: '添加公告时发生网络或服务器错误' };
      }
    },
     */
    deletePlant({ commit }, plantId) {
      commit('DELETE_PLANT', plantId);
    },
    /*
    // 删除植物
    async deletePlant({ commit }, plantId: number) {
      try {
        // 假设后端 API 的 URL 为 `/api/deletePlant`
        const response = await axios.delete(`/api/deletePlant/${plantId}`);
  
        if (response.data.success) {
          // 如果后端返回成功，提交 mutation 从 Vuex state 中删除植物
          commit('DELETE_PLANT', plantId);
        } else {
          // 处理后端返回的错误信息
          console.error('删除植物失败:', response.data.message);
          return { success: false, message: response.data.message };
        }
      } catch (error) {
        // 捕获并处理请求中的错误
        console.error('请求失败:', error.message);
        return { success: false, message: '删除植物时发生网络或服务器错误' };
      }
    },
  
    */
    approvePlant({ commit }, plantId) {
      commit('APPROVE_PLANT', plantId);
    }
  },


  getters: {
    allUsers: (state) => state.users,
    allPosts: (state) => state.posts,
    allComments: (state) => state.comments,
    allFeedbacks: state => state.feedbacks,
    allPlants: (state) => state.plants,
    allAnnouncements: (state) => state.announcements,
    currentUser: (state) => state.currentUser,
    isAuthenticated: (state) => state.isAuthenticated,
    isAdmin: (state) => state.currentUser?.role === 'admin',
    userAvatar: (state) => state.userAvatar  // 新增的getter
  }
});

export default store;