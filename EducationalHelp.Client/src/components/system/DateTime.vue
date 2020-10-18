<template>
  <span class="date-time">{{ getDate }} в {{ getTime }}</span>
</template>

<script>
export default {
name: "DateTime",
  props: ['dateTimeString'],
  computed: {
    getDate: function() {
      let date = new Date(this.dateTimeString.split("T")[0]);
      return date.getDate() + "." + date.getMonth() + "." + date.getFullYear();
    },
    getTime: function() {
      return this.dateTimeString.split("T")[1]
    }
  }
}
</script>

<style scoped>

</style>