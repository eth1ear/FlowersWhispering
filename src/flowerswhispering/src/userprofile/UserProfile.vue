<template>
  <div class="container" v-if="currentUser">
      <header class="header">
  <div class="logo">Flowers Whispering</div>
  <div class="nav-user-container">
    <nav class="nav-links">
       <nav v-if="currentUser.role === 'admin'">
         <button @click="goToAdminPanel" class="nav-button">管理</button>
      </nav>
      <button @click="goHome" class="nav-button">首页</button>
      <button @click="goToCommunity" class="nav-button">社区</button>
      <button @click="goToCatalog" class="nav-button">图鉴</button>
    </nav>
    <div class="user-info">
      <div class="user-avatar-wrapper">
        <img :src="currentUser.avatar" alt="User Avatar" @click="handleUserAvatarClick">
        <div class="user-info-list">
          <div v-if="currentUser.role !== 'guest'">
            <p>用户名: {{ currentUser.username }}</p>
            <p>邮箱: {{ currentUser.email }}</p>
            <p>角色: {{ currentUser.userRole }}</p>
          </div>
          <div v-else>
            <p class="login-prompt">点击登录</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</header>
  <!-- 视频背景 -->
  <video autoplay muted loop class="background-video">
    <source src="@/assets/video/background.mp4" type="video/mp4">
  </video>

    <div class="content-wrapper">
      <div class="left-panel">
        <div class="user-info-container">
          <div class="avatar-container" @click="openAvatarSelection">
            <img :src="currentUser.avatar" alt="用户头像" class="avatar" />
            <div class="avatar-overlay">更换头像</div>
            <!-- 隐藏的文件输入框 -->
            <input 
              type="file" 
              ref="fileInput" 
              @change="handleFileUpload" 
              accept="image/*" 
              style="display: none;" 
            />
          </div>
          <div class="user-details">
            <!-- 用户信息表单部分 -->
            <div class="form-group">
             <label for="username">邮&nbsp;箱：</label>
               <div class="editable-field-container">
                <span v-if="!editing">{{ currentUser.username }}</span>
                <input 
                v-if="editing" 
                type="username" 
                v-model="userTemp.username" 
                @input="validateUsername" 
                class="fixed-input" 
                />
                <!-- 错误信息显示在输入框下方 -->
                <p v-if="usernameError" class="error-message">{{ usernameError }}</p> <!-- 报错信息显示在输入框下方 -->
              </div>
            </div>

           <div class="form-group">
            <label for="role">身&nbsp;份：</label>
            <div class="editable-field-container">
              <span>{{ currentUser.userRole=='user'?'普通用户':'管理员' }}</span> <!-- 只显示，不允许编辑 -->
            </div>
          </div>
           <div class="form-group">
              <label for="gender">性&nbsp;别：</label>
              <div class="editable-field-container">
                <!-- 编辑状态下显示当前性别的文本 -->
                <span v-if="!editing">{{ currentUser.gender}}</span>
                <!-- 编辑状态下显示下拉菜单，默认选中当前性别 -->
                <select v-if="editing" v-model="userTemp.gender" class="fixed-input">
                  <option value="男">男</option>
                  <option value="女">女</option>
                  <option value="不愿透露">不愿透露</option>
                </select>
              </div>
            </div>
            
            <div class="form-group">
              <label for="language">语言偏好：</label>
              <div class="editable-field-container">
                <!-- 编辑状态下显示当前性别的文本 -->
                <span v-if="!editing">{{ currentUser.languagePreference=="zh-CN"?'中文':'英文'}}</span>
                <!-- 编辑状态下显示下拉菜单，默认选中当前性别 -->
                <select v-if="editing" v-model="userTemp.languagePreference" class="fixed-input">
                  <option value="zh-CN">中文</option>
                  <option value="EN">英文</option>
                </select>
              </div>
            </div>

           <div class="form-group">
             <label for="email">邮&nbsp;箱：</label>
               <div class="editable-field-container">
                <span v-if="!editing">{{ currentUser.email }}</span>
                <input 
                v-if="editing" 
                type="email" 
                v-model="userTemp.email" 
                @input="validateEmail" 
                class="fixed-input" 
                />
                <!-- 错误信息显示在输入框下方 -->
                <p v-if="emailError" class="error-message">{{ emailError }}</p> <!-- 报错信息显示在输入框下方 -->
              </div>
            </div>
            <div class="form-group">
              <label for="bio">简&nbsp;介：</label>
              <div class="editable-field-container">
                <span v-if="!editing">{{ currentUser.bio}}</span>
                <textarea v-if="editing" id="bio" v-model="userTemp.bio" class="fixed-input"></textarea>
              </div>
            </div>
          </div>
          <div class="form-group">
            <label for="registrationDate">注册时间：</label>
            <div class="editable-field-container">
              <span>{{ currentUser.registrationTime }}</span> <!-- 只显示，不允许编辑 -->
            </div>
          </div>

           <!-- 编辑个人信息按钮 -->
        <div class="edit-button-container">
          <button v-if="!editing" class="edit-button" @click="editUserInfo">编辑个人信息</button>
          
          <div v-if="editing" class="edit-actions">
            <button 
              class="confirm-button" 
              @click="confirmEdit" 
            >确认</button>
            <button class="cancel-button" @click="cancelEdit">取消</button>
            
          </div>
          
        </div>
        <button class="change-password-button" @click="showChangePasswordDialog = true">修改账户密码</button>
          <div class="logout-button-container">
         <button class="logout-button" @click="performLogout">退出登录</button>
         </div>
         </div>
        <!-- 返回主页按钮 -->
      



      </div>
      <div class="right-panel">
        <!-- 右侧框架的内容，例如其他信息 -->
     <!-- 使用 v-for 遍历从后端获取的帖子列表 -->
  <!-- 帖子列表 -->
  <div 
    v-for="post in posts" 
    :key="post.article_id" 
    class="post"
    @click="openPostDialog(post)"
  >
    <h3 class="post-title">{{ post.article_title }}</h3>
    <p class="post-content">{{ post.article_content }}</p>
    <p class="post-meta">
      发布日期: {{ post.published_date }} 
      <span v-if="post.article_type === 'announcement'"> | 类型: 公告</span>
      <span v-else> | 类型: 文章</span>
    </p>
    <!-- 确保删除按钮使用 @click.stop 来防止冒泡 -->
    <button class="delete-button" @click.stop="confirmDelete(post)">删除</button>
  </div>

  <!-- 删除确认弹框 -->
  <div v-if="showConfirmDialog" class="confirm-dialog">
    <div class="confirm-dialog-content">
      <p>确定要删除该帖子吗？</p>
      <button @click="deletePost(selectedPost)">确认</button>
      <button @click="cancelDelete">取消</button>
    </div>
  </div>
 
      </div>
    </div>

     
   <!-- 帖子详情弹框 -->
  <div v-if="showPostDialog" class="post-dialog">
    <div class="post-dialog-content">
      <div class="post-dialog-header">
        <h3>{{ selectedPost.article_title }}</h3>
        <!-- 使用 Unicode 或者字体图标实现叉叉 -->
        <button @click="closePostDialog" class="close-button">&times;</button>
      </div>
      <div class="post-dialog-body">
        <p>{{ selectedPost.article_content }}</p>
      </div>
      <div class="post-dialog-footer">
        <p>评论区：</p>
        <ul>
          <li v-for="comment in comments" :key="comment.id">{{ comment.text }}</li>
        </ul>
      </div>
    </div>
  </div>


  <!-- 修改密码弹窗 -->
  <div v-if="showChangePasswordDialog" class="password-dialog-overlay">
    <div class="password-dialog">
      <h3>修改账户密码</h3>
      <div class="form-group">
        <label for="old-password">旧密码：</label>
        <input type="password" id="old-password" v-model="oldPassword" />
      </div>
      <div class="form-group">
        <label for="new-password">新密码：</label>
        <input type="password" id="new-password" v-model="newPassword" />
      </div>
      <div class="form-group">
        <label for="confirm-password">确认新密码：</label>
        <input type="password" id="confirm-password" v-model="confirmPassword" />
      </div>
      <!-- 错误信息 -->
      <p v-if="passwordError" class="error-message">{{ passwordError }}</p>
      <div class="dialog-actions">
        <button class="confirm-button" @click="confirmChangePassword">确认</button>
        <button class="cancel-button" @click="cancelChangePassword">取消</button>
      </div>
    </div>
  </div>
    <!-- 头像选择弹出框 -->
    <div v-if="showAvatarSelection" class="avatar-selection-overlay">
      <div class="avatar-selection-container">
        <h3>选择头像</h3>
        <div class="avatar-grid">
          <div 
            v-for="(avatar, index) in avatarOptions" 
            :key="index" 
            :class="{'avatar-option': true, 'selected': selectedAvatar === avatar}"
            @click="selectAvatar(avatar)">
            <img :src="avatar" class="avatar-image" />
            <div v-if="selectedAvatar === avatar" class="checkmark">✔</div>
          </div>
        </div>
        <div class="edit-actions">
          <button class="confirm-button" @click="confirmAvatar">确认</button>
          <button class="cancel-button" @click="cancelAvatarSelection">取消</button>
        </div>
      </div>
    </div>
     <!-- 底部备案号 -->
     <footer class="footer">
      <p class="left"><a href="contact.html">联系我们</a></p>
     <div class="center">
    <span>© 2024 Flowers Whisperin&nbsp;&nbsp;&nbsp;&nbsp;</span>
    <a href="https://beian.miit.gov.cn/" target="_blank">豫ICP备2024087175号-1</a>
  </div>
      <div class="right"></div>
    </footer>
  </div> 
  <div v-else>
    加载中...
  </div>
