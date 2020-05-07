<template>
    <div class="row" v-if="show">
        <div class="alert alert-danger">
            <b-icon-exclamation-octagon-fill></b-icon-exclamation-octagon-fill> Возникли проблемы с интернет-подключением к серверу.
            <b-link onclick="location.reload()">Обновить страницу...</b-link>
        </div>
    </div>
</template>

<script>
    export default {
        props: ['show']
    }
</script>

<style scoped>
</style>