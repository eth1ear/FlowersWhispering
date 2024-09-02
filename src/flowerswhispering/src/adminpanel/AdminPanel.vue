<template>
    <div class="admin-panel-page">



      <!-- 侧边栏导航 -->
      <aside class="top-navbar">
        <nav>
          <ul>
            <li @click="currentView = 'users'" :class="{ active: currentView === 'users' }">用户管理</li>
            <li @click="currentView = 'posts'" :class="{ active: currentView === 'posts' }">帖子管理</li>
            <li @click="currentView = 'comments'" :class="{ active: currentView === 'comments' }">评论管理</li>
            <li @click="currentView = 'plants'" :class="{ active: currentView === 'plants' }">植物审核</li>
            <li @click="currentView = 'feedbacks'" :class="{ active: currentView === 'feedbacks' }">用户反馈</li>
            <li @click="currentView = 'announcements'" :class="{ active: currentView === 'announcements' }">公告发布</li>  
            <li @click="GoToHome">返回主页</li>       
          </ul>
        </nav>
      </aside>
      
      <!-- 主内容区域 -->
      <main class="main-content">
        <!-- 用户管理视图 -->
        <div v-if="currentView === 'users'">
          <h2>用户管理</h2>
          <button @click="toggleAddUserForm" class="btn-toggle-form my-animation-slide-top">
            {{ showAddUserForm ? '取消' : '添加用户' }}
          </button>
  
          <form v-if="showAddUserForm" @submit.prevent="handleAddUser" class="form-container my-animation-slide-bottom">
            <input v-model="newUser.username" placeholder="用户名" class="form-input" />
            <input v-model="newUser.email" placeholder="邮箱" class="form-input" />
            <input v-model="newUser.password" type="password" placeholder="密码" class="form-input" />
            <input v-model="newUser.role" placeholder="角色" class="form-input" /> 
            <button type="submit" class="btn-submit">添加用户</button>
          </form>
  
          <ul class="item-list">
            <li v-for="user in users" :key="user.username" class="item">
              <div class="item-details">
                <p><strong>用户名:</strong> {{ user.username }}</p>
                <p><strong>邮箱:</strong> {{ user.email }}</p>
                <p><strong>角色:</strong>
                <span v-if="!user.editing">{{ user.role }}</span>
                <select v-else v-model="user.newRole" class="form-input">
                  <option value="user">user</option>
                  <option value="admin">admin</option>
                </select>
                </p>
              </div>
              <div class="item-actions">
                <button v-if="!user.editing" @click="editUser(user)" class="btn-edit my-animation-slide-bottom">修改</button>
                <button v-else @click="saveUser(user)" class="btn-submit my-animation-slide-bottom">保存</button>
              </div>
            </li>
          </ul>
        </div>
  
        <!-- 帖子管理视图 -->
        <div v-if="currentView === 'posts'">
          <h2>帖子管理</h2>
          <ul class="item-list">
            <li v-for="post in posts" :key="post.id" class="item">
              <div class="item-details">
                <p><strong>标题:</strong> {{ post.title }}</p>
                <button @click="toggleExpandPost(post)" class="btn-details my-animation-slide-top">
                  {{ expandedPost === post ? '收起' : '展开' }}内容
                </button>
              </div>
  
              <div v-if="expandedPost === post" class="expanded-content my-animation-slide-bottom">
                <p>{{ post.content }}</p>
              </div>
  
              <div class="item-actions">
                <button @click="deletePost(post.id)" class="btn-delete my-animation-slide-bottom">删除</button>
              </div>
            </li>
          </ul>
        </div>
  
        <!-- 评论管理视图 -->
        <div v-if="currentView === 'comments'">
          <h2>评论管理</h2>
          <ul class="item-list">
            <li v-for="comment in comments" :key="comment.id" class="item">
              <div class="item-details">
                <p><strong>评论内容:</strong> {{ comment.content }}</p>
                <p><strong>所属帖子ID:</strong> {{ comment.postId }}</p>
              </div>
              <div class="item-actions">
                <button @click="handleDeleteComment(comment.id)" class="btn-delete my-animation-slide-bottom">删除</button>
              </div>
            </li>
          </ul>
        </div>
       <!-- 植物审核视图 -->
      <div v-if="currentView === 'plants'" class="plants-review-section">
        <!-- 已通过审核 -->
        <div class="plants-column">
          <h3>已通过审核</h3>
          <div class="table">
            <div class="table-header">
              <div>植物图片</div>
              <div>名称</div>
              <div>分类</div>
              <div>描述</div>
              <div>操作</div>
            </div>
            <div v-for="plant in approvedPlants" :key="plant.id" class="table-row">
              <div><img :src="plant.image" alt="植物图片" class="plant-image"></div>
              <div v-if="!plant.editing">{{ plant.name }}</div>
              <input v-else v-model="plant.name" class="form-input"/>

              <div v-if="!plant.editing">{{ plant.category }}</div>
              <input v-else v-model="plant.category" class="form-input"/>

              <div v-if="!plant.editing">{{ plant.description }}</div>
              <textarea v-else v-model="plant.description" class="form-input"></textarea>

              <div>
                <button v-if="!plant.editing" @click="editPlant(plant)" class="btn-edit">编辑</button>
                <button v-else @click="finishEditPlant(plant)" class="btn-submit">完成</button>
              </div>
            </div>
          </div>
        </div>

        <!-- 待审核 -->
        <div class="plants-column">
          <h3>待审核</h3>
          <div class="table">
            <div class="table-header">
              <div>植物图片</div>
              <div>名称</div>
              <div>分类</div>
              <div>描述</div>
              <div>操作</div>
            </div>
            <div v-for="plant in pendingPlants" :key="plant.id" class="table-row">
              <div><img :src="plant.image" alt="植物图片" class="plant-image"></div>
              <div v-if="!plant.editing">{{ plant.name }}</div>
              <input v-else v-model="plant.name" class="form-input"/>
              
              <div v-if="!plant.editing">{{ plant.category }}</div>
              <input v-else v-model="plant.category" class="form-input"/>
              
              <div v-if="!plant.editing">{{ plant.description }}</div>
              <textarea v-else v-model="plant.description" class="form-input"></textarea>
              
              <div>
                <button v-if="!plant.editing" @click="editPlant(plant)" class="btn-edit">编辑</button>
                <button v-else @click="finishEditPlant(plant)" class="btn-submit">完成</button>
                <button @click="approvePlant(plant)" class="btn-approve">通过</button>
                <button @click="deletePlant(plant.id)" class="btn-delete">删除</button>
              </div>
            </div>
          </div>
        </div>
      </div>
        
        <!-- 用户反馈视图 -->
        <div v-if="currentView === 'feedbacks'">
          <h2>用户反馈</h2>
          <ul class="item-list">
            <li v-for="feedback in feedbacks" :key="feedback.id" class="item">
              <div class="item-details">
                <p><strong>反馈标题:</strong> {{ feedback.title }}</p>
                <button @click="toggleExpandFeedback(feedback)" class="btn-details my-animation-slide-bottom">
                  {{ expandedFeedback === feedback ? '收起' : '展开' }}详情
                </button>
              </div>
              <div v-if="expandedFeedback === feedback" class="expanded-content my-animation-slide-bottom">
                <p>{{ feedback.content }}</p>
              </div>
            </li>
          </ul>
        </div>
  
        <!-- 公告管理视图 -->
        <div v-if="currentView === 'announcements'">
          <h2>公告管理</h2>
          <button @click="toggleAddAnnouncementForm" class="btn-toggle-form my-animation-slide-top">
            {{ showAddAnnouncementForm ? '取消' : '添加公告' }}
          </button>
  
          <form v-if="showAddAnnouncementForm" @submit.prevent="handleAddAnnouncement" class="form-container">
            <div class="form-group">
        <label for="announcement-title">标题</label>
        <input id="announcement-title" v-model="newAnnouncement.title" placeholder="请输入标题" class="form-input" />
      </div>
      <div class="form-group">
        <label for="announcement-content">内容</label>
        <textarea id="announcement-content" v-model="newAnnouncement.content" placeholder="请输入内容" class="form-input"></textarea>
      </div>
      <button type="submit" class="btn-submit">提交公告</button>
          </form>
  
          <ul class="item-list assignment">
            <li v-for="announcement in announcements" :key="announcement.id" class="item">
              <div class="item-details">
                <p><strong>标题:</strong> {{ announcement.title }}</p>
                <p><strong>发布日期:</strong> {{ announcement.date }}</p>
                <button @click="toggleExpandAnnouncement(announcement)" class="btn-details">
            {{ expandedAnnouncement === announcement ? '收起' : '展示' }}内容
          </button>
        </div>

        <div v-if="expandedAnnouncement === announcement" class="expanded-content">
          <p>{{ announcement.content }}</p>
        </div>
              <div class="item-actions">
                <button @click="handleDeleteAnnouncement(announcement.id)" class="btn-delete my-animation-slide-bottom">删除</button>
              </div>
            </li>
          </ul>
        </div>  

      </main>
    </div>