</template>


<script>
import { mapState, mapGetters, mapActions } from 'vuex';

export default {
  computed: {
    ...mapGetters({
      currentUser: 'getUserInfo', // 获取当前用户信息
      isAdmin: 'isAdmin', // 检查是否为管理员
      userAvatar: 'userAvatar', // 使用 getter 获取头像
      isAuthenticated: 'isAuthenticated' // 检查认证状态
    })
  },
  data() {
    return {
      posts: [{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },
        {
          article_id: 2,
          article_title: "示例公告",
          article_content: "这是一个公告类型的帖子，展示如何处理公告。",
          publisher_id: 2,
          published_date: "2024-09-03",
          article_type: "announcement"
        },{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },{
          article_id: 1,
          article_title: "示例帖子1",
          article_content: "这是第一个示例帖子的内容,展示数据库",
          publisher_id: 1,
          published_date: "2024-09-04",
          article_type: "article"
        },
      ], // 帖子数据
      showChangePasswordDialog: false, // 控制弹出框显示
      passwordError: '',
      oldPassword: '',
      newPassword: '',
      confirmPassword: '',
      showPostDialog: false, // 控制帖子详情弹框是否显示
      showConfirmDialog: false, // 删除确认弹框
      selectedPost: null, // 当前选中的帖子
      comments: [] ,// 评论列表


      userTemp: {},
      editing: false,
      emailError: null,
      usernameError: null,
    };
  },
  methods: {
    ...mapActions({
      logout: 'logout',
      setAvatar: 'setAvatar',
      updateUserInfo: 'updateUserInfo',
    }),
    editUserInfo() {
      this.userTemp = { ...this.currentUser };
      this.editing = true;
    },
    // 验证用户名是否为邮箱格式
    validateUsername() {
      const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (emailPattern.test(this.userTemp.username)) {
        this.usernameError = '用户名不能是邮箱格式';
      } else {
        this.usernameError = null;
      }
    },
    // 验证邮箱格式
    validateEmail() {
      const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailPattern.test(this.userTemp.email)) {
        this.emailError = '请输入有效的邮箱地址';
      } else {
        this.emailError = null;
      }
    },
    
    async confirmEdit() {
      if (!this.emailError && !this.usernameError && this.editing) {
        try {
          // 调用 Vuex 中的 action，发送更新的用户信息到后端
          await this.updateUserInfo({username:this.userTemp.username, 
                                    email:this.userTemp.email, 
                                    password:this.userTemp.password ,
                                    languagePreference:this.userTemp.languagePreference,
                                    bio:this.userTemp.bio,
                                    avatar:this.userTemp.avatar,
                                    gender:this.userTemp.gender});
          this.editing = false;
        } catch (error) {
          console.error('更新失败:', error);
          alert('更新用户信息失败，请稍后重试。');
        }
      }
    },
    toggleUserMenu() {
      this.showUserMenu = !this.showUserMenu;
    },
    handleUserAvatarClick() {
      if (this.currentUser.role === 'guest') {
        this.$router.push('/login'); // 如果是guest用户，点击跳转到登录页面
      } else {
        this.goToUserProfile(); // 否则跳转到个人主页
      }
    },
    cancelEdit() {
      this.editing = false;  // 恢复到原来的状态，不保存修改
      this.emailError = null;  // 取消编辑时，清除错误信息
    },
    openAvatarSelection() {
      this.$refs.fileInput.click(); // 点击头像区域时，打开文件选择器
    },
    handleFileUpload(event) {
      const file = event.target.files[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
          this.selectedAvatar = e.target.result; // 将Base64数据存储在selectedAvatar
          this.updateUserAvatar(); // 调用方法更新头像
        };
        reader.readAsDataURL(file); // 将文件读取为Base64格式
      }
    },
    async updateUserAvatar() {
      try {
        await this.updateUserInfo({username:this.currentUser.username, 
                                    email:this.currentUser.email, 
                                    password:this.currentUser.password ,
                                    languagePreference:this.currentUser.languagePreference,
                                    bio:this.currentUser.bio,
                                    avatar:this.selectedAvatar,
                                    gender:this.currentUser.gender});
      } catch (error) {
        console.error('更新头像失败:', error);
        alert('更新头像失败，请稍后再试。');
      }
    },
    goToCatalog() {
      this.$router.push('/catalog');
    },
    goToCommunity() {
      this.$router.push('/community');;
    },
    goToAdminPanel() {
      this.$router.push('/adminpanel');
    },
    goHome() {
      this.$router.push('/home');
    },
    performLogout() {
      this.logout(); 
      this.$router.push('/login'); // 跳转到登录页面
    },
     goToUserProfile() {
      this.$router.push('/userprofile');;
    },

    //刪除帖子部分
 // 点击删除按钮时，显示确认弹框
    confirmDelete(post) {
      this.selectedPost = post; // 保存要删除的帖子
      this.showConfirmDialog = true; // 显示确认弹框
    },
    // 确认删除操作
    deletePost(post) {
      // 实际删除操作应通过API调用来删除帖子
      this.posts = this.posts.filter(p => p.article_id !== post.article_id); // 从列表中移除帖子
      this.showConfirmDialog = false; // 关闭弹框
      this.selectedPost = null; // 重置选择的帖子
    },
    // 取消删除操作
    cancelDelete() {
      this.showConfirmDialog = false; // 关闭弹框
      this.selectedPost = null; // 重置选择的帖子
    },
     // 打开帖子详情弹框
    openPostDialog(post) {
      this.selectedPost = post;
      this.comments = [ // 模拟评论数据
        { id: 1, text: "这是第一条评论！" },
        { id: 2, text: "非常有趣的帖子，期待更多！" },
        { id: 2, text: "非常有趣的帖子，期待更多！" },
        { id: 2, text: "非常有趣的帖子，期待更多！" },
        { id: 2, text: "非常有趣的帖子，期待更多！" },
        { id: 2, text: "非常有趣的帖子，期待更多！" },
        { id: 2, text: "非常有趣的帖子，期待更多！" },
      ];
      this.showPostDialog = true;
    },
    // 关闭帖子详情弹框
    closePostDialog() {
      this.showPostDialog = false;
      this.selectedPost = null;
    },
    async confirmChangePassword() {
      // 清除之前的错误提示
      this.passwordError = '';

      // 确保新密码和确认密码相同
      if (this.newPassword !== this.confirmPassword) {
        this.passwordError = '新密码与确认密码不匹配';
        return;
      }
      // 验证旧密码是否正确
      if (this.oldPassword !== this.currentUser.password) {
        this.passwordError = '旧密码不正确';
        return;
      }

      // 确保新密码与旧密码不同
      if (this.newPassword === this.oldPassword) {
        this.passwordError = '新密码不能与旧密码相同';
        return;
      }

      
      // 确保新密码长度在 8 到 20 位之间
      if (this.newPassword.length < 8 || this.newPassword.length > 20) {
        this.passwordError = '新密码长度应为 8 到 20 位';
        return;
      }
      // 向后端发送请求更新密码
      try {
        await this.updateUserInfo({username:this.currentUser.username, 
                                    email:this.currentUser.email, 
                                    password:this.newPassword ,
                                    languagePreference:this.currentUser.languagePreference,
                                    bio:this.currentUser.bio,
                                    avatar:this.currentUser.avatar,
                                    gender:this.currentUser.gender});
        alert('密码修改成功');
        this.cancelChangePassword(); // 关闭弹框并清空数据
      } catch (error) {
        console.error('更新密码失败:', error);
        alert('更新密码失败，请稍后再试');
      }
    },
    cancelChangePassword() {
      this.showChangePasswordDialog = false; // 关闭弹窗
      this.oldPassword = '';
      this.newPassword = '';
      this.confirmPassword = '';
      this.passwordError = '';
    },
  }
};
</script>



