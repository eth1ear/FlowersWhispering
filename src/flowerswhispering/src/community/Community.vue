<template>
    <div class="forum-container">
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
          <img v-if="currentUser" src="../home/images/user-avatar.jpg" alt="User Avatar" @click="goToUserProfile()">
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
      <div class="forum-content">
        <div class="top-bar">
      <img src=".\images\logo.png" alt="Logo" class="logo1" />
     
      <div class="search-container">
        <input type="text" placeholder="搜索..." class="search-box" />
        <button class="search-button">搜索</button>
    </div>
    </div>
    <nav>
        <ul>
          <li :class="{ active: currentPage === 'home' }"><a href="#" @click="setPage('home')">首页</a></li>
          <li :class="{ active: currentPage === 'category' }"><a href="#" @click="setPage('category')">分类</a></li>
          <li :class="{ active: currentPage === 'hot' }"><a href="#" @click="setPage('hot')">热帖</a></li>
          <li :class="{ active: currentPage === 'post' }"><a href="#" @click="setPage('post')">发帖</a></li>
          <li :class="{ active: currentPage === 'profile' }"><a href="#" @click="setPage('profile')">个人中心</a></li>
        </ul>
      </nav>
        
      <!-- 首页 -->
      <div v-if="currentPage === 'home'">
        <div class="postcontainer">

<div v-for="post in paginatedPosts" :key="post.id" class="post">
<h2>{{ post.title }}</h2>
<p class="content">{{ post.content }}</p>
 <div class="footer">
  <span class="author">{{ post.author }}</span>
  <span class="date">{{ post.date }}</span>
 </div>
