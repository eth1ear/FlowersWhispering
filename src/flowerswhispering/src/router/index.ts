import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
 
const routes: Array<RouteRecordRaw> = [
  { path: '/', component: () => import('@/login/login.vue') },                   //页面切换到login
  {path:'/main_theme',component: () => import('@/main_theme/main_theme.vue')},  //页面切换到main theme
]
 
const router = createRouter({
  history: createWebHashHistory(),
  routes
})
 
export default router