<style scoped>

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
  background-color: rgba(70, 180, 118, 0.8); /* 使用rgba设置透明度，0.8表示80%的不透明度 */
  color: white;
  z-index: 10; /* 提高 z-index，确保 header 在其他内容上层 */
  position: relative;
}
.avatar-container {
  position: relative;
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
  cursor: pointer;
}

.avatar {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  object-fit: cover;
  transition: all 0.3s ease;
}

.avatar-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100px;
  height: 100px;
  border-radius: 50%;
  background-color: rgba(0, 0, 0, 0.6);
  color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  opacity: 0;
  transition: opacity 0.3s ease;
  font-size: 14px;
  text-align: center;
}

.avatar-container:hover .avatar {
  filter: brightness(0.7);
}

.avatar-container:hover .avatar-overlay {
  opacity: 1;
}

.user-info-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.container {
  position: relative;
  width: 100%;
  height: 100vh;
  overflow: hidden;
}

.background-video {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  z-index: -1; 
}


.content-wrapper {
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 80%;
  height: 100%;
  margin: 0 auto;
  z-index: 2;
}

.left-panel{
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: flex-start;
  background-color: rgba(255, 255, 255, 0.7);
  padding: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  height: 80%;
  z-index: 2;
  margin-bottom: 80px; /* 与底部之间的距离 */
}

