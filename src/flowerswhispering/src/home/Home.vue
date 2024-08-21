<template>
  <div class="common-layout">
    <el-container>
      <el-header>
        <div class="header">
          <div class="logo">Flowers Whispering</div>
          <div class="user-info">
            <span class="username">{{currentUser.username}}</span>
            <img src ="./images/user-avatar.jpg" alt="User Avatar" @click="goToUserProfile()">
          </div>
        </div>
      </el-header>

      <el-main>
        <!-- 视频容器 -->
        <div id="videoContainer">
          <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
            <source src="../assets/video/background.mp4" type="video/mp4">
          </video>      
        </div>

        <!-- 用户界面 -->
        <div class="feature-card" @click="goToCommunity()">
          <img src="./images/community.png" alt="Community">
          <h2>社区功能</h2>
          <p>加入我们的社区，分享你的植物故事，交流养护心得。</p>
          <button>进入社区</button>
        </div>
        <div class="feature-card" @click="goToCatalog()">
          <img src="./images/book.png" alt="Catalog">
          <h2>图鉴功能</h2>
          <p>浏览丰富的植物图鉴，获取详细的植物信息和养护建议。</p>
          <button>进入图鉴</button>
        </div>
      </el-main>

      <el-footer>
        <div class="footer">
          <p><a href="contact.html">联系我们</a></p>
          <p>© 2021 Flowers Whispering</p>
        </div>
      </el-footer>
    </el-container>
  </div>
  
   
</template>
  
<script lang="ts">
import { mapGetters } from 'vuex';
export default {
  name: 'Welcome',
  computed: {
    ...mapGetters(['currentUser'])
  },
  methods: {
    goToCatalog() {
      window.location.href = 'catalog.html';
    },

    goToCommunity() {
      window.location.href = 'community.html';
    },

    goToUserProfile() {
      window.location.href = 'user-profile.html';
    }
  }
};
</script>
  
<style>
  /* 视频容器 */
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
  
  /* 用户界面 */
  .choose {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 1;
    font-size: 25px;
    color: rgba(31, 141, 23, 0.737);
    text-align: center;
    background-image: url("./images/background.png");
    padding: 20px;
    border-radius: 15px;
    display: inline-block;
    width: 1000px;
    height: 600px;
  }

  /*按钮放置*/
  .button-row {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 150px; /* 调整按钮之间的间距 */
    top:500px;
  }
  
  .button-container {
    text-align: center;
  }
  
  .button-image {
    width: 200px;
    height: 180px;
    cursor: pointer;
    transition: transform 0.3s ease;
  }
  
  .button-image:hover {
    transform: scale(1.1);
  }
  
  .button-label {
    margin-top: 10px; /* 调整文本与图标间距 */
    color: rgba(35, 198, 23, 0.737);
    font-size: 40px;
    font-weight: bold;
  }

  body {
    margin: 0;
    font-family: Arial, sans-serif;
    background: url('./images/background.png') no-repeat center center fixed;
    background-size: cover;
    display: flex;
    flex-direction: column;
    height: 100vh;
    color: #333;  
  }

 
  .header {
    display: flex;
    justify-content: space-between; /* 让 logo 靠左，user-info 靠右 */
    align-items: center; /* 垂直居中 */
    padding: 0 20px; /* 可选：增加左右内边距 */
    background-color: #46b476cc;
    /* 调整为柔和的蓝色背景 */
    color: white;
    z-index: 1;
    /* 确保header在视频上方 */
    position: relative;
  }

  .logo {
    font-size: 24px;
    font-weight: bold;
    color: white;
  }

  .user-info {
    display: flex;
    align-items: center;
    gap: 10px;
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

  .username {
    font-size: 16px;
    font-weight: bold;
    color: white;
  }
  

  main {
    flex: 1;
    height: 1080px;
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 40px;
    z-index: 1;
    /* 确保main在视频上方 */
    position: relative;
  }

    .feature-card {
      width: 300px;
      height: 400px;
      background-color: rgba(255, 255, 255, 0.9);
      /* 半透明白色背景 */
      border-radius: 15px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      text-align: center;
      position: relative;
      cursor: pointer;
      transition: transform 0.3s ease, box-shadow 0.3s ease;
      overflow: hidden;
    }

    .feature-card img {
      width: 100%;
      height: 100%;
      object-fit: cover;
      position: absolute;
      top: 0;
      left: 0;
      z-index: 1;
      transition: opacity 0.3s ease, transform 0.5s ease;
    }

    .feature-card h2,
    .feature-card p,
    .feature-card button {
      z-index: 2;
      opacity: 0;
      position: relative;
      transform: translateY(20px);
      transition: opacity 0.3s ease, transform 0.5s ease;
    }

    .feature-card h2 {
      margin-top: -100px;
      font-size: 24px;
      color: #333;
      z-index: 2;
      position: relative;
    }

    .feature-card p {
      margin: 20px 20px;
      font-size: 16px;
      color: #666;
      z-index: 2;
      position: relative;
    }

    .feature-card button {
      position: absolute;
      bottom: 20px;
      left: 50%;
      transform: translateY(100px);
      padding: 10px 20px;
      font-size: 16px;
      background-color: #43a047;
      color: white;
      border: none;
      border-radius: 5px;
      cursor: pointer;
      z-index: 2;
      transition: 0.3s ease;
    }

    .feature-card button:hover {
      background-color: #388e3c;

    }

    .feature-card:hover img {
      opacity: 0.5;
      transform: scale(0.8);
    }

    .feature-card:hover h2,
    .feature-card:hover p,
    .feature-card:hover button {
      opacity: 1;
      transform: translateY(0);
    }

    footer {
      text-align: center;
      padding: 10px;
      background-color: #46b476cc;
      /* 调整为柔和的蓝色背景 */
      font-size: 12px;
      z-index: 1;
      /* 确保footer在视频上方 */
      position: relative;
      color: white;
    }

    footer a {
      color: white;
      text-decoration: none;
    }

    footer a:hover {
      text-decoration: underline;
    }

  </style>