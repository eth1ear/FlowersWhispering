<template>
  <div class="body">
    <div id="videoContainer">
        <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
          <source src="../assets/video/background.mp4" type="video/mp4">
        </video>
      </div>
    <div class="main-box">
      <div :class="['container', 'container-register', { 'is-txl': isLogin }]">
        <form>
          <h2 class="title">注册</h2>
          <div class="form__icons">
            <img class="form__icon" src="./images/wechat.png" alt="微信登录">
            <img class="form__icon" src="./images/alipay.png" alt="支付宝登录">
            <img class="form__icon" src="./images/QQ.png" alt="QQ登录">
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.username" class="form__input" type="text" placeholder="请输入用户名" required />
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.email" @input="handleInput('email')" class="form__input" type="email" placeholder="请输入邮箱" required />
            <span class="validation-feedback" v-if="registerForm.touched.email" :class="{ 'error': !isEmailValid, 'success': isEmailValid }">
              {{ isEmailValid ? '✓ 邮箱格式正确' : '✗ 请输入有效的邮箱地址' }}
            </span>
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.password" @input="handleInput('password')" class="form__input" type="password" placeholder="请输入密码" required />
            <span class="validation-feedback" v-if="registerForm.touched.password" :class="{ 'error': !isPasswordValid, 'success': isPasswordValid }">
              {{ isPasswordValid ? '✓ 密码长度有效' : '✗ 密码长度必须在8到20个字符之间' }}
            </span>
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.confirmPassword" @input="handleInput('confirmPassword')" class="form__input" type="password" placeholder="请再次输入密码" required />
            <span class="validation-feedback" v-if="registerForm.touched.confirmPassword" :class="{ 'error': !arePasswordsMatching, 'success': arePasswordsMatching }">
            {{ arePasswordsMatching ? '✓ 密码匹配' : '✗ 两次输入的密码不一致' }}
            </span>
          </div>
          <div class="form__button" @click="performRegister">立即注册</div>
        </form>
      </div>
      <div :class="['container', 'container-login', { 'is-txl is-z200': isLogin }]">
        <form>
          <h2 class="title">登录</h2>
          <div class="form__icons">
            <img class="form__icon" src="./images/wechat.png" alt="微信登录">
            <img class="form__icon" src="./images/alipay.png" alt="支付宝登录">
            <img class="form__icon" src="./images/QQ.png" alt="QQ登录">
          </div>
          <span class="text">或使用用户名登录</span>
          <input v-model="loginForm.usernameOrEmail" class="form__input" type="text" placeholder="用户名/邮箱" required />
          <input v-model="loginForm.password" class="form__input" type="password" placeholder="请输入密码" required />
          <div class="form__button" @click="performLogin">立即登录</div>        
        </form>
      </div>
      <div :class="['switch', { 'login': isLogin }]">
        <div class="switch__circle"></div>
        <div class="switch__circle switch__circle_top"></div>
        <div class="switch__container">
          <h2>{{ isLogin ? '您好 !' : '欢迎回来 !' }}</h2>
          <p>
            {{
              isLogin
                  ? '如果您还没有账号，请点击下方立即注册按钮进行账号注册'
                  : '如果您已经注册过账号，请点击下方立即登录按钮进行登录'
            }}
          </p>
          <div class="form__button" @click="isLogin = !isLogin">
            {{ isLogin ? '立即注册' : '立即登录' }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { defineComponent } from 'vue';
import { mapActions, mapGetters } from 'vuex';
export default {
  name: 'Login',
  data() {
    return {
      isLogin: true,
      loginForm: {
        usernameOrEmail: '',
        password: '',
      },
      registerForm: {
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
        touched: {
          email: false,
          password: false,
          confirmPassword: false,
        }
      },
    }
  },
  computed: {
    ...mapGetters(['isAuthenticated', 'currentUser']),
    isEmailValid() {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return this.registerForm.email ? emailRegex.test(this.registerForm.email) : false;
    },
    isPasswordValid() {
      return this.registerForm.password ? this.registerForm.password.length >= 8 && this.registerForm.password.length <= 20 : false;
    },
    arePasswordsMatching() {
      return this.registerForm.password && this.registerForm.confirmPassword ? this.registerForm.password === this.registerForm.confirmPassword : false;
    }
  },
  methods: {
    ...mapActions(['login', 'logout', 'register']),
    handleInput(field) {
      this.registerForm.touched[field] = true;
    },
    async performLogin() {
      const { usernameOrEmail, password } = this.loginForm;
      // 检查用户名或邮箱和密码是否为空
      if (!usernameOrEmail.trim() || !password.trim()) {
        alert('用户名/邮箱和密码不能为空！');
        return;
      }
      // 登录逻辑
      const response = await this.login(this.loginForm);
      if (response.success) {
        this.$router.push('/home');
      } else {
        alert(response.message);
      }
    },
    async performRegister() {
      const { username, email, password, confirmPassword } = this.registerForm;

      // 检查注册信息是否为空
      if (!username.trim() || !email.trim() || !password.trim() || !confirmPassword.trim()) {
        alert('所有字段都必须填写，并且不能为空！');
        return;
      }

      // 验证邮箱格式
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(email)) {
        alert('请输入有效的邮箱地址！');
        return;
      }

      // 验证密码长度
      if (password.length < 8 || password.length > 20) {
        alert('密码长度必须在8到20个字符之间！');
        return;
      }

      // 检查两次输入的密码是否一致
      if (password !== confirmPassword) {
        alert('两次输入的密码不一致');
        return;
      }
      const response = await this.register(this.registerForm);
      if (!response.success) {
        alert(response.message); // 显示错误消息
      } else {
        this.$router.push('/home'); // 或其他逻辑
        alert('注册成功!请登录');
    }
    },
    handleInput(field) {
      this.registerForm.touched[field] = true;
    }
  },
}
</script>


