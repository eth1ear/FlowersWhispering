import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import store from '@/store';
import Home from '../home/Home.vue';
import Login from '../login/login.vue';

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
    path: '/home',
    name: 'Home',
    component: Home,
    meta: { requiresAuth: true }  // 添加元数据标记需要验证
  },
  // 其他受保护的路由也应添加 requiresAuth 元数据
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!store.getters.isAuthenticated) {
      next({ name: 'Login' });
    } else {
      next();
    }
  } else {
    next(); // 确保总是调用 next() 方法
  }
});

export default router;
