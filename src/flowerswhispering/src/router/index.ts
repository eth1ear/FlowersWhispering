import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import store from '@/store';
import Home from '../home/Home.vue';
import Login from '../login/Login.vue';
import AdminPanel from '../adminpanel/AdminPanel.vue';
import Catalog from '../catalog/Catalog.vue';
import Community from '../community/Community.vue';
import Profile from '../userprofile/Profile.vue';
import Account from '../userprofile/Account.vue';
import Setting from '../userprofile/Setting.vue';
import Auth from '../userprofile/Auth.vue';
import CollectionList from '../userprofile/collection-list.vue';
import History from '../userprofile/History.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/login'  // 重定向到登录页面
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/catalog',
    name: 'Catalog',
    component: Catalog,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/community',
    name: 'Community',
    component: Community,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/profile',
    name: 'Profile',
    component: Profile,
    meta: { requiresAuth: true } // 需要验证
  },
  {
    path: '/account',
    name: 'Account',
    component: Account,
    meta: { requiresAuth: true } // 需要验证
  },
  {
    path: '/setting',
    name: 'Setting',
    component: Setting,
    meta: { requiresAuth: true } // 需要验证
  },
  {
    path: '/auth',
    name: 'Auth',
    component: Auth,
    meta: { requiresAuth: true } // 需要验证
  },
  {
    path: '/collection-list',
    name: 'CollectionList',
    component: CollectionList,
    meta: { requiresAuth: true } // 需要验证
  },
  {
    path: '/history',
    name: 'History',
    component: History,
    meta: { requiresAuth: true } // 需要验证
  },
  {
    path: '/home',
    name: 'Home',
    component: Home,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/adminpanel',
    name: 'AdminPanel',
    component: AdminPanel,
    meta: { requiresAuth: true, requiresAdmin: true }  // 需要管理员权限
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    // 检查用户是否已认证
    if (!store.getters.isAuthenticated) {
      alert('请先登录!');
      next({ name: 'Login' });
    } else if (to.matched.some(record => record.meta.requiresAdmin)) {
      // 检查用户是否是管理员
      if (!store.getters.isAdmin) {
        alert('您无权访问该页面。');
        next({ name: 'Home' }); // 重定向到主页
      } else {
        next();
      }
    } else {
      next();
    }
  } else {
    next(); // 确保总是调用 next() 方法
  }
});

export default router;