.right-panel {
  flex: 1;
  display: flex;
  background-color: rgba(211, 211, 211, 0.7);
  flex-direction: column;   /* 将帖子垂直排列 */
  align-items: flex-start;
  justify-content: flex-start; /* 让内容从顶部开始对齐 */
  padding: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  height: 80%;              /* 固定高度 */
  overflow-y: auto;          /* 当内容溢出时显示滚动条 */
  margin-bottom: 80px;
  z-index: 2;
  width: 100%;
}

.user-details {
  width: 100%;
  max-width: 400px;
}

.form-group {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  margin-bottom: 20px;
  font-family: '宋体','ZhiMangXing-Regular', sans-serif;
  font-size: 16px;
  max-width:290px;
}
.form-group label {
  min-width: 80px; /* 设置一个最小宽度确保标签在同一行 */
  margin-right: 10px; /* 标签和输入框之间的间距 */
  text-align: right;
}
.gender-group {
  display: flex;
  align-items: center;
}

.gender-label {
  text-align: left;
}

.gender-display {
  flex: 2;
  text-align: center; /* 性别文本居中显示 */
}

.gender-options {
  justify-content: center;
  gap: 10px;
}


.editable-field-container {
  display: flex;
  justify-content: center;
  flex-direction: column; /* 让输入框和反馈信息垂直排列 */
  width: 100%;
}