</div>
<!-- Pagination -->
<div class="pagination">
<button @click="prevPage" :disabled="currentPageNumber === 1">上一页</button>
<span>第 {{ currentPageNumber }} 页</span>
<button @click="nextPage" :disabled="currentPageNumber === totalPages">下一页</button>
</div>
</div>
  
      </div>


      <!-- 发帖 -->
      <div v-if="currentPage === 'post'">
       
      </div>


      <br />
      <div class="actions">
        <button @click="goHome" class="btn-primary">返回主页</button>
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
  
  <script>
  export default {
    name: 'PlantForum',
    data() {
      return {
        newPostTitle: '',
        newPostContent: '',
        isUserInfoVisible: false, // 控制用户信息列表的显示与隐藏
  
        searchQuery: '',
      currentPage: 'home',
      currentPageNumber: 1,
      posts: [
        { id: 1, title: '帖子1', content: '这是帖子1的内容...', author: '用户A', date: '2024-08-23' },
        { id: 2, title: '帖子2', content: '这是帖子2的内容...', author: '用户B', date: '2024-08-22' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子4', content: '这是帖子4的内容...', author: '用户D', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        { id: 3, title: '帖子3', content: '这是帖子3的内容...', author: '用户C', date: '2024-08-21' },
        // Add more posts here, 
      ],
      postsPerPage: 10,

      goToPostDetail(postId) {
      this.$router.push({ name: 'postDetail'  ,params: { id: postId }});
     
    },

      };
    },
    computed: {
      filteredPosts() {
      if (this.searchQuery) {
        return this.posts.filter(post => post.title.includes(this.searchQuery));
      }
      return this.posts;
    },
    totalPages() {
      return Math.ceil(this.filteredPosts.length / this.postsPerPage);
    },
    paginatedPosts() {
      const start = (this.currentPageNumber - 1) * this.postsPerPage;
      const end = start + this.postsPerPage;
      return this.filteredPosts.slice(start, end);
    }
  },
  
    methods: {
      addPost() {
        if (this.newPostTitle && this.newPostContent) {
          this.posts.push({
            id: Date.now(),
            title: this.newPostTitle,
            content: this.newPostContent
          });
          this.newPostTitle = '';
          this.newPostContent = '';
        }
      },
      setPage(page) {
      this.currentPage = page;
      },
      selectTab(tab) {
      this.currentTab = tab;

    },
    goHome() {
      this.$router.push('/home');
     },
    prevPage() {
      if (this.currentPageNumber > 1) {
        this.currentPageNumber--;
      }
    },
    nextPage() {
      if (this.currentPageNumber < this.totalPages) {
        this.currentPageNumber++;
      }
    }
    
    }
  }
  
  </script>
  
  <style scoped>
  
  .forum-container {
    display: flex;
   flex-direction: column;
   min-height: 100vh;
  }
  
  /* 设置背景图像 */
  .forum-container {
   
    background-size: cover;
    height: 100vh; /* 使背景覆盖整个视口 */
    display: flex;
    align-items: center;
    justify-content: center;
  }
  
  
  .forum-content {
  background-color: white;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  padding: 20px;
  width: 1000px;
  max-width: 1600px;
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


  /* 下面是复制的 */
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


  /* 上面是复制的 */



  /* 退回主页 */
  .btn-primary {
  padding: 8px 16px;
  background-color: #288628;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-primary:hover {
  background-color: #555;
}


  .post-form, .posts {
    margin-bottom: 20px;
  }
  
  input, textarea {
    width: 100%;
    padding: 10px;
    margin: 10px 0;
    border: 1px solid #ddd;
    border-radius: 4px;
  }
  
  button {
    padding: 10px 20px;
    background-color: #28a745;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }
  
  
  .top-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  background-color: #f4f4f4;
  border-bottom: 1px solid #ddd;
  position: fixed;
  top: 70px; /* 根据需要调整 */
  width: 980px;
  }
  
  
  .logo1 {
  height: 50px;
  }
  
  .search-container {
    display: flex;
    align-items: center;
}

  .search-box {
  width: 300px;
  padding: 5px;
  border: 1px solid #ddd;
  border-radius: 4px;
  }

  .search-button {
    padding: 5px 15px;
    border: none;
    background-color: #288628;
    color: white;
    border-radius: 4px;
    cursor: pointer;
    font-size: 16px;
}

.search-button:hover {
    background-color: #555;
}

.search-input {
    margin-right: 100px; /* 设置搜索框和按钮之间的间距 */
}

  
  nav {
  background-color: #288628;
  position: fixed;
  top: 140px; /* 根据需要调整 */
  width: 1000px;
  }
  
  nav ul {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  
  }
  
  nav ul li {
  margin: 0;
  }
  
  nav ul li a {
  display: block;
  padding: 12px 24px;
  color: #fff;
  text-decoration: none;
  }
  
  nav ul li a:hover {
  background-color: #555;
  }
  
  nav ul li.active a {
  background-color: #0cc140; /* Highlight color for the active item */
  }
  
  
  .postcontainer {
  margin-top: 150px; /* 设置距离页面顶部的距离 */
  height: 60vh; /* 页面高度的50% */
  overflow-y: auto; /* 允许垂直滚动 */
  border: 1px solid #ddd; /* 可选的边框样式 */
}

.post {
  border: 1px solid #ddd;
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 4px;
  cursor: pointer; /* 显示点击指示符 */
}

.post-list {
  margin-top: 20px;
}

.post {
  border-bottom: 1px solid #ddd;
  margin-bottom: 10px;
  padding-bottom: 10px;
}

.post h2 {
  margin: 0;
}

.post .content {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.post .footer {
  display: flex;
  justify-content: space-between;
  font-size: 0.8em;
}

.pagination {
  text-align: center;
  margin-top: 20px;
}

.pagination button {
  margin: 0 5px;
}
  
  
  
  .footer {
  display: flex; /* 使用 Flexbox 布局 */
  justify-content: space-between; /* 左右两端对齐 */
  align-items: center; /* 垂直居中对齐 */
  padding: 0px 20px; /* 左右内边距 */
  background-color: #46b476cc; /* 背景颜色 */
  font-size: 15px; /* 字体大小 */
  color: white; /* 字体颜色 */
  position: relative; /* 为了确保子元素绝对定位不会影响其他元素 */
  width: 100%; /* 确保footer占满整个宽度 */
  box-sizing: border-box; /* 包括内边距和边框在内的总宽度 */
  }
  
  .footer .left {
  flex: 1; /* 左侧元素占用剩余空间 */
  /* 可以添加其他样式 */
  }
  
  .footer .center {
  text-align: center; /* 居中对齐 */
  flex: 1; /* 使中心部分占据剩余空间 */
  }
  
  .footer .right {
  flex: 1; /* 右侧占位元素占用剩余空间，保持居中 */
  /* 可以添加其他样式 */
  }
  
  .footer a {
  color: white; /* 链接颜色 */
  text-decoration: none; /* 去除下划线 */
  }
  
  .footer a:hover {
  text-decoration: underline; /* 鼠标悬停时显示下划线 */
  }
  
  
  </style>
  