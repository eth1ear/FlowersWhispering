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

    <!-- 主内容区域 -->
         <main class="main-content">
      <!-- 用户头像和基本信息 -->
      
      <div class="profile-header">
           <div class="avatar-container" @mouseover="hoverAvatar = true" @mouseleave="hoverAvatar = false">
          <img :src="avatarUrl" alt="用户头像" :class="{ 'avatar-hover': hoverAvatar }" />
          <div v-if="hoverAvatar" class="change-avatar-overlay">
            <span @click="changeAvatar">更换头像</span>
          </div>
        </div>
       
        <div class="user-info">
           <p>{{ user.username }}</p>
         </div>
       </div>
      <!-- 下部分：基本信息 -->
      <div class="basic-info-box">
        <h2>基本信息</h2>
        <hr />
        <div class="info-group">
          <label>用户名:</label>
          <span>{{ user.username }}</span>
        </div>
        <div class="info-group">
          <label>性别:</label>
          <span v-if="!editGender">{{ user.gender }}</span>
          <div v-else>
            <input type="radio" id="male" value="男" v-model="user.gender">
            <label for="male">男</label>
            <input type="radio" id="female" value="女" v-model="user.gender">
            <label for="female">女</label>
            <button @click="saveGender" class="action-button">确定</button>
            <button @click="cancelEdit('gender')" class="action-button">取消</button>
          </div>
          <button v-if="!editGender" @click="editField('gender')" class="edit-button">编辑</button>
        </div>
        <div class="info-group">
          <label>邮箱:</label>
          <span v-if="!editEmail">{{ user.email }}</span>
          <input v-else v-model="user.email" type="email" placeholder="请输入邮箱" />
          <button v-if="!editEmail" @click="editField('email')" class="edit-button">编辑</button>
          <button v-if="editEmail" @click="saveEmail" class="action-button">保存</button>
          <button v-if="editEmail" @click="cancelEdit('email')" class="action-button">取消</button>
        </div>
        <div class="info-group">
          <label>电话:</label>
          <span v-if="!editPhone">{{ user.phone }}</span>
          <input v-else v-model="user.phone" type="tel" placeholder="请输入电话号码" />
          <button v-if="!editPhone" @click="editField('phone')" class="edit-button">编辑</button>
          <button v-if="editPhone" @click="savePhone" class="action-button">保存</button>
          <button v-if="editPhone" @click="cancelEdit('phone')" class="action-button">取消</button>
        </div>
        <div class="info-group">
          <label>个人简介:</label>
          <span v-if="!editBio">{{ user.bio }}</span>
          <div v-else>
            <textarea v-model="user.bio" rows="3" placeholder="你很懒，还没有添加简介"></textarea>
            <button @click="saveBio" class="action-button">提交</button>
            <button @click="cancelEdit('bio')" class="action-button">取消</button>
          </div>
          <button v-if="!editBio" @click="editField('bio')" class="edit-button">编辑</button>
        </div>
        <div class="info-group">
          <label>所在地:</label>
          <span v-if="!editLocation">{{ user.location }}</span>
          <div v-else>
            <select v-model="user.location">
              <option value="">请选择地区</option>
              <option value="北京">北京</option>
              <option value="上海">上海</option>
              <option value="广州">广州</option>
              <!-- 其他选项 -->
            </select>
            <button @click="saveLocation" class="action-button">确定</button>
            <button @click="cancelEdit('location')" class="action-button">取消</button>
          </div>
          <button v-if="!editLocation" @click="editField('location')" class="edit-button">编辑</button>
        </div>
        <div class="info-group">
          <label>地址管理:</label>
          <a href="#">+ 新增收货地址</a>
        </div>
        <div class="info-group">
          <label>出生日期:</label>
          <input type="date" v-model="user.birthdate" />
        </div>
      </div>
</main>
  </div>

