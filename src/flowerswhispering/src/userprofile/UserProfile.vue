<template>
  <div class="container">
    <div class="background"></div>
    <div class="content-wrapper">
      <div class="left-panel">
        <div class="user-info-container">
          <div class="avatar-container" @click="changeAvatar">
            <img src="./images/user-avatar.jpg" alt="用户头像" class="avatar" />
            <div class="avatar-overlay">更换头像</div>
          </div>
          <!-- 这里放置用户信息的代码 -->
          <div class="user-details">
            <!-- 用户信息表单部分 -->
            <div class="form-group">
              <label for="username">用户名：</label>
              <input type="text" id="username" v-model="user.username" disabled />
            </div>
            <div class="form-group">
              <label for="phone">手机号：</label>
              <input type="text" id="phone" v-model="user.phone" disabled />
            </div>
            <div class="form-group">
              <label for="email">邮&nbsp;&nbsp;&nbsp;&nbsp;箱：</label>
              <span>{{ user.email }}</span>
               <button class="edit-button" @click="openEmailModal">修改</button>
            </div>
            <div class="form-group gender-group">
              <label>性&nbsp;&nbsp;&nbsp;&nbsp;别：</label>
              <label><input type="radio" value="male" v-model="user.gender" /> 男</label>
              <label><input type="radio" value="female" v-model="user.gender" /> 女</label>
              <label><input type="radio" value="other" v-model="user.gender" /> 不愿透露</label>
            </div>
            <div class="form-group">
              <label for="bio">简&nbsp;&nbsp;&nbsp;&nbsp;介：</label>
              <textarea id="bio" v-model="user.bio" maxlength="60"></textarea>
            </div>
            <div class="submit-button-container">
              <button class="submit-button" @click="submitForm">提交</button>
            </div>
          </div>
        </div>
          <!-- 返回主页按钮 -->
        <div class="return-home-button-container">
          <button class="return-home-button" @click="goHome">返回主页</button>
        </div>
      </div>
      <div class="right-panel">
        <!-- 右侧框架的内容，例如其他信息 -->
      </div>
    </div>
      <!-- 模态框 -->
    <div class="modal-overlay" v-if="showEmailModal">
      <div class="modal-content">
        <h3>修改邮箱</h3>
        <div class="modal-form-group">
          <label>当前邮箱：</label>
          <span>{{ user.email }}</span>
        </div>
        <div class="modal-form-group">
          <label for="newEmail">新邮箱：</label>
          <input type="email" id="newEmail" v-model="emailForm.newEmail" />
        </div>
        <div class="modal-form-group verification-group">
          <label for="verificationCode">验证码：</label>
          <input type="text" id="verificationCode" v-model="emailForm.verificationCode" />
          <button class="verification-button" @click="sendVerificationCode">获取验证码</button>
        </div>
        <div class="modal-button-group">
          <button class="confirm-button" @click="confirmEmailChange">确认修改</button>
          <button class="cancel-button" @click="closeEmailModal">取消</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      user: {
        avatar: "./images/user-avatar.jpg",
        username: 'shark',
        phone: '绑定手机号',
        email: '1761503586@qq.com',
        gender: '',
        bio: ''
      },
      showEmailModal: false,
        emailForm: {
        newEmail: '',
        verificationCode: ''
      }
    };
  },
  methods: {
    changeAvatar() {
      // 触发更换头像的逻辑
      alert('更换头像的函数触发');
      // 这里你可以调用文件上传对话框或者其他逻辑
      // 例如： this.$refs.avatarInput.click();
    },
    submitForm() {
      // 提交表单的逻辑
      alert('提交表单的函数触发');
      // 在这里可以将表单数据发送到服务器
    },
    openEmailModal() {
      // 打开修改邮箱的模态框
      this.showEmailModal = true;
    },
    closeEmailModal() {
      // 关闭修改邮箱的模态框
      this.showEmailModal = false;
    },
    sendVerificationCode() {
      // 发送验证码
      alert('发送验证码');
    },
    confirmEmailChange() {
      // 确认修改邮箱
      alert('确认修改邮箱');
    },
    goHome() {
      this.$router.push('/home'); // 跳转到home.vue
    }
  }
};
</script>

<style scoped>
.avatar-container {
  position: relative;
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
  cursor: pointer; /* 鼠标悬停时显示为手形 */
}

