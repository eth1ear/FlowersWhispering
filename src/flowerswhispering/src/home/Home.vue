<template>
  <div class="common-layout">
    <!-- 视频容器 -->
    <div id="videoContainer">
        <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
          <source src="../assets/video/background.mp4" type="video/mp4">
        </video>
      </div>
    <header class="header">
      <div class="logo">Flowers Whispering</div>
      <div class="user-info" v-if="currentUser">
        <span v-if="currentUser" class="username">{{ currentUser.username }}</span>
        <div class="user-avatar-wrapper">
          <img v-if="currentUser" src="./images/user-avatar.jpg" alt="User Avatar" @click="goToUserProfile()">
          <!-- 用户详细信息列表 -->
          <div class="user-info-list">
            <p>用户名: {{ currentUser.username }}</p>
            <p>邮箱: {{ currentUser.email }}</p>
            <p>角色: {{ currentUser.role }}</p>
          </div>
        </div>
        <button class="logout-button" @click="performLogout">{{ currentUser.role === 'guest' ? '登录' : '登出' }}</button>
      </div>
    </header>

    <main class="main-content">
      <!-- 用户界面 -->
      <div class="introduction-card">
        <h1>Flowers Whispering</h1>
        <h2>充满诗意与梦想的园地 植物爱好者们的自留地</h2>
      </div>

      <div class="introduction-content">
        <p>花语无声,婉转悠扬。欢迎来到flowerwhispering这里将引导您步入一个由花朵与绿植共织的奇幻世界。<br><br>
          植物图鉴中蕴藏丰富,每一朵花、每一片叶都有着自己的故事和灵魂。从罕见的野花到庭院中常见的绿植,每一种植物都被赋予了详尽而精美的描述,辅以图片,仿佛置身自然之中,能闻其香、触其绿。<br><br>
          植物论坛中激情澎湃,汇聚了一群同样热爱植物的朋友们。或是园艺初学者,或是资深植物爱好者,这里都是你们的俱乐部。分享养植经验,交流种植心得,论坛的讨论涵盖从植物照护到景观设计的各个方面,每一个帖子都充满了知识与热情。<br><br>
          我们信仰植物的力量,相信它们能够治愈心灵、修养身心。我们一起耳倾花语,手播绿意,走入一段宁静的幸福旅程。</p>
      </div>
      <div class="scroll-down-arrow" @click="scrollToCards">
        <img src="./images/down-arrow.png" alt="Scroll Down" class="arrow-image">
      </div>

      <!-- Feature Cards Container -->
      <div class="feature-cards-container">
        <!-- 社区 -->
        <div class="feature-card" @click="goToCommunity()">
          <img src="./images/community.png" alt="Community">
          <div class="card-content">
            <h3>Community</h3>
          </div>
          <h2>社区</h2>
          <p>加入我们的社区,分享你的植物故事,交流养护心得。</p>
        </div>
        <!-- 管理员界面 -->
        <div v-if="isAdmin" class="feature-card" @click="goToAdminPanel()">
          <img src="./images/admin.png" alt="Admin Panel">
          <div class="card-content">
            <h3>Admin Panel</h3>
          </div>
          <h2>管理员面板</h2>
          <p>访问管理员面板,管理用户和内容。</p>
        </div>
        <!-- 图鉴 -->
        <div class="feature-card" @click="goToCatalog()">
          <img src="./images/book.png" alt="Catalog">
          <div class="card-content">
            <h3>Catalog</h3>
          </div>
          <h2>图鉴</h2>
          <p>浏览丰富的植物图鉴,获取详细的植物信息和养护建议。</p>
        </div>
      </div>
    </main>

    <footer class="footer">
      <p class="left"><a href="contact.html">联系我们</a></p>
      <p class="center">© 2024 Flowers Whispering</p>
      <div class="right"></div>
    </footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { mapGetters, mapActions } from 'vuex';
