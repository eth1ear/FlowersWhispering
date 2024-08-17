import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import mitt from 'mitt'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
 
const app=createApp(App)
app.config.globalProperties.emitter = mitt()
 
app.use(router)

// 引入 Element Plus
app.use(ElementPlus)

app.mount('#app')