</template>
  
<script>
  import { mapActions, mapGetters } from 'vuex';
  
  export default {
    name: 'AdminPanel',
    data() {
      return {
        currentView: 'users',
        showAddUserForm: false,
        showAddAnnouncementForm: false,
        expandedPost: null,
        expandedFeedback: null,
        expandedAnnouncement: null, // 当前展开的公告
        newUser: {
          username: '',
          email: '',
          password: '',
          role: ''
        },
        newAnnouncement: {
          title: '',
          content: ''
        }
      };
    },
    computed: {
      ...mapGetters(['allUsers', 'allPosts', 'allComments', 'allFeedbacks', 'allAnnouncements','allPlants']),
      users() {
        return this.allUsers;
      },
      posts() {
        return this.allPosts;
      },
      comments() {
        return this.allComments;
      },
      feedbacks() {
        return this.allFeedbacks;
      },
      announcements() {
        return this.allAnnouncements;
      },
      approvedPlants() {
      return this.allPlants.filter(plant => plant.approved);
    },
      pendingPlants() {
      return this.allPlants.filter(plant => !plant.approved);
    }
    },
    methods: {
      ...mapActions(['addUser', 'deleteUser', 'updateUser', 'deletePost', 'deleteComment', 'deleteAnnouncement', 'addAnnouncement']),
      
      toggleExpandAnnouncement(announcement) {
      if (this.expandedAnnouncement === announcement) {
        this.expandedAnnouncement = null;
      } else {
        this.expandedAnnouncement = announcement;
      }
    },
      toggleAddUserForm() {
        this.showAddUserForm = !this.showAddUserForm;
      },
      handleAddUser() {
        if (this.newUser.username && this.newUser.email && this.newUser.password) {
          this.addUser({ ...this.newUser });
          this.newUser = { username: '', email: '', password: '', role: '' };
          this.showAddUserForm = false;
        }
      },
      /**
       * handleAddUser部分连接后端接口后的调用
          handleAddUser() {
            if (this.newUser.username && this.newUser.email && this.newUser.password) {
              this.addUser({ ...this.newUser })
                .then(response => {
                  if (response && !response.success) {
                    alert(response.message);
                  } else {
                    this.newUser = { username: '', email: '', password: '', role: '' };
                    this.showAddUserForm = false;
                  }
                });
            } else {
              alert('请填写所有字段');
            }
          }
       * 
       */
      handleDeleteUser(username) {
        this.deleteUser(username);
      },
      editUser(user) {
        user.editing = true;
        user.newRole = user.role; // 初始化下拉栏的值
      },
      saveUser(user) {
        user.role = user.newRole;
        user.editing = false;
        this.updateAdmin(user); // 仅触发 Vuex 的 updateAdmin action
      },
      handleDeletePost(postId) {
        this.deletePost(postId);
      },
      handleDeleteComment(commentId) {
        this.deleteComment(commentId);
      },
      toggleAddAnnouncementForm() {
        this.showAddAnnouncementForm = !this.showAddAnnouncementForm;
      },
      handleAddAnnouncement() {
        if (this.newAnnouncement.title && this.newAnnouncement.content) {
          const newAnnouncement = {
            ...this.newAnnouncement,
            id: Date.now(),
            date: new Date().toLocaleDateString()
          };
          this.addAnnouncement(newAnnouncement);
          this.newAnnouncement = { title: '', content: '' };
          this.showAddAnnouncementForm = false;
        } else {
          alert('请填写公告的标题和内容');
        }
      },
      handleDeleteAnnouncement(announcementId) {
        this.deleteAnnouncement(announcementId);
      },
      toggleExpandPost(post) {
        this.expandedPost = this.expandedPost === post ? null : post;
      },
      toggleExpandFeedback(feedback) {
        this.expandedFeedback = this.expandedFeedback === feedback ? null : feedback;
      },
      //植物管理
      editPlant(plant) {
      plant.editing = true;
    },
    finishEditPlant(plant) {
      plant.editing = false;
    },
    approvePlant(plant) {
      plant.approved = true;
    },
    deletePlant(plantId) {
      this.$store.commit('DELETE_PLANT', plantId);
      },
    GoToHome()
    {
        this.$router.push('/home');
    },//返回主页界面
    }
};
  
