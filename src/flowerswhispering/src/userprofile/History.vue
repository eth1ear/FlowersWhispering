<template>
  <div class="user-profile-page">
    <!-- 侧边栏导航 -->
    <aside class="sidebar">
      <nav>
        <ul>
          <li><router-link to="/profile" active-class="active">个人资料</router-link></li>
          <li><router-link to="/account" active-class="active">账号设置</router-link></li>
          <li><router-link to="/setting" active-class="active">隐私设置</router-link></li>
          <li><router-link to="/auth" active-class="active">信息认证</router-link></li>
          <li><router-link to="/collection-list" active-class="active">我的收藏</router-link></li>
          <li><router-link to="/history" active-class="active">浏览历史</router-link></li>
        </ul>
      </nav>
      <!-- 返回主页按钮放在侧边栏底部 -->
      <div class="actions">
        <button @click="goHome" class="btn-primary">返回主页</button>
      </div>
    </aside>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'UserProfilePage',
  methods: {
    goHome() {
      this.$router.push('/home');
    }
  }
});
</script>

<style scoped>
.user-profile-page {
  display: flex;
  height: 100vh;
  overflow: hidden; /* 防止出现滚动条 */
}

.sidebar {
  width: 220px;
  background-color: #f7f7f7;
  padding: 20px;
  display: flex;
  flex-direction: column;
  border: 1px solid #e0e0e0; /* 添加边框 */
  border-radius: 8px; /* 可选：为边框增加圆角 */
  justify-content: space-between;
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar li {
  margin-bottom: 10px;
}

.sidebar a {
  text-decoration: none;
  color: #333;
  font-weight: bold;
  display: block;
  padding: 10px 15px;
  border-radius: 4px;
  transition: background-color 0.3s ease;
}

.sidebar a.active,
.sidebar a:hover {
  background-color: #007bff;
  color: white;
}

.actions {
  margin-top: auto; /* 将返回主页按钮推到最下方 */
  padding-top: 20px;
}

.btn-primary {
  padding: 8px 16px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-primary:hover {
  background-color: #0056b3;
}
</style>
