<template>

  <!--大标题-->
  <header class="header">
  <div class="logo">Flowers Whispering</div>
  <div class="nav-user-container">
    <nav class="nav-links">
      <button @click="goToHome" class="nav-button">首页</button>
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
            <p>角色: {{ currentUser.userRole }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</header>
       <!--大标题-->
  
  
      <div class="book-background">
          <!-- 视频容器 -->
      <div id="videoContainer">
          <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
            <source src="../assets/video/background.mp4" type="video/mp4">
          </video>
      </div>
      </div>
  
       
      <!-- 用户操作界面容器 ，所有界面内的按钮，文本都在此改动-->
  <div class="user-interfacer">
     <!-- 竖条图片容器 -->
     <div class="side-bar">
            <div class="user-info">
                <p class="user-name">{{userName}}</p>    <!--显示用户名-->
    
                <div class="tabs">              <!--显示选项卡-->

                  <button :class="{ active: activeTab === 'Mypost' }" @click="setActiveTab('Mypost')">我的帖子</button>
                  <button :class="{ active: activeTab === 'Mycomment' }" @click="setActiveTab('Mycomment')">我的评论</button>
      
                  <button :class="{ active: activeTab === 'home' }" @click="goToCommunity">返回上步</button>

                </div> 
            </div>
        </div>
  
      <div class="top-banner">
          <button class="user-button" @click="GotoPersonalCenter">   <!--用户头像-->
              <img src="./images/logo.png" alt="User" />   
              <span class="tooltip">个人中心</span>
          </button>
      </div>

      
      <div class="post-container1">
    <h1 class="post-header1">帖子列表</h1>
    <ul class="post-list1">
      <li v-for="item in visiblePosts" :key="item.id" class="post-item1">
        <div>
          <h2 class="post-title1">{{ item.title }}</h2>
          <p class="post-summar1y">{{ summarizeContent(item.content) }}</p>
        </div>
        <div class="post-info1">
          <span class="post-author1">{{ item.author }}</span>
          <span class="post-time1">{{ item.date }}</span>
        </div>
      </li>
    </ul>
    <div class="pagination-controls1">
      <button @click="navigateToPrevious" :disabled="currentPage === 1">上一页</button>
      <button @click="navigateToNext" :disabled="currentPage === totalPagesCount">下一页</button>
    </div>
  </div>
     


      

        
     
  
    
       

      




  
  </div>
  
  <footer class="footer">
      <p class="left"><a href="contact.html">联系我们</a></p>
      <p class="center">© 2024 Flowers Whispering</p>
      <div class="right"></div>
    </footer>
  
  </template>
  
  <script>
  import { defineComponent } from 'vue';
  import { mapGetters, mapActions } from 'vuex';
  
  export default {
    name: "Community",
    data() {
      return {
        buttonImageUrl: '../catalog/images/user_example.png',  // 默认图片，后端接入用户头像
        userName: 'Wuhuairline', // 默认用户名,后端接入用户姓名
        activeTab:"Mypost",
        isUserInfoVisible: false, // 控制用户信息列表的显示与隐藏

       
      
      // 添加更多测试数据
    
    
        
     
      };
    },
    
    mounted() {
    window.scrollTo(0, 0);
    
  },
    computed: {
    ...mapGetters({
      currentUser: 'getUserInfo', // 获取当前用户信息
      isAdmin: 'isAdmin',
    }),
    //帖子列表
    totalPagesCount() {
      return Math.ceil(this.postsData.length / this.itemsPerPage);
    },
    visiblePosts() {
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = startIndex + this.itemsPerPage;
      return this.postsData.slice(startIndex, endIndex);
    },
  },
    methods:
    {
      ...mapActions(['logout']),

      
    handleUserAvatarClick() {
      if (this.currentUser.role === 'guest') {
        this.$router.push('/login'); // 如果是guest用户，点击跳转到登录页面
      } else {
        this.goToUserProfile(); // 否则跳转到个人主页
      }
    },
    setActiveTab(tabName)
        {
          this.activeTab=tabName;  
        },  //设置选项卡

        async fetchPostsData1() {
    // 模拟从后端获取数据
      // this.postsData = await fetch('your-api-endpoint').then(response => response.json());
    this.postsData = [
      { id: 1, title: '帖子标题1', content: '这是帖子内容1...', author: '作者1', date: '2024-09-01' },
      { id: 2, title: '帖子标题2', content: '这是帖子内容2...', author: '作者2', date: '2024-09-02' },
      // 你可以添加更多的测试数据
      { id: 3, title: '帖子标题3', content: '这是帖子内容3...', author: '作者3', date: '2024-09-03' },
      { id: 4, title: '帖子标题4', content: '这是帖子内容4...', author: '作者4', date: '2024-09-04' },
      // 确保数据量超过 itemsPerPage 以测试分页功能
      { id: 5, title: '帖子标题5', content: '这是帖子内容5...', author: '作者5', date: '2024-09-05' },
      { id: 6, title: '帖子标题6', content: '这是帖子内容6...', author: '作者6', date: '2024-09-06' },
      { id: 7, title: '帖子标题7', content: '这是帖子内容7...', author: '作者7', date: '2024-09-07' },
      { id: 8, title: '帖子标题8', content: '这是帖子内容8...', author: '作者8', date: '2024-09-08' },
      { id: 9, title: '帖子标题9', content: '这是帖子内容9...', author: '作者9', date: '2024-09-09' },
      { id: 10, title: '帖子标题10', content: '这是帖子内容10...', author: '作者10', date: '2024-09-10' },
      { id: 11, title: '帖子标题11', content: '这是帖子内容11...', author: '作者11', date: '2024-09-11' },
      // 添加更多测试数据以覆盖多个页面
    ];
  },
      
    summarizeContent(content) {
      return content.length > 100 ? content.slice(0, 100) + '...' : content;
    },
    navigateToPrevious() {
      if (this.currentPage > 1) this.currentPage--;
    },
    navigateToNext() {
      if (this.currentPage < this.totalPagesCount) this.currentPage++;
    },
    
  

  
      Gotouserpage()
      {
          this.$router.push('/userprofile');
      }, //切换用户页面
      
      GotoPostDetil()
      {
          this.$router.push('/postDetail');
      }, //切换用户页面
  
      goToCommunity()
      {
          this.$router.push('/community');
      }, //切换用户页面
      
      
      GoToHome()
      {
          this.$router.push('/home');
      },//返回主页界面
      
     
    },
    created() {
    this.fetchPostsData1();
  }
  };
  </script>
  
  
  
  
  <style scoped>
  .book-background 
  {
      display: flex;
     flex-direction: column;
     min-height: 100vh;
     position: relative;
  }
    
    /* 设置背景图像 */
    .book-background 
    {
      background-size: cover;
      height: 100vh; /* 使背景覆盖整个视口 */
      display: flex;
      align-items: center;
      justify-content: center;
      z-index:-1;
    }
    
  
    /* 操作界面*/ 
    .user-interfacer {
  position: absolute;
  top: 55%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 80%;
  height: 90%;
  background-image: url('./images/backgroundtry.png');
  background-size: cover;
  background-position: center; /* 确保背景图片居中 */
  z-index: 2;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  color: white;
  border: 5px solid rgb(36, 156, 172);
  box-sizing: border-box;
  border-radius: 10px;
  opacity: 0.9;
  }
  
  
  /* 竖条图片格式*/
  
  .side-bar {
      position: absolute;
      top: 20%; /* 从顶部开始对齐 */
      left: 0; /* 从左侧开始对齐 */
      width: 140px; /* 自定义宽度 */
      height: fill; /* 自定义高度 */
      display: flex;
      flex-direction: column;
      align-items: center;
      z-index: 3; /* 确保竖条在其他内容之上 */
      background-color: rgb(73, 192, 206); /* 背景颜色，使内容更显眼 */
  }
  
  
  .user-info {
      display: flex;
      flex-direction: column;
      align-items: center;
      color: rgb(17, 213, 244); /* 设置文本颜色 */
  }
  
  .user-name {
      margin: 20px 0; /* 自定义用户名上下间距 */
      font-size: 20px; /* 自定义用户名字体大小 */
      text-align: center; /* 文本居中对齐 */
  }
  
  .tabs {
      display: flex;
      flex-direction: column;
      align-items: center;
  }
  
  .tabs button {
      background: none;
      border: none;
      color: white;
      font-size: 18px;
      margin: 10px 0;
      cursor: pointer;
      padding: 10px 20px;
      border-radius: 5px;
      transition: background-color 0.3s ease, color 0.3s ease;
  }
  .tabs button.active {
    background-color: rgba(19, 49, 169, 0.6); /* 选中时背景颜色 */
     color: #18c574; /* 选中时文本颜色 */
}
  
  .tabs button:hover {
      background-color: rgba(38, 77, 234, 0.289); /* 鼠标悬停效果 */
      color: #18c574; /* 鼠标悬停时文本颜色 */
  }
  
  
  
  
  /* 横条图片格式*/ 
  .top-banner {
    width: 100%; /* 横条图片容器宽度占据整个用户界面宽度 */
    display: flex;
    position: absolute;
    top: 0; /* 将横条图片定位到容器顶部 */
    left: 0; /* 从左侧开始对齐 */
    z-index: 4; /* 确保横条图片在用户界面内容之上 */
  }
  
  
  
  
  .top-banner img {
    width: 870px; /* 自定义横条宽度 */
    height: 150px; /* 自定义高度 */
  }
  
  .user-button
  {
     z-index: 4;
    border: 5px solid rgb(4, 195, 202); /* 绿色边框 */
    box-sizing: border-box; 
    transition: transform 0.3s ease, background-color 0.3s ease;  /*动态平滑*/
    cursor: pointer; /*指针变化*/
  }
  
  .user-button img {
    width: 120px; /* 自定义按钮图片的宽度 */
    height: 135px; /* 自定义按钮图片的高度 */
  }
  
  /* 鼠标悬停效果 */
  .user-button:hover {
    transform: scale(1.1); /* 放大按钮 */
  }

  .user-button .tooltip {
  display: none;
  position: absolute;
  bottom: 0%; /* 在按钮上方显示 */
  left: 0;
  width: 100%; /* 宽度充满按钮 */
  min-height: 30px; /* 最小高度 */
  background: #1cacef;
  color: #fff;
  padding: 10px 0; /* 上下填充 */
  border-radius: 3px;
  text-align: center; /* 文本居中 */
  box-sizing: border-box; /* 包括 padding 和 border 在内 */
  font-size: 18px; /* 设置文本大小 */
}

.user-button:hover .tooltip {
  display: block;
}
  


  
/* 帖子详情 */

.post-container1 {
  position: absolute;
  top: 8%;
  left: 20%;
  width: 900px;
  height: 600px;
  padding: 20px;
  background-color: #ffffff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  border: 2px solid rgb(45, 198, 22);
  color: rgb(45, 198, 22);
}

.post-header1 {
  text-align: center;
  font-size: 35px;
  color: rgb(45, 198, 22);
  margin-bottom: 0;
}


.post-list1 {
  padding: 0;
  margin: 0;
  list-style-type: none;
  max-height: 480px; /* 设置最大高度 */
  overflow-y: auto; /* 启用垂直滚动条 */
}


.post-item1 {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  transition: transform 0.3s ease;
  cursor: pointer;
  background-color: #d2f0ed;
  margin: 8px 0;
  border-radius: 5px;
}

.post-item1:hover {
  transform: scale(1.05);
}

.post-title1 {
  font-size: 22px;
  margin: 0;
}

.post-summary1 {
  font-size: 18px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.post-info1 {
  display: flex;
  justify-content: space-between;
  font-size: 16px;
}

.post-author1, .post-time1 {
  color: rgb(45, 198, 22);
}

.pagination-controls1 {
  text-align: center;
  margin-top: 20px;
}

.pagination-controls1 button {
  margin: 0 5px;
}






  /*  固有写法，显示用户*/ 
  .user-avatar-wrapper {
    position: relative;
    display: inline-block;
  }

  .user-info-list {
    position: absolute;
    top: 50px;
    left: -200px!important;
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
  background-color: rgba(70, 180, 118, 0.8); /* 使用rgba设置透明度，0.8表示80%的不透明度 */
  color: white;
  z-index: 10; /* 提高 z-index，确保 header 在其他内容上层 */
  position: relative;
}

.logo {
  font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
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

.user-info {
  position: relative;
}

.user-avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  cursor: pointer;
}

.user-avatar-wrapper {
  display: flex;
  align-items: center;
}

.user-info-list {
  z-index: 20; /* 提高用户信息列表的层级，确保它显示在 header 之上 */
  position: absolute;
  top: 50px;
  right: 0;
  background-color: white; /* 为弹出的内容添加背景色，避免透明度导致内容不清晰 */
  padding: 10px;
  border-radius: 5px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); /* 添加阴影效果，确保弹出层次感 */
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

  
  
    /*  固有写法，显示用户*/ 
  
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