export default defineComponent({
  name: 'Home',
  data() {
    return {
      isUserInfoVisible: false, // 控制用户信息列表的显示与隐藏
    };
  },
  computed: {
    ...mapGetters(['currentUser', 'isAdmin']),
  },
  methods: {
    ...mapActions(['logout']),
    typeEffect(el: HTMLElement, text: string, delay: number = 100, callback: () => void = () => {}) {
      let i = 0;
      const cursor = document.createElement('span');
      cursor.classList.add('typing-cursor');
      el.appendChild(cursor);

      function typing() {
        if (i < text.length) {
          el.innerHTML = text.substring(0, i + 1);
          el.appendChild(cursor); // 保持光标在末尾
          i++;
          setTimeout(typing, delay);
        } else {
          cursor.classList.add('blink'); // 打字完成后光标开始闪烁
          if (callback) {
            callback(); // 完成后调用回调函数
          }
        }
      }
      typing();
    },
    startTypingEffect() {
      const paragraphElement = document.querySelector('.introduction-content p') as HTMLElement;
      const text = paragraphElement.dataset.text || ''; // 获取段落的文本
      paragraphElement.innerText = ''; // 清空段落的初始文本

      this.typeEffect(paragraphElement, text, 50); // 启动打字效果
    },
    scrollToCards() {
      const cardsSection = document.querySelector('.feature-cards-container') as HTMLElement;
      const arrowButton = document.querySelector('.scroll-down-arrow') as HTMLElement;

      if (cardsSection) {
        cardsSection.style.display = 'flex'; // 将 display 设置为 flex 以显示卡片
        cardsSection.classList.add('visible');
        cardsSection.scrollIntoView({ behavior: 'smooth' });
      }

      if (arrowButton) {
        arrowButton.classList.add('fade-out'); 
        arrowButton.style.pointerEvents = 'none'; // 禁用点击
      }
    },
    goToCatalog() {
      this.$router.push('/catalog');
    },
    goToCommunity() {
      this.$router.push('/community');;
    },
    goToUserProfile() {
      this.$router.push('/profile');;
    },
    goToAdminPanel() {
      this.$router.push('/adminpanel');
    },
    performLogout() {
      this.logout(); 
      this.$router.push('/login'); // 跳转到登录页面
    },
    showUserInfo() {
      this.isUserInfoVisible = true;
    },
    hideUserInfo() {
      this.isUserInfoVisible = false;
    },
  },
  mounted() {
    const paragraphElement = document.querySelector('.introduction-content p') as HTMLElement;
    paragraphElement.dataset.text = paragraphElement.innerText;
    paragraphElement.innerText = ''; // 清空文本内容以防止页面初始显示

    this.startTypingEffect(); // 启动打字效果
  }
});
</script>

