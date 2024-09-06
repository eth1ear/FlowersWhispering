<template>

  <!--大标题-->
  <header class="header">
  <div class="logo">Flowers Whispering</div>
  <div class="nav-user-container">
    <nav class="nav-links">
      <nav v-if="currentUser.userRole === 'admin'">
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
          <div v-if="currentUser.userRole !== 'guest'">
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

                  <button :class="{ active: activeTab === 'Announcement' }" @click="setActiveTab('Announcement')">社区首页</button>
                  <button :class="{ active: activeTab === 'PersonalCenter' }" @click="setActiveTab('PersonalCenter')">个人中心</button>
      
                  <button :class="{ active: activeTab === 'HotPosts' }" @click="setActiveTab('HotPosts')">热门帖子</button>
                  <button :class="{ active: activeTab === 'SentPost' }" @click="setActiveTab('SentPost')">帖子发布</button>
                  <button :class="{ active: activeTab === 'Category' }" @click="setActiveTab('Category')">帖子分类</button>
                  <button :class="{ active: activeTab === 'ContributorsList' }" @click="setActiveTab('ContributorsList')">贡献榜单</button>
                  <button :class="{ active: activeTab === 'home' }" @click="GoToHome">返回首页</button>

                </div> 
            </div>
        </div>
  
      <div class="top-banner">
          <button class="user-button" @click="Gotouserpage">   <!--用户头像-->
              <img src="./images/logo.png" alt="User" />   
            
          </button>
      </div>

      

      <!--个人中心部分-->
      <div v-if="activeTab === 'PersonalCenter'">
        
  <!--功能点按钮设置卡片效果-->

  

  <div class="card-container1">                        
        <div class="card" @click="GotoPostDetil()">
          <img src="./images/button1.png" alt="Card 1" />
          <div class="card-info">我的帖子</div>
        </div>
        <div class="card" @click="GoToCultivation()">
          <img src="./images/button2.png" alt="Card 2" />
          <div class="card-info">我的评论</div>
        </div>
        <div class="card" @click="GoToRecognition()">
          <img src="./images/button3.png" alt="Card 3" />
          <div class="card-info">我的收藏</div>
        </div>
      </div>
      <!--功能点按钮，设置卡片效果-->
      </div>
     <!--个人中心部分-->



      <!--公告部分-->
      <div v-if="activeTab === 'Announcement'" class="card-container">
        <div class="card-container">                        
            <div class="love-time-title1">
              欢迎来到叶语花谣社区
            </div>
        </div>

        <div class ="information-box1">    <!--搜索框，重要部分！！！-->
             <input 
              type="text" 
              v-model="searchQuery"   
              placeholder="搜索你喜欢的帖子！" 
              class="information-input1" 
             />
            <button @click="searchDatabase" class="information-button1">搜索</button>
           </div>
  


          </div>
         <!--公告部分-->

         <!--发帖部分-->
      <div v-if="activeTab === 'SentPost'">
        <div class="post-container">
    <div class="post-title">
      发布帖子
    </div>
    <form @submit.prevent="submitPost">
      <div class="form-group">
        <label for="title" class="form-label">标题:</label>
        <input type="text" id="title" v-model="title" class="form-input" required />
      </div>
      <div class="form-group">
        <label for="content" class="form-label">内容:</label>
        <textarea id="content" v-model="content" class="form-textarea" rows="5" required></textarea>
      </div>
      <div class="form-buttons">
        <button type="submit" class="btn-submit">发布</button>
        <button type="button" @click="discard" class="btn-cancel">放弃</button>
      </div>
    </form>
  </div>

      </div>
      <!--发帖部分-->

  <!--分类部分-->
  <div v-if="activeTab === 'HotPosts'">
        
    

      </div>
     <!--分类部分-->

     
  
    
       
     
<!-- 用户贡献榜部分 -->
<div v-if="activeTab === 'ContributorsList'" class="contributors-container">
    <h2 class="contributors-title">用户贡献榜</h2>
    <ul class="contributors-list">
      <li v-for="(user, index) in paginatedContributors" :key="index" class="contributor-item">
        <span class="user-rank">{{ (currentPage - 1) * itemsPerPage + index + 1 }}</span>
        <span class="user-name">{{ user.username }}</span>
        <span class="user-contribution">贡献分数: {{ calculateContribution(user) }}</span>
      </li>
    </ul>
    <div class="pagination">
      <button @click="prevPage" :disabled="currentPage === 1">上一页</button>
      <span>页码 {{ currentPage }} / {{ totalPages }}</span>
      <button @click="nextPage" :disabled="currentPage === totalPages">下一页</button>
    </div>
  </div>
