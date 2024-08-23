<template>
    <div class="forum-container">
        <!-- 视频容器 -->
    <div id="videoContainer">
        <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
          <source src="../assets/video/background.mp4" type="video/mp4">
        </video>
    </div>
  
    <header class="header">
      
    </header>
  
    <main class="main-content">
      <div class="forum-content">
        <div class="top-bar">
      <img src=".\images\logo.png" alt="Logo" class="logo" />
      <input type="text" placeholder="搜索..." class="search-box" />
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
        // Add more posts here
      ],
      postsPerPage: 10,
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
  top: 40px; /* 根据需要调整 */
  width: 980px;
  }
  
  
  .logo {
  height: 50px;
  }
  
  .search-box {
  width: 300px;
  padding: 5px;
  border: 1px solid #ddd;
  border-radius: 4px;
  }
  
  nav {
  background-color: #288628;
  position: fixed;
  top: 110px; /* 根据需要调整 */
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
    margin-top: 140px; /* 设置距离页面顶部的距离 */
      height: 70vh; /* 页面高度的50% */
      overflow-y: auto; /* 允许垂直滚动 */
      border: 1px solid #ddd; /* 可选的边框样式 */
    }
  
  
  .post {
    border: 1px solid #ddd;
    padding: 10px;
    margin-bottom: 10px;
    border-radius: 4px;
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
  