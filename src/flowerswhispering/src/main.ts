import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store';
import mitt from 'mitt'

import "vue3-video-play"  //引入播放插件,记得下载

 
const app=createApp(App)
app.config.globalProperties.emitter = mitt()
 
app.use(router)
app.use(store);
app.mount('#app')