.editable-field input,
.editable-field span,
textarea {
  width: 100%;
  padding: 5px;
  text-align: center;
  border-radius: 5px;
  border: 1px solid #ccc;
  background-color: rgba(255, 255, 255, 0.7);
}

input[type="text"], textarea {
  width: 100%; /* 设置宽度为父容器的100% */
  max-width: 300px; /* 设置最大宽度，确保不会变大 */
  height: 30px; /* 固定高度 */
  box-sizing: border-box; /* 包括内边距和边框在内计算宽度 */
  padding: 5px;
  font-size: 14px;
}

textarea {
  height: 100px; /* 设置多行文本框的固定高度 */
  resize: none; /* 禁用手动调整大小 */
}
.confirm-button,
.cancel-button {
  width: 120px; /* 调整宽度为长方形 */
  padding: 10px;
  border: none;
  border-radius: 25px;
  cursor: pointer;
  font-size: 14px;
  font-weight: bold;
  transition: background-color 0.3s, transform 0.2s ease;
}

.confirm-button {
  background-color: #28a745;
  color: white;
}

.confirm-button:hover {
  background-color: #218838;
}

.cancel-button {
  background-color: #dc3545;
  color: white;
}

.cancel-button:hover {
  background-color: #c82333;
}

.edit-actions {
  display: flex;
  justify-content: center; /* 居中对齐容器内的按钮 */
  gap: 20px; /* 增加按钮之间的间距 */
  width: 260px; /* 容器的宽度设置为按钮的总宽度加上间距 */
}