<!-- 用户贡献榜部分 -->



  
           
    

        
         
       
        

  


      




  
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
        activeTab:"Announcement",
        isUserInfoVisible: false, // 控制用户信息列表的显示与隐藏

       
      currentPage: 1,
      
      itemsPerPage: 5 ,// 每页显示的用户数
      contributors: [
      { username: 'Alice', posts: 10, comments: 20 },
      { username: 'Bob', posts: 5, comments: 15 },
      { username: 'Charlie', posts: 8, comments: 30 },
      { username: 'David', posts: 7, comments: 12 },
      { username: 'David1', posts: 7, comments: 12 },
      { username: 'David2', posts: 7, comments: 12 },
      { username: 'David3', posts: 7, comments: 12 },
      { username: 'David4', posts: 7, comments: 12 },
      { username: 'David5', posts: 7, comments: 12 },
      { username: 'David6', posts: 7, comments: 12 },
      { username: 'David7', posts: 7, comments: 12 },
      { username: 'David8', posts: 7, comments: 12 },
      
     
    ],
     // 发帖
     title: '',
      content: ''
        
     
      };
    },
    
    mounted() {
    window.scrollTo(0, 0);
    this.startCarousel();
  },
    computed: {
    ...mapGetters({
      currentUser: 'getUserInfo', // 获取当前用户信息
      isAdmin: 'isAdmin',
    }),
    // 计算分页后的用户列表
    paginatedContributors() {
      // 计算总页数
      const totalPages = Math.ceil(this.contributors.length / this.itemsPerPage);

      // 计算当前页码的开始和结束索引
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;

      // 对用户按贡献度排序
      const sortedContributors = this.contributors.slice().sort((a, b) => {
        return this.calculateContribution(b) - this.calculateContribution(a);
      });

      // 返回当前页的用户列表
      return sortedContributors.slice(start, end);
    },
    // 计算总页数
    totalPages() {
      return Math.ceil(this.contributors.length / this.itemsPerPage);
    }
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

    goToDetail(id) {
      // 使用 Vue Router 导航到帖子详情页
      this.$router.push({ name: 'PostDetail', params: { id } });
    
    },
   startCarousel() {
      setInterval(this.nextSlide, 3000);
    },
    calculateContribution(user) {
      // 计算贡献分数 = 发帖数 * 5 + 评论数
      return (user.posts * 5) + user.comments;
    },
    // 前一页
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    // 下一页
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
  
    async submitPost() {
      // 提交到后端逻辑
      try {
        await fetch('https://your-backend-api.com/posts', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            title: this.title,
            content: this.content
          })
        });
        alert('帖子已发布成功!');
        this.clearForm();
      } catch (error) {
        console.error('发布失败:', error);
        alert('发布失败，请稍后再试。');
      }
    },
    discard() {
      this.clearForm();
    },
    clearForm() {
      this.title = '';
      this.content = '';
    },
  
      Gotouserpage()
      {
          this.$router.push('/userprofile');
      }, //切换用户页面
      
      GotoPostDetil()
      {
          this.$router.push('/postDetail');
      }, //切换用户页面
  
      
      
      
      GoToHome()
      {
          this.$router.push('/home');
      },//返回主页界面
      
     
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
  

  
.card-container1 
    {
      display: flex;
      flex-wrap: wrap;
      gap: 80px; /* 卡片之间的间距 */
      cursor: pointer; /*指针变化*/
      margin-top: -50px; /* 调整距离顶部的距离 */
    }
    
    .card {
      position: relative;
      left:5%;
      width: 300px; /* 自定义卡片宽度 */
      height: 300px; /* 自定义卡片高度 */
      overflow: hidden; /* 隐藏超出边界的部分 */
      border-radius: 10px; /* 圆角效果 */
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* 卡片阴影 */
      transition: transform 0.3s ease, box-shadow 0.3s ease; /* 动态平滑 */
      border-radius: 12px; /* 圆角边框 */
    }
    
    .card img {
      width: 100%;
      height: 100%;
      object-fit: cover; 
      background-color: aliceblue;
    }
    
    .card-info {
      position: absolute;
      bottom: 0;
      left: 0;
      right: 0;
      background: rgb(28, 127, 13);
      color: white; /* 文字颜色 */
      font-size: 28px;
      padding: 10px;
      text-align: center;
      transform: translateY(100%); /* 初始隐藏 */
      transition: transform 0.3s ease; /* 动态平滑 */
      font-family: '黑体','ZhiMangXing-Regular', sans-serif;
    }
    
    
    
    .card:hover {
      transform: scale(1.05); /* 卡片放大效果 */
      box-shadow: 0 6px 12px rgba(0, 0, 0, 0.3); /* 卡片阴影放大效果 */
    }
    
    .card:hover .card-info {
      transform: translateY(0); /* 鼠标悬停时显示文字 */
    }
  
  @keyframes jianBian {
    to {
        background-position: -2000rem;
    }
}
  .love-time-title1 {
    position: absolute;
    top: 15%;            /* 垂直居中 */
    left: 52%;           /* 水平居中 */
    transform: translate(-50%, -50%); /* 精确居中 */
    font-size: 4rem;
    font-weight: 600;
    letter-spacing: 0.2rem;
    line-height: 4rem;
    text-align: center;
    background: linear-gradient(to right, #ff4500, #ffa500, #ffd700, #90ee90, #00ffff, #1e90ff, #9370db, #ff69b4, #ff4500);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    animation: jianBian 60s linear infinite;
    width: 3000px; /* 根据需要调整宽度 */
   
}

   /*  搜索框   */

   .information-box1 
  {
    display: flex;
    position:relative;
    left:5%;

    margin-top: -50px; /* 设置距离顶部的间距 */
}

.information-input1 
{
 
  width: 800px;
  padding: 10px;
  border: 2px solid rgb(32, 197, 209); /* 绿色边框 */
  border-radius: 45px;
  outline: none;
  transition: border-color 0.3s ease;
  margin-right: 10px; /* 搜索框与按钮之间的间距 */
  font-size:16px;
}

.information-input1:focus {
  border-color: rgb(61, 173, 205); /* 聚焦时的边框颜色 */
}

.information-button1 {
  padding: 5px 20px;
  background-color: rgb(61, 220, 228); /* 按钮背景色 */
  border: none;
  border-radius: 45px;
  color: white;
  font-size:20px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.information-button1:hover {
  background-color: rgb(28, 127, 13); /* 悬停时的按钮背景色 */
  transform: scale(1.05); /* 悬停时放大效果 */
}




  



/* 用户贡献榜 */
/* 用户贡献榜 */
.contributors-container {
 
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

.contributors-title {
  text-align: center; /* 居中标题 */
  position: relative;
  top: 0;
  font-size: 35px;
  z-index: 6;
  color: rgb(45, 198, 22);
  margin-bottom: 0;
}

.contributors-list {
  padding: 0;
  margin: 0;
  list-style-type: none; /* 移除默认的列表样式 */
}

.contributor-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  transition: transform 0.3s ease; /* 动态效果 */
  cursor: pointer; /* 鼠标放在上面改变指针 */
  background-color: #d2f0ed;
  margin: 8px 0;
  border-radius: 5px;
}

.contributor-item:hover {
  transform: scale(1.05); /* 放大效果 */
}

.user-rank {
  font-size: 22px;
  font-weight: bold;
  margin-right: 10px;
}

.user-name {
  font-size: 22px;
  transition: color 0.3s ease; /* 动态颜色变化效果 */
}

.user-name:hover {
  color: rgb(37, 210, 226); /* 悬停时的文字颜色 */
}

.user-contribution {
  font-size: 18px;
}

.pagination {
    text-align: center; /* 水平居中对齐 */
  }
  .pagination button {
    margin: 0 5px; /* 可选：按钮之间的间距 */
  }

   /* 帖子发布 */
   .post-container {
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
  display: flex;
  flex-direction: column;
}

.post-title {
  text-align: center;
  font-size: 35px;
  color: rgb(45, 198, 22);
  margin-bottom: 20px;
}

.form-content {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.form-group {
  margin-bottom: 15px;
}

.form-label {
  display: block;
  font-size: 18px;
  margin-bottom: 5px;
}

.form-input, .form-textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
  box-sizing: border-box;
}

.form-input {
  font-size: 16px;
}

.form-textarea {
  font-size: 16px;
}

.form-buttons {
  display: flex;
  justify-content: center;
  margin-top: 240px;
}

.btn-submit, .btn-cancel {
  font-size: 18px;
  padding: 10px 20px;
  margin: 0 5px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-submit {
  background-color: rgb(45, 198, 22);
  color: #fff;
}

.btn-submit:hover {
  background-color: rgb(37, 210, 226);
}

.btn-cancel {
  background-color: #d2f0ed;
  color: rgb(45, 198, 22);
}

.btn-cancel:hover {
  background-color: rgb(37, 210, 226);
  color: #fff;
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