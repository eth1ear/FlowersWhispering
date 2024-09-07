<template>
  <Header />
  <div class="container" v-if="currentUser">
    <!-- 视频背景 -->
    <video autoplay muted loop class="background-video">
      <source src="@/assets/video/background.mp4" type="video/mp4">
    </video>

    <div class="content-wrapper">
      <!-- 左侧侧边栏 -->
      <div class="left-panel">
        <div class="avatar-container" @click="goToUserProfile">
          <img :src="currentUser.avatar" alt="用户头像" class="avatar" />
        </div>
        <hr class="divider"> <!-- 分割线 -->
        <nav class="nav">
          <ul>
            <li 
              :class="{ active: selectedText === '热门帖子' }" 
              @click="selectOption('热门帖子')"
            >
              热门帖子
            </li>
            <li 
              :class="{ active: selectedText === '我的收藏' }" 
              @click="selectOption('我的收藏')"
            >
              我的收藏
            </li>
            <li 
              :class="{ active: selectedText === '我的评论' }" 
              @click="selectOption('我的评论')"
            >
              我的评论
            </li>
             <li 
              :class="{ active: selectedText === '发布帖子' }" 
              @click="selectOption('发布帖子')"
            >
              发布帖子
            </li>
          </ul>
        </nav>
      </div>

      <!-- 右侧内容展示 -->
      <div class="right-panel">
        <!-- 帖子列表 -->
        <div v-if="selectedText === '热门帖子'">
          <!-- 热门帖子列表 -->

    <!-- 搜索框 -->
    <div class="search-bar">
      <input v-model="searchQuery" placeholder="搜索帖子..." />
      <button @click="performSearch" class="search-button">
        <span class="search-icon">🔍</span> 搜索
      </button>
    </div>
     
          <div v-for="post in paginatedPosts" :key="post.article_id" class="post" @click="openPostDialog(post)">
            <div class="post-title">{{ post.article_title }}</div>
            <div class="post-content">{{ post.article_content }}</div>
            <div class="post-meta">{{ post.published_date }}</div>
          </div>

          <!-- 分页控件 -->
          <div class="pagination">
            <span class="pagination-arrow" @click="prevPage" :class="{ disabled: currentPage === 1 }">&lt;</span>
            <span>第 {{ currentPage }} 页 / 共 {{ totalPages }} 页</span>
            <span class="pagination-arrow" @click="nextPage" :class="{ disabled: currentPage === totalPages }">&gt;</span>
            <input v-model="inputPage" @keyup.enter="goToPage" placeholder="跳转页数" />
          </div>
        </div>
        <!-- 收藏列表 -->
        <div v-if="selectedText === '我的收藏'">
  <!-- 搜索框 -->
    <div class="search-bar">
      <input v-model="searchQuery" placeholder="搜索帖子..." />
      <button @click="performSearch" class="search-button">
        <span class="search-icon">🔍</span> 搜索
      </button>
    </div>
    <!-- 收藏列表 -->
<div v-for="favorite in favorites"
 :key="favorite.favorite_id" 
 class="post" 
 @click="openFavoriteDialog(favorite)"
 >
  <h3 class="post-title">{{ favorite.article_title }}</h3>
  <p class="post-content">{{ favorite.article_content }}</p>
  <p class="post-meta">发布日期: {{ favorite.published_date }}</p>
  <!-- 改为取消收藏按钮 -->
  <button class="cancelFavorite-button" @click.stop="confirmCancelFavorite(favorite)">取消收藏</button>
  </div>
        </div>
        <!-- 评论列表 -->
        <div v-if="selectedText === '我的评论'">
        <div v-for="comment in comments" :key="comment.commentId" class="post">
         <h3 class="comment-title">{{comment.content }}</h3>
 <!-- 评论底部信息：评论者和时间在左下角，帖子标题在右下角 -->
    <div class="comment-meta">
      <div class="comment-left">
        <span>{{ comment.username }}</span>
        <span>&nbsp;|&nbsp;</span>
        <span>{{ comment.createdDate }}</span>
      </div>
      <div class="comment-right">
        <span>{{ comment.title }}</span>
      </div>
    </div>
  </div>
        </div>

   