.logout-button-container {
  display: flex;
  justify-content: center;
  margin-top: auto; /* 让按钮靠下 */
  margin-bottom: 20px;
}

.logout-button {
  background-color: #ff4d4d;
  width: 136px; /* 调整宽度为长方形 */
  height:35px;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size:16px;
  transition: background-color 0.3s ease, transform 0.2s ease;
  margin-top:20px;
}

.logout-button:hover {
  background-color: #ff1a1a;
  transform: scale(1.05);
}

.logout-button:active {
  background-color: #cc0000;
  transform: scale(1);
}

/* 新增的样式 */
.edit-button-container {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}

.edit-button {
  background-color: #ffc107;
  color: #fff;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.edit-button:hover {
  background-color: #e0a800;
  transform: scale(1.05);
}

.edit-actions {
  display: flex;
  justify-content: center; /* 水平居中对齐 */
  width: 100%;
  margin-top: 10px;
}

/* 头像选择弹出框相关样式 */
.avatar-selection-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.avatar-selection-container {
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  width: 400px;
  text-align: center;
}

.avatar-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
  margin-top: 20px;
}

.avatar-option {
  position: relative;
  cursor: pointer;
}

.avatar-image {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  border: 2px solid transparent;
  transition: all 0.3s ease;
}

.avatar-option.selected .avatar-image {
  border-color: #007bff;
  filter: brightness(0.7);
}

