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
        <img :src="userAvatar" alt="User Avatar" @click="handleUserAvatarClick">
        <div class="user-info-list">
          <div v-if="currentUser.role !== 'guest'">
            <p>用户名: {{ currentUser.username }}</p>
            <p>邮箱: {{ currentUser.email }}</p>
            <p>角色: {{ currentUser.role }}</p>
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
            <img :src="userAvatar" alt="用户头像" class="avatar" />
            <div class="avatar-overlay">更换头像</div>
          </div>
          <div class="user-details">
            <!-- 用户信息表单部分 -->
           <div class="form-group">
             <label for="username">用户名：</label>
                <div class="editable-field-container">
                 <span v-if="!editing">{{ currentUser.username }}</span>
                 <input v-if="editing" type="text" v-model="userTemp.username" class="fixed-input" />
                </div>
           </div>

           <div class="form-group">
             <label for="email">邮&nbsp;&nbsp;&nbsp;&nbsp;箱：</label>
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
            <div class="form-group gender-group">
              <label class="gender-label">性&nbsp;&nbsp;&nbsp;&nbsp;别：</label>
              <div class="gender-display" v-if="!editing">{{ currentUser.gender === 'male' ? '男' : currentUser.gender === 'female' ? '女' : '不愿透露' }}</div>
              <div class="gender-options" v-if="editing">
                <label><input type="radio" value="male" v-model="userTemp.gender" /> 男</label>
                <label><input type="radio" value="female" v-model="userTemp.gender" /> 女</label>
                <label><input type="radio" value="other" v-model="userTemp.gender" /> 不愿透露</label>
              </div>
            </div>

            <div class="form-group">
              <label for="bio">简&nbsp;&nbsp;&nbsp;&nbsp;介：</label>
              <div class="editable-field-container">
                <span v-if="!editing">{{ currentUser.bio ? currentUser.bio : '这家伙很懒，什么都没留下' }}</span>
                <textarea v-if="editing" id="bio" v-model="userTemp.bio" maxlength="60" class="fixed-input"></textarea>
              </div>
            </div>
          </div>
           <!-- 编辑个人信息按钮 -->
        <div class="edit-button-container">
          <button v-if="!editing" class="edit-button" @click="editUserInfo">编辑个人信息</button>
          <div v-if="editing" class="edit-actions">
            <button 
              class="confirm-button" 
              @click="confirmEdit" 
              :disabled="emailError"
            >确认</button>
            <button class="cancel-button" @click="cancelEdit">取消</button>
          </div>
          
        </div>
          <div class="logout-button-container">
         <button class="logout-button" @click="performLogout">退出登录</button>
         </div>
         </div>
       
        <!-- 返回主页按钮 -->
      
      </div>
      <div class="right-panel">
        <!-- 右侧框架的内容，例如其他信息 -->
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
import { mapState, mapGetters, mapActions} from 'vuex';
export default {
  computed: {
    ...mapState({
      currentUser: state => state.currentUser , // 从Vuex store中获取 currentUser
      userAvatar: state => state.userAvatar // 确保这里绑定了全局的 userAvatar
    }),
    ...mapGetters(['userAvatar'])  // 使用全局的userAvatar
  },
  data() {
    return {
      userTemp: {},
      editing: false,
      showAvatarSelection: false,
      selectedAvatar: null,
      emailError: null,  // 用于存储邮箱格式错误信息
      avatarOptions: [
        require('@/userprofile/images/avatar1.jpg'),
        require('@/userprofile/images/avatar2.jpg'),
        require('@/userprofile/images/avatar3.jpg'),
        require('@/userprofile/images/avatar4.jpg'),
        require('@/userprofile/images/avatar5.jpg'),
        require('@/userprofile/images/avatar6.jpg')
      ]
    };
  },
  methods: {
     ...mapActions(['logout']),
    editUserInfo() {
      this.userTemp = { ...this.currentUser };  // 创建 currentUser 的临时副本
      this.editing = true;
    },
    validateEmail() {
      const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailPattern.test(this.userTemp.email)) {
        this.emailError = '请输入有效的邮箱地址';
      } else {
        this.emailError = null;
      }
    },
    confirmEdit() {
        //alert("Confirm button clicked"); // 调试：确认按钮点击后是否进入方法
      if (!this.emailError) {
        //alert("No email error, proceeding with update"); // 调试：确认邮箱格式正确，准备提交更新
        this.$store.commit('UPDATE_USER', this.userTemp);
        this.editing = false;
        // alert("Edit successful!"); // 调试：确认提交成功后显示成功信息
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
      this.emailError = '';  // 取消编辑时，清除错误信息
    },
    openAvatarSelection() {
      this.showAvatarSelection = true;
      this.selectedAvatar = this.userAvatar;  // 使用全局的userAvatar
    },
    selectAvatar(avatar) {
      this.selectedAvatar = avatar;
    },
    confirmAvatar() {
      this.$store.dispatch('setAvatar', this.selectedAvatar);  // 使用action更新全局头像
      this.showAvatarSelection = false;
    },
    cancelAvatarSelection() {
      this.showAvatarSelection = false;
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

.left-panel,
.right-panel {
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

.user-details {
  width: 100%;
  max-width: 400px;
}

.form-group {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  margin-bottom: 20px;
  font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
  font-size: 16px;
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
  position: absolute;
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

</style>
