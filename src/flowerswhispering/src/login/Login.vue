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
           <!-- 游客登录按钮 -->
           <div class="form__button guest-button" @click="performGuestLogin">游客登录</div>
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

<script lang="ts">
import { defineComponent, ref ,computed} from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'Login',
  setup() {
    const store = useStore();
    const router = useRouter();

    const isLogin = ref(true);

    const loginForm = ref({
      usernameOrEmail: '',
      password: ''
    });

    const registerForm = ref({
      username: '',
      email: '',
      password: '',
      confirmPassword: '',
      touched: {
        email: false,
        password: false,
        confirmPassword: false
      }
    });

    const isEmailValid = computed(() => {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return emailRegex.test(registerForm.value.email);
    });

    const isPasswordValid = computed(() => {
      const password = registerForm.value.password;
      return password.length >= 8 && password.length <= 20;
    });

    const arePasswordsMatching = computed(() => {
      return registerForm.value.password === registerForm.value.confirmPassword;
    });

    const handleInput = (field: keyof typeof registerForm.value.touched) => {
      registerForm.value.touched[field] = true;
    };

    const performLogin = async () => {
      const { usernameOrEmail, password } = loginForm.value;

      if (!usernameOrEmail.trim() || !password.trim()) {
        alert('用户名/邮箱和密码不能为空！');
        return;
      }

      try {
        const response = await store.dispatch('login', { usernameOrEmail, password });
        if (response) {
          router.push('/home');
        }
      } catch (error:any) {
        alert(error.message || '登录失败');
      }
    };

    const performGuestLogin = async () => {
      try {
        const response = await store.dispatch('guestLogin');
        if (response) {
          router.push('/home');
        } else {
          alert('游客登录失败，请稍后再试。');
        }
      } catch (error) {
        alert('游客登录失败，请稍后再试。');
      }
    };

    const performRegister = async () => {
      const { username, email, password, confirmPassword } = registerForm.value;

      if (!username.trim() || !email.trim() || !password.trim() || !confirmPassword.trim()) {
        alert('所有字段都必须填写，并且不能为空！');
        return;
      }

      if (!isEmailValid.value) {
        alert('请输入有效的邮箱地址！');
        return;
      }

      if (!isPasswordValid.value) {
        alert('密码长度必须在8到20个字符之间！');
        return;
      }

      if (!arePasswordsMatching.value) {
        alert('两次输入的密码不一致');
        return;
      }

      try {
        const response = await store.dispatch('register', {
          username,
          email,
          password
        });

        if (response) {
          alert('注册成功!请登录');
          isLogin.value = true;
        } else {
          alert('注册失败');
        }
      } catch (error:any) {
        alert(error.message || '注册失败');
      }
    };

    return {
      isLogin,
      loginForm,
      registerForm,
      isEmailValid,
      isPasswordValid,
      arePasswordsMatching,
      handleInput,
      performLogin,
      performGuestLogin,
      performRegister
    };
  }
});
</script>


<style lang="scss" scoped>
.body {
  width: 100%;
  height: 100vh;
  min-width: 1200px;
  min-height: 800px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-family: "Montserrat", sans-serif;
  font-size: 12px;
  color: #a0a5a8;
}
#videoContainer {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    min-width: 1200px;
    min-height: 800px;
    z-index: -1; /* 让视频在背景层 */
    object-fit: cover;
    overflow: hidden;
  }
  
.fullscreenVideo {
  width: 100%;
  height: 100%;
  min-width: 1200px;
  min-height: 800px;
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
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        font-size: 50px;
        font-weight: 70;
        line-height: 0;
        color: #181818;
      }

      .text {
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        margin-top: 30px;
        margin-bottom: 12px;
        font-size: 14px;
      }

      .form__input {
        width: 250px;
        height: 40px;
        margin: 4px 0;
        padding-left: 25px;
        font-size: 14px;
        letter-spacing: 0.15px;
        border: none;
        outline: none;
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
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
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        font-size: 58px;
        font-weight: 70;
        line-height: 3;
        color: #181818;
      }

      p {
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
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
    //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    width: 180px;
    height: 50px;
    border-radius: 25px;
    margin-top: 50px;
    text-align: center;
    line-height: 50px;
    font-weight: 0;
    font-size: 18px;
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
  .guest-button {
  margin-top: 10px;
  background-color: #e0e0e0;
  color: #333;
  }

  .guest-button:hover {
  background-color: #ccc;
  }

}
</style>