<!-- 发布评论 -->
   <div v-if="selectedText === '发布帖子'">
     <div class="post-form">
      <label for="postTitle">帖子标题:</label>
      <input id="postTitle" v-model="newPostTitle" placeholder="输入帖子标题..." />
      <hr>
      <label for="postContent">帖子内容:</label>
      <textarea id="postContent" v-model="newPostContent" placeholder="输入帖子内容..."></textarea>
      <button @click="submitNewPost" class="submit-post-button">发布帖子</button>
      <button @click="cancelNewPost" class="cancel-post-button">放弃发布</button>
    </div>
   </div>

      </div>
       <!-- 帖子详情弹框 -->
   <div class="post-dialog" v-if="showPostDialog">
    <div class="post-dialog-content">
      <!-- 标题居中 -->
      <div class="post-dialog-header">
        <div class="left">
             <!--这里需要改逻辑，如果后端传来的这个是已经收藏的帖子，那么就显示取消收藏并且给出另一个按钮 用一个v-if渲染-->
          <div v-if="isFavorited===false">
        <button class="addFavorite-button" @click="addToFavorite">添加收藏</button>
        </div> 
        </div>
     
        <div class="center">
        <h2 class="post-title">{{ selectedPost.article_title }}</h2>
        </div>
         <div class="right">
        <button class="close-button" @click="closePostDialog">×</button>
        </div>
      </div>
      
      <!-- 作者和发布日期放在左上角 -->
      <div class="post-dialog-meta">
        <p>{{ selectedPost. selectedPostUsername }} | 发布日期: {{ selectedPost.published_date }}</p>
      </div>

      <!-- 帖子内容显示区域，增大内容区域高度 -->
      <div class="post-dialog-body">
        <p>{{ selectedPost.article_content }}</p>
      </div>

      <!-- 评论区 -->
      <div class="post-dialog-footer">
        <h4>评论区</h4>
        <div class="comment-list">
          <div v-for="comment in comments" :key="comment.id" class="comment-item">
            {{ comment.text }}
          </div>
        </div>

        <!-- 输入评论框，固定在评论区的最下方 -->
        <div class="comment-input">
          <input v-model="newComment" placeholder="参与评论..." />
          <button @click="submitComment" class="submit-button">提交评论</button>
        </div>
      </div>
    </div>
  </div>
        <!-- 取消收藏确认弹框 -->
  <div v-if="showConfirmFavoriteDialog" class="confirm-dialog">
  <div class="confirm-dialog-content">
    <p>确定要取消收藏该帖子吗？</p>
    <button @click="cancelFavorite(selectedFavorite)">确认</button>
    <button @click="cancelDialog">取消</button>
  </div>
</div>

      </div>
    </div>
  <Footer />
</template>




<script>
import { mapGetters } from 'vuex';
import Header from '@/home/Header.vue';
import Footer from '@/home/Footer.vue';

