<template>
    <section id="wrapper" class="container-fluid">
        <div class="error-box">
            <div class="error-body text-center">
                <h1 class="text-danger">404</h1>
                <h3>Страница не найдена!</h3>
                <p v-if="this.$route.params.id == undefined" class="text-muted m-t-30 m-b-30">Страница, к которой Вы обратились, не существует.</p>
                <p v-else class="text-muted m-t-30 m-b-30">Ресурс с идентификатором <i>{{this.$route.params.id}}</i> не обнаружен.</p>
                <router-link :to="{ name: 'homePage' }" class="btn btn-danger btn-rounded m-b-40">На главную</router-link>
            </div>
        </div>
    </section>
</template>

<script lang="ts" src="./index.ts">


</script>

<style scoped src="./index.css">

</style>