</script>
  
<style scoped>
  @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap');
  
  .admin-panel-page {
  min-height: 100vh;
  background-image: url('\\images\\ground.jpg');
  background-size: cover;
  background-position: center;
  background-attachment: fixed; /* 背景固定，不随页面滚动 */
  padding: 20px;
  box-sizing: border-box;
}
  
.top-navbar {
  background-color: rgba(15, 123, 47, 0.7);
  padding: 15px 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.5);
  z-index: 10;
  position: fixed;
  width: 8%;
  height:100%;
  top: 0;
  left: 0;
}

.top-navbar ul {
  list-style: none;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column; 
  justify-content: space-around;
}

.top-navbar li {
  padding: 20px 33px;
  cursor: pointer;
  border-radius: 4px;
  color: rgb(255, 255, 255);
  font-weight: bold;
  margin-bottom: 10px; /* 每条数据条之间的间距 */
  transition: background-color 0.3s ease;
}
  
.top-navbar li:hover,
.top-navbar li.active {
  background-color: #1abc9c;
}

 
.main-content {
  margin-top: 120px; /* 增加 margin-top 值，将内容向下移动 */
  flex: 1;
  width:83%;
  margin-left: 11%;
  padding: 40px;
  background-color: rgba(255, 255, 255, 0.9);
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  overflow-y: auto;
}

  
h2 {
  font-size: 24px;
  color: #333;
  margin-top:0px;
  margin-bottom: 20px;
}
  