export default {
  components: {
    Header,
    Footer,
  },
  computed: {
    ...mapGetters({
      currentUser: 'getUserInfo', // 获取当前用户信息
    }),
     totalPages() {
      return Math.ceil(this.posts.length / this.pageSize); // 动态计算总页数
    },
  paginatedPosts() {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.posts.slice(start, end); // 确保返回的是根据 pageSize 切分的帖子
    },
    //搜索帖子功能，接入后端！！！目前给的这filter会报错，接口处理吧
    performSearch() {
      // // 根据搜索框内容过滤帖子
      // this.posts = this.originalPosts.filter(post => {
      //   return post.article_content.includes(this.searchQuery) || post.article_title.includes(this.searchQuery);
      // });
      // this.currentPage = 1; // 重置到第一页
    },
  },
  data() {
    return {
      newPostTitle: '',//新发布帖子的标题
      newPostContent: '',//新发布帖子的内容
      searchQuery: '', // 搜索框绑定的输入内容
      selectedText: '发布帖子', // 默认显示热门帖子
      showPostDialog: false, // 控制帖子详情弹框显示
      selectedPost: null, // 当前选中的帖子
      newComment: '', // 存储用户输入的新评论
      comments: [], // 模拟的评论数据
      selectedPostUsername: "作者名称",
      currentPage: 1,
      pageSize: 5, // 每页显示的帖子数
      isFavorited: false,//默认没收藏的
      inputPage: '',  // 跳转页码输入
      showConfirmFavoriteDialog: false,
      posts: [  // 示例帖子数据
        {
          article_id: 1,
          article_title: "热门帖子1",
          article_content: "这是第一个热门帖子的内容。这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容这是第一个热门帖子的内容",
          published_date: "2024-09-07"
        },
        {
          article_id: 2,
          article_title: "热门帖子2",
          article_content: "这是第二个热门帖子的内容。",
          published_date: "2024-09-06"
        },
        {
          article_id: 3,
          article_title: "热门帖子3",
          article_content: "这是第三个热门帖子的内容。",
          published_date: "2024-09-05"
        },
        {
          article_id: 4,
          article_title: "热门帖子4",
          article_content: "这是第四个热门帖子的内容。",
          published_date: "2024-09-04"
        },
        {
          article_id: 5,
          article_title: "热门帖子5",
          article_content: "这是第一个热门帖子的内容。",
          published_date: "2024-09-07"
        },
        {
          article_id: 6,
          article_title: "热门帖子6",
          article_content: "这是第二个热门帖子的内容。",
          published_date: "2024-09-06"
        },
        {
          article_id: 7,
          article_title: "热门帖子7",
          article_content: "这是第三个热门帖子的内容。",
          published_date: "2024-09-05"
        },
        {
          article_id: 8,
          article_title: "热门帖子8",
          article_content: "这是第四个热门帖子的内容。",
          published_date: "2024-09-04"
        },
             {
          article_id: 9,
          article_title: "热门帖子9",
          article_content: "这是第一个热门帖子的内容。",
          published_date: "2024-09-07"
        },
        {
          article_id: 10,
          article_title: "热门帖子10",
          article_content: "这是第二个热门帖子的内容。",
          published_date: "2024-09-06"
        },
      ], // 帖子数据
       favorites: [{
        favorite_id: 1,
        article_id: 101,
        article_title: "收藏的帖子1",
        article_content: "这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。这是第一个示例帖子的内容,展示数据库。",
        published_date: "2024-09-06"
      },
      {
        favorite_id: 2,
        article_id: 102,
        article_title: "收藏的帖子2",
        article_content: "这是第二篇收藏的帖子内容。",
        published_date: "2024-09-05"
      }
      ],
       comments: [{
        commentId: 1,
        userId: 101,
        articleId: 201,
        content: "这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。这是对文章的评论内容。",
        createdDate: "2024-09-07",
        username: "张三",
        title: "如何学习 Vue.js"
      },
      {
        commentId: 2,
        userId: 102,
        articleId: 202,
        content: "这篇文章写得很好！",
        createdDate: "2024-09-06",
        username: "李四",
        title: "JavaScript 的基本概念"
      }
      ],
    };
  },
  methods: {
    selectOption(option) {
      this.selectedText = option; // 切换选项
    },
    openPostDialog(post) {
      this.selectedPost = post; // 保存当前选中的帖子
      this.showPostDialog = true; // 显示详情弹框
      this.comments = [ // 模拟评论数据
        { id: 1, text: "这是第一条评论！" },
        { id: 2, text: "非常有趣的帖子，期待更多！" }
      ];
    },
    closePostDialog() {
      this.showPostDialog = false; // 关闭弹框
      this.selectedPost = null; // 重置选择的帖子
    },
    submitComment() {
      if (this.newComment) {
        this.comments.push({ id: this.comments.length + 1, text: this.newComment });
        this.newComment = ''; // 清空输入框
      }
    },
    goToUserProfile() {
      this.$router.push('/userprofile'); // 跳转到个人主页
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    goToPage() {
      const page = parseInt(this.inputPage);
      if (page > 0 && page <= this.totalPages) {
        this.currentPage = page;
        this.inputPage = ''; // 清空输入框
      }
    },
    // 打开收藏的帖子详情，使用与帖子相同的弹框
  openFavoriteDialog(favorite) {
    this.selectedPost = favorite; // 将选中的收藏帖子数据传递给 selectedPost
    this.comments = [ /* 模拟或者获取实际的评论数据 */ ];
    this.showPostDialog = true; // 显示弹框
  },
  // 确认取消收藏操作
  confirmCancelFavorite(favorite) {
    this.selectedFavorite = favorite;
    this.showConfirmFavoriteDialog = true;
  },
  
  // 取消收藏操作
  cancelFavorite(favorite) {
    this.favorites = this.favorites.filter(fav => fav.favorite_id !== favorite.favorite_id); // 移除收藏
    this.showConfirmFavoriteDialog = false;
    this.selectedFavorite = null;
  },
  // 关闭弹框
  cancelDialog() {
    this.showConfirmFavoriteDialog = false;
    this.selectedFavorite = null;
    },
   submitNewPost() {
    if (this.newPostTitle.trim() === '' || this.newPostContent.trim() === '') {
      alert('请填写完整的帖子标题和内容。');
      return;
    }
    
    // 这里可以添加代码来提交新帖子，例如发送请求到后端
    console.log('发布帖子:', this.newPostTitle, this.newPostContent);

    // 清空输入框
    this.newPostTitle = '';
    this.newPostContent = '';
  },
  cancelNewPost() {
    // 清空输入框并返回
    this.newPostTitle = '';
    this.newPostContent = '';
    this.selectedText = '热门帖子'; // 或者根据实际需要重置为其他状态
  },
  },
};
</script>


<style scoped>
.avatar-container {
  position: relative;
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
  cursor: pointer;
   transition: transform 0.3s ease; /* 添加过渡效果 */
}

.avatar {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  object-fit: cover;
  transition: all 0.3s ease;
}
.avatar-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100px;
  height: 100px;
  border-radius: 50%;
  background-color: rgba(0, 0, 0, 0.6);
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
  transform: scale(1.1); /* 悬停时放大1.1倍 */
}