<style lang="scss" scoped>
.body {
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  font-family: "Montserrat", sans-serif;
  font-size: 12px;
  color: #a0a5a8;
}
#videoContainer {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: -1; /* 让视频在背景层 */
    overflow: hidden;
  }
  
.fullscreenVideo {
  width: 100%;
  height: 100%;
  object-fit: cover;
}
.main-box {
  position: relative;
  width: 1000px;
  min-width: 1000px;
  min-height: 600px;
  height: 600px;
  padding: 25px;
  background-color: #ecf0f3;
  box-shadow: 1px 1px 100px 10PX #ecf0f3;
  border-radius: 12px;
  overflow: hidden;

  .container {
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 0;
    width: 600px;
    height: 100%;
    padding: 25px;
    background-color: #ecf0f3;
    transition: all 1.25s;

    form {
      display: flex;
      justify-content: center;
      align-items: center;
      flex-direction: column;
      width: 100%;
      height: 100%;
      color: #a0a5a8;

      .form__icon {
        object-fit: contain;
        width: 30px;
        margin: 0 5px;
        opacity: .5;
        transition: .15s;

        &:hover {
          opacity: 1;
          transition: .15s;
          cursor: pointer;

        }
      }

      .title {
        font-size: 34px;
        font-weight: 700;
        line-height: 3;
        color: #181818;
      }

      .text {
        margin-top: 30px;
        margin-bottom: 12px;
      }

      .form__input {
        width: 250px;
        height: 40px;
        margin: 4px 0;
        padding-left: 25px;
        font-size: 13px;
        letter-spacing: 0.15px;
        border: none;
        outline: none;
        font-family: 'Montserrat', sans-serif;
        background-color: #ecf0f3;
        transition: 0.25s ease;
        border-radius: 8px;
        box-shadow: inset 2px 2px 4px #d1d9e6, inset -2px -2px 4px #f9f9f9;

        &::placeholder {
          color: #a0a5a8;
        }
      }
    }
  }

  .container-register {
    z-index: 100;
    left: calc(100% - 650px);
    
  }

  .container-login {
    left: calc(100% - 650px);
    z-index: 0;
  }

  .is-txl {
    left: 0;
    transition: 1.25s;
    transform-origin: right;
  }

  .is-z200 {
    z-index: 200;
    transition: 1.25s;
  }

  .switch {
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 0;
    left: 0;
    height: 100%;
    width: 400px;
    padding: 0px;
    z-index: 200;
    transition: 1.25s;
    background-color: #ecf0f3;
    overflow: hidden;
    box-shadow: 4px 4px 10px #d1d9e6, -4px -4px 10px #f9f9f9;
    color: #a0a5a8;

    .switch__circle {
      position: absolute;
      width: 500px;
      height: 500px;
      border-radius: 50%;
      background-color: #ecf0f3;
      box-shadow: inset 8px 8px 12px #d1d9e6, inset -8px -8px 12px #f9f9f9;
      bottom: -60%;
      left: -60%;
      transition: 1.25s;
    }

    .switch__circle_top {
      top: -30%;
      left: 60%;
      width: 300px;
      height: 300px;
    }

    .switch__container {
      display: flex;
      justify-content: center;
      align-items: center;
      flex-direction: column;
      position: absolute;
      width: 400px;
      padding: 50px 55px;
      transition: 1.25s;

      h2 {
        font-size: 34px;
        font-weight: 700;
        line-height: 3;
        color: #181818;
      }

      p {
        font-size: 14px;
        letter-spacing: 0.25px;
        text-align: center;
        line-height: 1.6;
      }
    }
  }

  .login {
    left: calc(100% - 400px);

    .switch__circle {
      left: 0;
    }
  }

  .input-wrapper {
    display: flex;
    flex-direction: column;
    align-items: start;
  }

  .validation-feedback {
    margin-top: 2px;
    margin-left:20px;
    font-size: 12px;
  }

  .success {
    color: green;
  }

  .error {
    color: red;
  }

  .form__button {
    width: 180px;
    height: 50px;
    border-radius: 25px;
    margin-top: 50px;
    text-align: center;
    line-height: 50px;
    font-size: 14px;
    letter-spacing: 2px;
    background-color: #4b70e2;
    color: #f9f9f9;
    cursor: pointer;
    box-shadow: 8px 8px 16px #d1d9e6, -8px -8px 16px #f9f9f9;

    &:hover {
      box-shadow: 2px 2px 3px 0 rgba(255, 255, 255, 50%),
      -2px -2px 3px 0 rgba(116, 125, 136, 50%),
      inset -2px -2px 3px 0 rgba(255, 255, 255, 20%),
      inset 2px 2px 3px 0 rgba(0, 0, 0, 30%);
    }
  }
}
</style>