.item-list {
  list-style: none;
  align-items: center; /* 垂直居中 */
  padding: 0;
  margin-top: 10px;
  width: 80vw;
  display: flex; /* 启用 Flexbox 布局 */
  flex-wrap: wrap; /* 允许换行 */
  gap: 10px; /* 设置列和行之间的间距 */
}

.item-list li {
  display: flex; /* 让内容在同一行内对齐 */
  align-items: center; /* 垂直居中 */
  width: calc(49% - 5px); /* 设置每条数据条占父容器宽度的50%，并减去一半的间距 */
  padding: 15px 20px; /* 增加内部间距，使条目显得更宽 */
  margin-bottom: 10px; /* 每条数据条之间的间距 */
  background-color: #ffffff; /* 背景色，便于视觉区分 */
  border-radius: 8px; /* 圆角效果 */
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* 添加阴影 */
  box-sizing: border-box; /* 包含内边距和边框在内 */
}

.assignment{
  height:120px;
}

.item {
  background-color: #f9f9f9;
  padding: 15px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(153, 97, 97, 0.1);
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 50px; /* 设置宽度为父容器的 100%，使其适应 flex 布局 */
}
  
  .item-details p {
    margin: 10px;
    color: #238d89;
  }
  
  .item-actions {
    display: flex;
    gap: 10px;
  }
  
  .btn-delete {
  padding: 8px 12px;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}
  .btn-toggle-form,
  .btn-submit,
  .btn-edit,
  .btn-details {
    background-color: #597187;
    color: rgb(250, 250, 250);
    border: none;
    padding: 8px 16px;
    border-radius: 8px;
    cursor: pointer;
    transition: background-color 0.3s ease, transform 0.2s ease-in-out;
    font-size: 14px;
  }
  
  .btn-toggle-form:hover,
  .btn-submit:hover,
  .btn-edit:hover,
  .btn-delete:hover,
  .btn-details:hover {
    background-color: #128a22;
  }
  
  .form-container {
    background-color: rgba(255, 255, 255, 0.9);
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
    gap: 20px; /* 设置输入框之间的间距 */
    animation: fade-in 0.5s ease-out forwards;
}
.form-group {
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-size: 14px;
  margin-bottom: 5px;
  color: #333;
}
  .form-input {
    margin-bottom: 10px;
    padding: 10px;
    border: 2px solid #ced4da;
    border-radius: 8px;
    font-size: 16px;
    background-color: #ffffff;
  }
  
  textarea.form-input {
    height: 100px;
    resize: none;
  }
  
  .expanded-content {
    margin-top: 10px;
    padding: 10px;
    background-color: #e9ecef;
    border-radius: 8px;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  }
  /* PLANTS*/
  /* 省略其他样式，只展示植物详情样式 */