.avatar-container:hover .avatar-overlay {
  opacity: 1;
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

.background-video {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  z-index: -1; 
}


.content-wrapper {
  position: relative;
  display: flex;
  justify-content:flex-start;
  align-items: flex-start;
  width: 80%;
  height: 100%;
  margin: 0 auto;
  z-index: 2;
  margin-top:80px;
}

.left-panel{
  flex: 1;
  display: column;
  justify-content: center;
  align-items: flex-start;
  background-color: rgba(255, 255, 255, 0.7);
  padding: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  height: 80%;
  max-width:10%;
  z-index: 2;
  margin-bottom: 80px; /* 与底部之间的距离 */

}

.right-panel {
  flex: 1;
  display: flex;
  background-color: rgba(211, 211, 211, 0.7);
  flex-direction: column;   /* 将帖子垂直排列 */
  align-items: flex-start;
  justify-content: flex-start; /* 让内容从顶部开始对齐 */
  padding: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  height: 80%;              /* 固定高度 */
  margin-bottom: 80px;
  z-index: 2;
}
.nav ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.nav li {
  max-height:60px;
  margin: 0; /* 移除上下间距 */
  cursor: pointer;
  padding: 20px 0; /* 调整上下内边距 */
  text-align: center;
  font-size: 18px; /* 默认文字大小 */
  transition: background-color 0.3s ease, opacity 0.3s ease, font-size 0.3s ease, border-radius 0.3s ease;
  opacity: 0.7; /* 默认透明度 */
  color: #000; /* 默认字体颜色 */
  position: relative; /* 让伪元素能正确定位 */
  border-radius: 0; /* 初始状态无圆角 */
}

.nav li:hover, .nav li.active {
  background-color: rgba(69, 199, 137, 0.9); /* 选中时背景为绿色 */
  color: white; /* 选中时字体变白 */
  font-size: 22px; /* 选中时字体变大 */
  opacity: 1; /* 选中时取消透明度 */
  border-radius: 15px; /* 悬停时边缘变圆，半径可根据需求调整 */
}

.nav li:hover {
  opacity: 1; /* 悬停时取消透明度 */
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  position: fixed;
  bottom: 20px;
  left: 55%;
  transform: translateX(-50%);
  bottom:8%;
  font-size: 16px;
}

.pagination-arrow {
  cursor: pointer;
  padding: 10px;
  margin: 0 10px;
}

.pagination-arrow.disabled {
  color: gray;
  cursor: not-allowed;
}

input {
  width: 60px;
  margin-left: 20px;
  text-align: center;
  padding: 5px;
  border: 1px solid #ccc;
  border-radius: 5px;
}



.edit-actions {
  display: flex;
  justify-content: center; /* 居中对齐容器内的按钮 */
  gap: 20px; /* 增加按钮之间的间距 */
  width: 260px; /* 容器的宽度设置为按钮的总宽度加上间距 */
}

.logout-button-container {
  display: flex;
  justify-content: center;
  margin-top: auto; /* 让按钮靠下 */
  margin-bottom: 20px;
}

.logout-button {
  background-color: #ff4d4d;
  width: 136px; /* 调整宽度为长方形 */
  height:35px;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size:16px;
  transition: background-color 0.3s ease, transform 0.2s ease;
  margin-top:20px;
}

.logout-button:hover {
  background-color: #ff1a1a;
  transform: scale(1.05);
}

.logout-button:active {
  background-color: #cc0000;
  transform: scale(1);
}

/* 新增的样式 */
.edit-button-container {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}

.edit-button {
  background-color: #ffc107;
  color: #fff;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.edit-button:hover {
  background-color: #e0a800;
  transform: scale(1.05);
}

.edit-actions {
  display: flex;
  justify-content: center; /* 水平居中对齐 */
  width: 100%;
  margin-top: 10px;
}

/* 头像选择弹出框相关样式 */
.avatar-selection-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.avatar-selection-container {
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  width: 400px;
  text-align: center;
}

.avatar-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
  margin-top: 20px;
}