<style>
  .scroll-down-arrow {
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    margin-top: 20px;
    animation: bounce 1.5s infinite;
    transition: opacity 0.5s ease-in-out; /* 添加透明度渐变效果 */
  }

  .scroll-down-arrow.fade-out {
    opacity: 0; /* 渐变到完全透明 */
  }

  .arrow-image {
    width: 50px; /* 根据需要调整图片大小 */
    height: auto;
  }

  @keyframes bounce {
    0%, 20%, 50%, 80%, 100% {
      transform: translateY(0);
    }
    40% {
      transform: translateY(-10px);
    }
    60% {
      transform: translateY(-5px);
    }
  }

  .typing-cursor {
    display: inline-block;
    width: 2px;
    height: 1em;
    background-color: black;
    margin-left: 2px;
    vertical-align: bottom;
    animation: none;
  }

  .blink {
    animation: blink 1s step-start infinite; 
  }

  @keyframes blink {
    50% {
      opacity: 0;
    }
  }

  .user-avatar-wrapper {
    position: relative;
    display: inline-block;
  }

  .user-info-list {
    position: absolute;
    top: 50px;
    left: -125px;
    background-color: rgba(255, 255, 255, 0.95);
    border: 2px solid #46b476;
    border-radius: 8px; 
    padding: 15px;
    box-shadow: 0 6px 16px rgba(0, 0, 0, 0.15); 
    z-index: 10;
    min-width: 200px;
    opacity: 0;
    transform-origin: top;
    transform: translateY(0px) scale(0.05);
    transition: opacity 0.3s ease, transform 0.3s ease;
    pointer-events: none;
  }

  .user-avatar-wrapper:hover .user-info-list {
    opacity: 1;
    transform: translateY(0) scale(1); 
    pointer-events: auto; 
  }

  .user-info-list p {
    margin: 2px 0;
    font-size: 14px;
    color: #333;
    font-family: '宋体','ZhiMangXing-Regular', sans-serif;
  }

  .logout-button {
    background-color: #ff4d4d;
    color: white;
    border: none;
    border-radius: 5px;
    padding: 5px 10px;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }

  .logout-button:hover {
    background-color: #ff1a1a;
  }

  .common-layout {
    margin: 0;
    padding: 0;
    font-family: Arial, sans-serif;
    background-size: cover;
    display: flex;
    flex-direction: column;
    height: 100vh;
    width: 100vw;
    min-width: 1200px;
    min-height: 800px;
    color: #333;
  }

  .header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 20px;
    background-color: #46b476cc;
    color: white;
    z-index: 1;
    position: relative;
  }

  .logo {
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    font-size: 28px;
    font-weight: bold;
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
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    font-size: 28px;
    font-weight: bold;
  }

  .main-content {
    flex: 1;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    gap: 40px;
    position: relative;
  }

  #videoContainer {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    min-width: 1200px;
    min-height: 800px;
    z-index: -1;
    overflow: hidden;
  }

  .fullscreenVideo {
    width: 100%;
    height: 100%;
    min-width: 1200px;
    min-height: 800px;
    object-fit: cover;
  }


  .introduction-card {
    margin-top: 30px;
    width: 100%; /* 使 introductionCard 占据整行 */
    max-width: 1200px; /* 可选：设置最大宽度 */
    height: 375px;
    background-color: rgba(164, 207, 168, 0);
    border-radius: 15px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0);
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    text-align: center;
    cursor: pointer;
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    overflow: hidden;
    margin-bottom: 0px;
  }

  .introduction-card h1 {
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    font-size: 154px;
    margin-top:60px;
    margin-bottom:40px;
    color: #f3fff3;
  }

  .introduction-card h2 {
    font-family: '黑体','ZhiMangXing-Regular', sans-serif;
    font-size: 35px;
    margin-top:0px;
    color: #cfe3cf;
  }

  .introduction-content {
    margin-top: 0px;
    width: 100%; /* 使 introductionCard 占据整行 */
    max-width: 1400px; /* 可选：设置最大宽度 */
    height: 500px;
    background-color: rgba(177, 212, 181, 0.874);
    border-radius: 15px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.068);
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    text-align: center;
    cursor: pointer;
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    overflow: hidden;
    margin-bottom: 40px;
  }

  .introduction-content p {
    font-family: '华文楷体','ZhiMangXing-Regular', sans-serif;
    font-size: 24px;
    font-weight: 600;
    text-align: justify;
    color: #252b25;
    text-align: left;
    white-space: pre-wrap; /* 保留换行 */
    margin-left:80px;
    margin-right:80px;
    margin-top:50px;
    margin-bottom:0px;
  }


  .feature-cards-container {
    display: none;
    opacity: 0;
    transition: opacity 1s ease-in-out;
    justify-content: space-evenly; /* 让 feature-cards 横向排列 */
    gap: 40px; /* 让卡片之间保持间距 */
    width: 100%; /* 占据整个宽度 */
    max-width: 1400px; /* 可选：设置最大宽度 */
    margin-bottom:50px;
  }
  .feature-cards-container.visible {
    display: flex; /* 当添加 .visible 类时显示 */
    opacity: 1;
  }

  .feature-card {
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    width: 390px;
    height: 500px;
    background-color: rgba(255, 255, 255, 0.9);
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

  .card-content {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: rgba(0, 0, 0, 0.8);
    transition: transform 0.3s ease, opacity 0.3s ease;
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
    font-family: '楷体','ZhiMangXing-Regular', sans-serif;
    margin-top: -100px;
    font-size: 52px;
    color: #333;
  }

  .feature-card p {
    font-family: '宋体','ZhiMangXing-Regular', sans-serif;
    margin: 20px 20px;
    font-size: 22px;
    color: #666;
  }

  .card-content {
    position: absolute;
    font-size: 48px;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: white;
    text-align: center;
    transition: transform 0.3s ease, opacity 0.3s ease;
    z-index: 1; /* 确保文字在图片上方 */
  }
  .feature-card:hover img {
    opacity: 0.5;
    transform: scale(0.8);
  }

  .feature-card:hover .card-content {
    transform: translate(-50%, 50%) scale(0.5);
    opacity: 0;
  }

  .feature-card:hover h2,
  .feature-card:hover p,
  .feature-card:hover button {
    opacity: 1;
    transform: translateY(0);
  }

  .footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0px 20px;
  background-color: #46b476cc;
  font-size: 15px;
  color: white;
}

.footer .left {
  flex: 1; /* 左侧元素占用剩余空间 */
}

.footer .center {
  flex: 1;
  text-align: center; /* 居中对齐 */
}

.footer .right {
  flex: 1; /* 右侧占位元素占用剩余空间,保持居中 */
}

.footer a {
  color: white;
  text-decoration: none;
}

.footer a:hover {
  text-decoration: underline;
}

</style>