.plant-details {
  background-color: rgba(255, 255, 255, 0.9);
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
  margin-top: 20px;
}

.plant-details .form-group {
  margin-bottom: 15px;
}

.plant-details label {
  font-weight: bold;
  margin-bottom: 5px;
  display: block;
}

.plant-details .form-input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ced4da;
  border-radius: 8px;
  background-color: #e9ecef;
  color: #495057;
  resize: none;
}
/**/ 
.plants-review-section {
  display: flex;
  justify-content: space-between;
}

.plants-column {
  width: 48%; /* 设置宽度为两栏均分 */
  background-color: #fff;
  padding: 10px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  overflow-y: auto; /* 添加滚动条 */
}

.table-header, .table-row {
  display: flex;
  justify-content: space-between;
  padding: 10px;
  background-color: #f5f5f5;
  border-bottom: 1px solid #ccc;
}

.table-header {
  font-weight: bold;
}

.table-row {
  background-color: #f9f9f9;
  margin-bottom: 5px;
}

.plant-image {
  width: 50px;
  height: 50px;
  object-fit: cover;
  border-radius: 4px;
}

.form-input {
  width: 100%;
  padding: 5px;
  margin: 2px 0;
}

.btn-edit, .btn-submit, .btn-approve, .btn-delete {
  margin-left: 10px;
  cursor: pointer;
}
  /* Slide-in and fade-in animations */
.my-animation-slide-top {
    animation: slide-top 1s ease-out forwards;
}

.my-animation-slide-bottom {
    animation: slide-bottom 1s ease-out forwards;
}

.my-animation-fade-in {
    animation: fade-in 0.5s ease-out forwards;
}

@keyframes fade-in {
    0% { opacity: 0; transform: translateY(-10px); }
    100% { opacity: 1; transform: translateY(0); }
}

@keyframes slide-top {
    0% { opacity: 0; transform: translateY(-20%); }
    100% { opacity: 1; transform: translateY(0); }
}

@keyframes slide-bottom {
    0% { opacity: 0; transform: translateY(20%); }
    100% { opacity: 1; transform: translateY(0); }
}
</style>
  