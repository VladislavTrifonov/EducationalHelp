<template>
  <b-breadcrumb>
    <b-breadcrumb-item v-for="(item, idx) in breadcrumbs" :key="idx" active>
      <router-link v-if="item.to !== undefined && item.link == true" :to="item.to" class="breadcrumb__link">
        {{item.title}}
      </router-link>
      <span v-else>
          {{item.title}}
      </span>
    </b-breadcrumb-item>
  </b-breadcrumb>
</template>

<script src="./index.ts" lang="ts">
</script>

<style scoped src="./index.css">
</style>
