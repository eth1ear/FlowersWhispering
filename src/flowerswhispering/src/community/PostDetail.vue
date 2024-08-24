<template>
    <div class="post-detail">
      <div v-if="post" class="post-content">
        <h1>{{ post.title }}</h1>
        <p class="date">{{ post.date }}</p>
        <div class="content">
          <p>{{ post.content }}</p>
        </div>
        <div class="footer">
          <span class="author">Author: {{ post.author }}</span>
        </div>
      </div>
      <div v-else class="loading">Loading...</div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        post: null
      };
    },
    created() {
      this.fetchPost();
    },
    methods: {
      async fetchPost() {
        const postId = this.$route.params.id;
        // 替换成你的实际 API 路径
        const response = await fetch(`/api/posts/${postId}`);
        this.post = await response.json();
      }
    }
  }
  </script>
  
  <style scoped>
  .post-detail {
    margin: 20px auto;
    max-width: 800px;
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 8px;
    background: #f9f9f9;
  }
  
  .post-content {
    margin-bottom: 20px;
  }
  
  .post-content h1 {
    font-size: 2em;
    margin-bottom: 10px;
  }
  
  .post-content .date {
    font-size: 0.9em;
    color: #666;
    margin-bottom: 20px;
  }
  
  .post-content .content {
    font-size: 1.1em;
    line-height: 1.6;
    margin-bottom: 20px;
  }
  
  .post-content .footer {
    font-size: 0.9em;
    color: #333;
  }
  
  .post-content .author {
    font-weight: bold;
  }
  
  .loading {
    text-align: center;
    font-size: 1.2em;
    color: #999;
  }
  </style>
  