.avatar {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  object-fit: cover;
  transition: all 0.3s ease; /* 过渡效果 */
}

.avatar-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100px;
  height: 100px;
  border-radius: 50%;
  background-color: rgba(0, 0, 0, 0.6); /* 半透明黑色背景 */
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
  filter: brightness(0.7); /* 使头像变暗 */
}

.avatar-container:hover .avatar-overlay {
  opacity: 1; /* 显示“更换头像”提示 */
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

.background {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: url('./images/background.png') no-repeat center center;
  background-size: cover;
  z-index: 1; /* 背景层 */
}

.content-wrapper {
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 80%;
  height: 100%;
  margin: 0 auto; /* 确保内容区域居中 */
  z-index: 2; /* 内容层在背景层之上 */
}

.left-panel,
.right-panel {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: flex-start; /* 将内容向上对齐 */
  background-color: rgba(255, 255, 255, 0.7); /* 半透明背景 */
  padding: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2); /* 为框架添加阴影效果 */
  height: 80%; /* 高度设置为页面的80% */
}

.user-details {
  width: 100%;
  max-width: 400px;
}

.form-group {
  display: flex;
  align-items: center; /* 让label和textarea在垂直方向上对齐 */
  margin-bottom: 20px;
  font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
  font-size: 16px;
}

textarea {
  width: 100%;
  height: 60px; /* 设置固定高度，确保与其他输入框对齐 */
  padding: 10px;
  border-radius: 5px;
  border: 1px solid #ccc;
  font-size: 14px;
  resize: none; /* 禁用调整大小的控件 */
  background-color: rgba(255, 255, 255, 0.7); /* 与背景一致的透明度 */
}

/* “修改”按钮的样式 */
.edit-button {
  padding: 5px 15px;
  background-color: #28a745; /* 绿色背景 */
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 14px;
  transition: background-color 0.3s ease, transform 0.2s ease;
  margin-left: 10px;
}

.edit-button:hover {
  background-color: #218838; /* 悬停时稍微加深的绿色 */
  transform: scale(1.05); /* 鼠标悬停时放大效果 */
}

.edit-button:active {
  background-color: #1e7e34; /* 点击时更深的绿色 */
  transform: scale(1); /* 点击时回到原始大小 */
}

/* “提交”按钮的样式 */
.submit-button-container {
  display: flex;
  justify-content: center;
}

.submit-button {
  width: 50%;
  padding: 10px;
  background-color: #5cb85c; /* 提交按钮的绿色背景 */
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 18px; /* 增大提交按钮的字体 */
  font-weight: bold;
  margin-top: 20px;
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.submit-button:hover {
  background-color: #4cae4c; /* 悬停时稍微加深的绿色 */
  transform: scale(1.05); /* 鼠标悬停时放大效果 */
}

.submit-button:active {
  background-color: #3d8b3d; /* 点击时更深的绿色 */
  transform: scale(1); /* 点击时回到原始大小 */
}
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 100;
}

.modal-content {
  width: 400px;
  background-color: white;
  border-radius: 10px;
  padding: 30px;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.3);
}

.modal-content h3 {
  margin-bottom: 20px;
  text-align: center;
  color: #333;
}

.modal-form-group {
  display: flex;
  align-items: center;
  margin-bottom: 15px;
}

.modal-form-group label {
  width: 100px;
  text-align: right;
  margin-right: 10px;
  font-weight: bold;
  color: #333;
}

.modal-form-group input {
  flex: 1;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
  outline: none;
  font-size: 14px;
}

.verification-group {
  position: relative;
}

/* 验证码按钮样式 */
.verification-button {
  padding: 6px 12px;
  background-color: #e67e22;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 12px;
  transition: background-color 0.3s ease;
}

.verification-button:hover {
  background-color: #d35400;
}

/* 确认修改和取消按钮样式 */
.confirm-button,
.cancel-button {
  width: 45%;
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
  margin-left:30px;
}

.cancel-button:hover {
  background-color: #c82333;
}
/* 返回主页按钮样式 */
.return-home-button-container {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}

.return-home-button {
  padding: 10px 20px;
  background-color: #007bff; /* 蓝色背景 */
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.return-home-button:hover {
  background-color: #0056b3;
  transform: scale(1.05); /* 鼠标悬停时的放大效果 */
}

.return-home-button:active {
  background-color: #004085;
  transform: scale(1); /* 点击时恢复原状 */
}
</style>
