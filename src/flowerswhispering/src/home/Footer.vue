<template>
  <footer class="footer">
    <p class="left"><a href="contact.html">联系我们</a></p>
    <div class="center">
      <span>© 2024 Flowers Whispering&nbsp;&nbsp;&nbsp;&nbsp;</span>
      <a href="https://beian.miit.gov.cn/" target="_blank">豫ICP备2024087175号-1</a>
    </div>
    <div class="right"></div>
  </footer>
</template>

<script>
export default {
  name: 'Footer',
};
</script>

<style scoped>
.footer {
  display: flex;
  justify-content: space-between;  /* 左中右均匀分布 */
  align-items: center;             /* 垂直居中对齐 */
  background-color: rgba(70, 180, 118, 0.8);
  color: white;
  position: relative;
  bottom: 0;
  width: 100%;                     /* 跨满页面 */
  z-index: 10;
}

.footer .left {
  text-align: left;
  margin-left: 10px;               /* 左边距 */
}

.footer .center {
  text-align: center;
  flex: 1;                         /* 中间内容居中，并占据剩余空间 */
}

.footer .right {
  text-align: right;
  margin-right: 10px;              /* 右边距 */
}

.footer a {
  color: white;
  text-decoration: none;
}

.footer a:hover {
  color: rgb(24, 212, 209); /* 悬停下划线效果 */
}
</style>