.checkmark {
  position: absolute;
  top: 5px;
  right: 5px;
  font-size: 18px;
  color: white;
  background-color: #007bff;
  border-radius: 50%;
  width: 20px;
  height: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.error-message {
  color: red;
  font-size: 12px;
  margin-top: 2px;
  margin-left: 5px;  /* 保持与输入框对齐 */
}

.validation-feedback {
  font-size: 12px;
  color: green; /* 默认成功信息颜色 */
  display: block; /* 确保反馈信息占据整行 */
}

.validation-feedback.error {
  color: red; /* 错误信息颜色 */
}

.validation-feedback.success {
  color: green; /* 成功信息颜色 */
}


.user-info {
    display:table-column;
    align-items: center;
    gap: 10px;
    position: relative;
}

.user-info img {
    width: 40px;
    height: 40px;
    border-radius: 50%;
    cursor: pointer;
    transition: transform 0.3s ease;
}

.user-info img:hover {
    transform: scale(1.1);
}
.user-info-list {
  z-index: 2; /* 提高用户信息列表的层级，确保它显示在 header 之上 */
  position: absolute;
  left: -200px!important;
  top: 50px;
  right: 0;
  background-color: white; /* 为弹出的内容添加背景色，避免透明度导致内容不清晰 */
  padding: 10px;
  border-radius: 5px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); /* 添加阴影效果，确保弹出层次感 */
}
.username {
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    font-size: 28px;
    font-weight: bold;
}

.logo {
  font-family: 'Caveat-VariableFont', 'ZhiMangXing-Regular', sans-serif;
  font-size: 28px;
  font-weight: bold;
}

.nav-user-container {
  display: flex;
  align-items: center;
  gap: 20px; /* 按钮与头像之间的间距 */
}

.nav-links {
  display: flex;
  gap: 20px; /* 按钮之间的间距 */
}

.nav-button {
  background: none;
  border: none;
  color: white;
  font-size: 18px;
  cursor: pointer;
  transition: color 0.3s;
}

.nav-button:hover {
  color: #ffcc00; /* 鼠标悬停时变色 */
}

.user-avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  cursor: pointer;
}
.login-prompt {
  display: flex;
  justify-content: center;  /* 水平居中对齐 */
  align-items: center;      /* 垂直居中对齐 */
  height: 100%;             /* 让内容充满父容器的高度 */
  color: blue;
  cursor: pointer;
  text-align: center;
}

.login-prompt:hover {
  text-decoration: underline;
}

.footer {
  display: flex;
  justify-content: space-between;  /* 左中右均匀分布 */
  align-items: center;             /* 垂直居中对齐 */
  background-color: rgba(70, 180, 118, 0.8);
  color: white;
  position:relative;
  width: 100%;                     /* 跨满页面 */
  z-index: 10;
}

.footer .left {
  text-align: left;
  margin-left: 10px;               /* 左边距 */
}

.footer .center {
  text-align: center;
  flex: 1;                         /* 中间内容居中，并占据剩余空间 */
}

.footer .right {
  text-align: right;
  margin-right: 10px;              /* 右边距 */
}

.footer a {
  color: white;
  text-decoration: none;
}

.footer a:hover {
  color: rgb(24, 212, 209); /* 悬停下划线效果 */
}


/*帖子*/
/* 帖子悬停变大效果 */
.post {
  background-color: white;
  padding: 15px;
  margin-bottom: 5px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-height: 150px;
  box-sizing: border-box;
  position: relative;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  cursor: pointer;
  z-index:3;
}

.post:hover {
  transform: scale(1.05);
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
}
.post-title {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 10px;
}

.post-content {
  font-size: 14px;
  color: #333;
  margin-bottom: 10px;
  overflow: hidden;          /* 如果内容过多，隐藏超出的部分 */
  white-space: nowrap;       /* 防止内容换行 */
  text-overflow: ellipsis;   /* 超出部分用省略号显示 */
}