</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'UserProfilePage',
  data() {
    return {
      avatarUrl: require('./user-avatar.jpg'), // 替换为用户头像的路径，后期改为后端上传
      user: {
        username: 'shark', // 用户注册时的用户名
        gender: '未选择',
        email: 'user@example.com',
        phone: '123-456-7890',
        bio: '未填写',
        location: '未选择',
        address: '未填写',
        birthdate: ''
      },
      hoverAvatar: false, // 控制鼠标悬停时的效果
      editGender: false,
      editBio: false,
      editLocation: false,
      editEmail: false,
      editPhone: false
    };
  },
  methods: {
    editField(field: 'gender' | 'bio' | 'location' | 'email' | 'phone') {
      this[`edit${field.charAt(0).toUpperCase() + field.slice(1)}` as 'editGender' | 'editBio' | 'editLocation' | 'editEmail' | 'editPhone'] = true;
    },
    cancelEdit(field: 'gender' | 'bio' | 'location' | 'email' | 'phone') {
      this[`edit${field.charAt(0).toUpperCase() + field.slice(1)}` as 'editGender' | 'editBio' | 'editLocation' | 'editEmail' | 'editPhone'] = false;
    },
    saveGender() {
      this.editGender = false;
      // 后端API调用，保存性别
    },
    saveBio() {
      this.editBio = false;
      // 后端API调用，保存个人简介
    },
    saveLocation() {
      this.editLocation = false;
      // 后端API调用，保存所在地
    },
    saveEmail() {
      this.editEmail = false;
      // 后端API调用，保存邮箱
    },
    savePhone() {
      this.editPhone = false;
      // 后端API调用，保存电话
    },
    goHome() {
      this.$router.push('/home');
    },
    changeAvatar() {
      // 预留的后端接口调用
      alert('更换头像接口调用');
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

.main-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  padding: 20px;
  background-color: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  margin-left: 20px;
  justify-content: flex-start; /* 从上至下排列 */
}

.profile-header{
  border: 1px solid #e0e0e0;
  padding: 20px;
  border-radius: 8px;
  display: flex;
  width: 100%;
  padding: 20px;
  margin-bottom: 10px;
}
.basic-info-box {
  border: 1px solid #e0e0e0;
  padding: 20px;
  border-radius: 8px;
  width: 100%;
  padding: 20px;
  margin-bottom: 10px;
}

.avatar {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  object-fit: cover;
  margin-right: 20px;
}

.avatar-container {
  position: relative;
  display: inline-block;
  cursor: pointer;
  flex-shrink: 0; /* 防止头像变形 */
  margin-right: 20px; /* 留出头像和用户信息之间的间距 */
  width: 120px; /* 设置为头像的宽度 */
  height: 120px; /* 设置为头像的高度 */
}

.avatar-container img {
  width: 100%;
  height: 100%;
  border-radius: 50%;
  transition: opacity 0.3s ease;
}

.avatar-hover {
  opacity: 0.6;
}

.change-avatar-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: rgba(0, 0, 0, 0.5);
  color: white;
  font-size: 16px;
  border-radius: 50%;
}

.user-info {
  flex-grow: 1;
  text-align: left;
  display: flex;
  align-items: center;
  justify-content: space-between; /* 用户信息靠右对齐 */
}

.user-info p {
  margin: 0; /* 去掉默认的段落间距 */
  font-size: 24px; /* 你可以根据需要调整字体大小 */
  font-weight: bold; /* 使用户名突出 */
}

.user-info h3 {
  margin: 0;
  font-size: 24px; /* 增大用户名字体 */
}

hr {
  border: none;
  border-top: 2px solid #007bff; /* 设置分割线 */
  margin-bottom: 20px;
}

.info-group {
  display: flex;
  align-items: center;
  margin-bottom: 20px; /* 增大每个信息项的间距 */
}

.info-group label {
  width: 120px;
  font-size: 18px; /* 增大标签字体 */
  font-weight: bold;
}

.info-group span,
.info-group input,
.info-group select,
.info-group textarea {
  flex-grow: 1;
  font-size: 18px; /* 增大内容字体 */
}

.info-group textarea {
  resize: none;
  margin-top: 10px;
  padding: 8px;
}

.action-button {
  margin-left: 10px;
  padding: 6px 12px;
  font-size: 16px; /* 增大按钮字体 */
  color: white;
  background-color: #ff6b6b;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.action-button:hover {
  background-color: #ff4b4b;
}

.edit-button {
  margin-left: 10px;
  padding: 2px 8px;
  font-size: 16px; /* 增大编辑按钮字体 */
  color: #007bff;
  background-color: transparent;
  border: none;
  cursor: pointer;
}

.edit-button:hover {
  text-decoration: underline;
}
</style>