.avatar-option {
  position: relative;
  cursor: pointer;
}

.avatar-image {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  border: 2px solid transparent;
  transition: all 0.3s ease;
}

.avatar-option.selected .avatar-image {
  border-color: #007bff;
  filter: brightness(0.7);
}

.checkmark {
  position: absolute;
  top: 5px;
  right: 5px;
  font-size: 18px;
  color: white;
  background-color: #007bff;
  border-radius: 50%;
  width: 20px;
  height: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.error-message {
  color: red;
  font-size: 12px;
  margin-top: 2px;
  margin-left: 5px;  /* 保持与输入框对齐 */
}

.validation-feedback {
  font-size: 12px;
  color: green; /* 默认成功信息颜色 */
  display: block; /* 确保反馈信息占据整行 */
}

.validation-feedback.error {
  color: red; /* 错误信息颜色 */
}

.validation-feedback.success {
  color: green; /* 成功信息颜色 */
}


.user-info {
    display:table-column;
    align-items: center;
    gap: 10px;
    position: relative;
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
.user-info-list {
  z-index: 2; /* 提高用户信息列表的层级，确保它显示在 header 之上 */
  position: absolute;
  left: -200px!important;
  top: 50px;
  right: 0;
  background-color: white; /* 为弹出的内容添加背景色，避免透明度导致内容不清晰 */
  padding: 10px;
  border-radius: 5px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); /* 添加阴影效果，确保弹出层次感 */
}
.username {
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    font-size: 28px;
    font-weight: bold;
}
.user-avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  cursor: pointer;
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

.footer {
  position:absolute;
}

.search-bar {
  display: flex;
  align-items: center;
  width: 100%;
  margin-bottom: 20px;
}

.search-bar input {
  flex: 1; /* 占据整个宽度 */
  padding: 10px;
  border: 1px solid #007BFF; /* 边框颜色改为偏蓝色 */
  background-color: #E3F2FD; /* 背景颜色为浅蓝色 */
  border-radius: 5px 0 0 5px;
  font-size: 16px;
}

.search-button {
  padding: 10px 20px;
  background-color: #007BFF; /* 按钮背景颜色为蓝色 */
  color: white;
  border: none;
  border-radius: 0 5px 5px 0;
  cursor: pointer;
  font-size: 16px;
  display: flex;
  align-items: center;
}

.search-button .search-icon {
  margin-right: 5px;
}

.search-button:hover {
  background-color: #0056b3; /* 悬停时的深蓝色 */
}

/*帖子*/
/* 帖子悬停变大效果 */
.post {
  background-color: white;
  padding: 15px;
  margin-bottom: 5px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  max-width:94%;
  max-height: 150px;
  box-sizing: border-box;
  position: relative;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  cursor: pointer;
  z-index:3;
  margin-left:3.5%;
  margin-top:10px;
}

.post:hover {
  transform: scale(1.05);
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
}

.post-title {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 10px;
}

.post-content {
  font-size: 14px;
  color: #333;
  margin-bottom: 10px;
  max-height: 80px;          /* 限制最大高度 */
  overflow: hidden;          /* 隐藏超出的内容 */
  display: -webkit-box;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 2;     /* 限制行数为3，超出部分显示省略号 */
  white-space: normal;       /* 允许换行 */
}


.post-meta {
  font-size: 12px;
  color: #666;
  text-align: right;
}

.comment-title{
  font-size: 14px;
  color: #333;
  margin-bottom: 20px;
  max-height: 80px;          /* 限制最大高度 */
  overflow: hidden;          /* 隐藏超出的内容 */
  display: -webkit-box;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 3;     /* 限制行数为3，超出部分显示省略号 */
  overflow-y: auto;
  white-space: normal;       /* 允许换行 */
}
/* 删除按钮样式 */
.cancelFavorite-button,
.delete-button {
  position: absolute;
  right: 20px;
  top: 25%;
  transform: translateY(-50%);
  background-color: red;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
}

.cancelFavorite-button{
  background-color: #ffc107;
}

.delete-button:hover {
  background-color: darkred;
}

/* 删除确认弹框样式 */
.confirm-dialog {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
  z-index: 1000;
}

.confirm-dialog-content {
  text-align: center;
}

.confirm-dialog-content button {
  margin: 10px;
  padding: 5px 15px;
  cursor: pointer;
}

.confirm-dialog-content button {
  margin: 10px;
  padding: 10px 20px;
  cursor: pointer;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  transition: background-color 0.3s ease, color 0.3s ease, box-shadow 0.3s ease;
}

.confirm-dialog-content button {
  background-color: #4CAF50; /* 确认按钮的绿色 */
  color: white;
}

.confirm-dialog-content button:hover {
  background-color: #45a049;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.confirm-dialog-content button:last-child {
  background-color: #f44336; /* 取消按钮的红色 */
  color: white;
}

.confirm-dialog-content button:last-child:hover {
  background-color: #e53935;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

/* 帖子详情弹框样式 */
.post-dialog {
  position: fixed;
  top: 15%;
  left: 50%;
  transform: translateX(-50%);
  width: 60%;
  max-width: 900px;
  background-color: white;
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
  z-index: 1000;
  overflow: hidden;
}

.post-dialog-header {
  display: flex;
}
.post-dialog-header .center{
  padding: 20px;
  text-align: center; /* 标题居中 */
  font-size: 24px;
  font-weight: bold;
  background-color: #f5f5f5;
  border-bottom: 1px solid #ddd;
  position: relative;
  flex: 1; /* 平均分配宽度 */
}
.post-dialog-header .left{
  padding: 20px;
  text-align: center; /* 标题居中 */
  font-size: 24px;
  font-weight: bold;
  background-color: #f5f5f5;
  border-bottom: 1px solid #ddd;
  position: relative;
  flex: 1; /* 平均分配宽度 */
}
.post-dialog-header .right{
  padding: 20px;
  text-align: center;
  font-size: 24px;
  font-weight: bold;
  background-color: #f5f5f5;
  border-bottom: 1px solid #ddd;
  position: relative;
  flex: 1; /* 平均分配宽度 */
}
.addFavorite-button{
  position: relative;
  margin-left: auto; /* 将按钮推到最右侧 */
  margin-top:25%;
  transform: translateY(-50%);
  background-color: red;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
}

.addFavorite-button{
  background-color: #ffc107;
}
.post-dialog-meta {
  padding: 10px 20px;
  text-align: left; /* 左上角显示 */
  font-size: 14px;
  color: #666;
  max-height:3px;
}
.close-button {
  padding: 5px 10px;
  font-size: 40px; /* 设置字体大小，让叉叉明显 */
  background-color: transparent; /* 去掉背景色 */
  color: #007bff; /* 设置叉叉的颜色 */
  border: none;
  cursor: pointer;
  transition: color 0.3s ease;
  position:relative;
  margin-right:5%;
}

.close-button:hover {
  color: #0056b3; /* 悬停时变成深蓝色 */
}

.post-dialog-body {
  padding: 10px;
  margin-top:30px;
  height:180px;
  max-height: 300px; /* 增大内容展示区域高度 */
  overflow-y: auto; /* 内容超出时滚动 */
  font-size: 16px;
}

.post-dialog-footer {
  padding: 20px;
  background-color: #f5f5f5;
  border-top: 1px solid #ddd;
  display: flex;
  flex-direction: column;
}

.comment-list {
  max-height: 150px; /* 评论区的最大高度 */
  overflow-y: auto; /* 评论区带滚动条 */
  margin-bottom: 10px;
}

.comment-item {
  padding: 10px;
  background-color: #eee;
  border-radius: 5px;
  margin-bottom: 5px;
}

.comment-input {
  display: flex;
  align-items: center;
  margin-top: 10px;
  margin-left: -2%;
}

.comment-input input {
  flex: 1;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  margin-right: 10px;
  text-align: left; /* 确保文本从左边开始输入 */
}

.submit-button {
  background-color: red;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.submit-button:hover {
  background-color: darkred;
}
.post-form {
  background-color: rgba(255, 255, 255, 0.9);
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 100%; /* 占满整个右侧面板 */
  max-width: none; /* 取消最大宽度限制 */
  margin: 0; /* 清除外边距 */
}

.post-form {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: stretch;
  flex: 1;
  width: 100%;
  height: 100%;
  padding: 20px;
  box-sizing: border-box;
}

.post-form label,
.post-form input,
.post-form textarea,
.post-form button {
  margin-bottom: 10px;
  width: 100%;
}

.post-form textarea {
  flex-grow: 1;
  resize: none; /* 禁止调整大小 */
}

.button-group {
  display: flex;
  justify-content: space-between;
}

.submit-post-button,
.cancel-post-button {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 10px 15px;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.cancel-post-button {
  background-color: #dc3545;
}

.submit-post-button:hover {
  background-color: #218838;
}

.cancel-post-button:hover {
  background-color: #c82333;
}

.dropdown-container {
  position: relative;
  display: inline-block;
  background-color: rgba(255, 255, 255, 0.5); /* 透明背景 */
  padding: 10px;
  border-radius: 5px;
  cursor: pointer;
  text-align: right;
  z-index: 4;
  margin-left:16px;
}

.dropdown-text {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.triangle {
  margin-left: 10px;
  font-size: 16px;
  cursor: pointer;
  transition: transform 0.3s ease;
}

.triangle-rotate {
  transform: rotate(180deg); /* 点击后旋转三角形 */
}

.dropdown-menu {
  position: absolute;
  top: 100%;
  left: 0;
  background-color: white;
  border: 1px solid #ccc;
  border-radius: 5px;
  width: 100%;
  z-index: 1;
  list-style: none;
  padding: 10px 0;
  margin: 0;
  z-index:5;
}

.dropdown-menu li {
  padding: 10px;
  text-align: left;
  cursor: pointer;
  transition: background-color 0.3s;
}

.dropdown-menu li:hover {
  background-color: #f0f0f0;
}

.selectedoption {
  width:100%;
  margin-left:1px;
  overflow-y: auto;          /* 当内容溢出时显示滚动条 */
  overflow-x: hidden;         /* 当内容溢出时显示滚动条 */
}

.divider {
  border: none;
  min-height: 3px;
  width:100%;
  background-color: #45c789; /* 天蓝色 */
  z-index:3;
}

.comment-meta {
  display: flex;
  justify-content: space-between;  /* 左右分布 */
  font-size: 12px;
  color: #666;
}

.comment-left {
  display: flex;
}
.comment-right {
  text-align: right;
  font-weight: bold;
}
.footer{
  position:fixed;
  left:0;
}
</style>