.post-meta {
  font-size: 12px;
  color: #666;
  text-align: right;
}
/* 删除按钮样式 */
.delete-button {
  position: absolute;
  right: 20px;
  top: 25%;
  transform: translateY(-50%);
  background-color: red;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
}

.delete-button:hover {
  background-color: darkred;
}

/* 删除确认弹框样式 */
.confirm-dialog {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
  z-index: 1000;
}

.confirm-dialog-content {
  text-align: center;
}

.confirm-dialog-content button {
  margin: 10px;
  padding: 5px 15px;
  cursor: pointer;
}

.confirm-dialog-content button {
  margin: 10px;
  padding: 10px 20px;
  cursor: pointer;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  transition: background-color 0.3s ease, color 0.3s ease, box-shadow 0.3s ease;
}

.confirm-dialog-content button {
  background-color: #4CAF50; /* 确认按钮的绿色 */
  color: white;
}

.confirm-dialog-content button:hover {
  background-color: #45a049;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.confirm-dialog-content button:last-child {
  background-color: #f44336; /* 取消按钮的红色 */
  color: white;
}

.confirm-dialog-content button:last-child:hover {
  background-color: #e53935;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

/* 帖子详情弹框样式 */
.post-dialog {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
  z-index: 100;
  width: 60%;
  max-width: 800px;
  max-height: 80vh;
  overflow: hidden; /* 不显示外部滚动条 */
}

.post-dialog-content {
  display: flex;
  flex-direction: column;
}

.post-dialog-header {
  display: flex;
  justify-content: space-between; /* 标题和按钮在同一行 */
  align-items: center;
  font-size: 24px;
  margin-bottom: 20px;
}

.post-dialog-header h3 {
  font-size: 20px;
  color: #00aaff;
}

.close-button {
  padding: 5px 10px;
  font-size: 40px; /* 设置字体大小，让叉叉明显 */
  background-color: transparent; /* 去掉背景色 */
  color: #007bff; /* 设置叉叉的颜色 */
  border: none;
  cursor: pointer;
  transition: color 0.3s ease;
}

.close-button:hover {
  color: #0056b3; /* 悬停时变成深蓝色 */
}

/* 中间内容部分：固定高度，支持滚动 */
.post-dialog-body {
  flex: 1;
  max-height: 250px; /* 固定内容区域高度 */
  overflow-y: auto; /* 内容超出时显示滚动条 */
  margin-bottom: 20px;
}

/* 评论区：固定高度，支持滚动 */
.post-dialog-footer {
  font-size: 14px;
  max-height: 150px; /* 固定评论区域高度 */
  overflow-y: auto; /* 评论超出时显示滚动条 */
  background-color: #f1f1f1;
  padding: 10px;
  border-radius: 5px;
}
/* 修改个人信息和修改密码按钮 */
.edit-button, .change-password-button {
  padding: 10px 15px;
  font-size: 16px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin: 10px 0;
}

.edit-button {
  background-color: orange;
  color: white;
}

.change-password-button {
  background-color: #007bff; /* 蓝色按钮 */
  color: white;
}

.change-password-button:hover {
  background-color: #0056b3; /* 悬停时颜色变深 */
}
/* 修改密码弹窗背景 */
.password-dialog-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5); /* 半透明背景 */
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 3; /* 确保弹窗显示在最上层 */
}

/* 修改密码弹窗 */
.password-dialog {
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  width:300px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
}

.password-dialog h3 {
  text-align: center;
  margin-bottom: 20px;
  color: #007bff;
}

.password-dialog .form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
}

.password-dialog .form-group label {
  margin-bottom: 5px;
}

.password-dialog .form-group input {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 5px;
  width:100%;
}

.dialog-actions {
  display: flex;
  justify-content: space-between;
}

.confirm-button {
  background-color: #28a745;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 14px;
}

.confirm-button:hover {
  background-color: #218838;
}

.cancel-button {
  background-color: #dc3545;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 14px;
}

.cancel-button:hover {
  background-color: #c82333;
}

</style>
