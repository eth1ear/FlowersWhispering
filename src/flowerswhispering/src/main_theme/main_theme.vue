<template>
    <div class="main">
      <!-- 视频容器 -->
      <div id="videoContainer">
        <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
          <source src="../main_theme/video/background.mp4" type="video/mp4">
        </video>
      </div>
  
      <!-- 用户界面 -->
      <div class="choose">
        <div class="welcome">
        <img>
        <h1>欢迎回家！</h1>
        <h1>{{name}}</h1>
        </div>
        <div class="button-row">
          <div class="button-container">
            <img @click="navigateToPage1" src="../main_theme/pictures/community.png" class="button-image" alt="按钮-通往论坛">
            <p class="button-label">社区</p>
          </div>
          <div class="button-container">
            <img @click="navigateToPage2" src="../main_theme/pictures/book.png" class="button-image" alt="按钮-通往图鉴">
            <p class="button-label">图鉴</p>
          </div>
        </div>
      </div>
    </div>
</template>
  
<script lang="ts">
export default {
  name: 'Welcome',
  data() {
    return {
      name: 'Wuhuairline'   // 给后端同学接入登录的用户名
    };
  },
  methods: {
    navigateToPage1() {
      // 跳转页面的函数在这里，等待完成
    },
    navigateToPage2() {
      // 跳转页面的函数在这里，等待完成
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
    background-image: url("../main_theme/pictures/background.png");
    padding: 20px;
    border-radius: 15px;
    display: inline-block;
    width: 1200px;
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
  </style>