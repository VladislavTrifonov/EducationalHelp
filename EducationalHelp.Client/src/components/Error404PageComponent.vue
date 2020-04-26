<template>
    <section id="wrapper" class="container-fluid">
        <div class="error-box">
            <div class="error-body text-center">
                <h1 class="text-danger">404</h1>
                <h3>Страница не найдена!</h3>
                <p v-if="this.$route.params.id == undefined" class="text-muted m-t-30 m-b-30">Страница, к которой Вы обратились, не существует.</p>
                <p v-else class="text-muted m-t-30 m-b-30">Ресурс с идентификатором <i>{{this.$route.params.id}}</i> не обнаружен.</p>
                <router-link to="/" class="btn btn-danger btn-rounded m-b-40">На главную</router-link>
            </div>
        </div>
    </section>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { Component } from 'vue-property-decorator';

    @Component({})
    export default class Error404PageComponent extends Vue {

    }

</script>

<style scoped>
    #wrapper {
        width: 100%
    }

    error-box {
        height: 100%;
        position: fixed;
        top: 20%;
        width: 100%
    }

    .error-box .footer {
        width: 100%;
        left: 0;
        right: 0
    }

    .error-body {
        padding-top: 5%
    }

        .error-body h1 {
            font-size: 210px;
            font-weight: 900;
            line-height: 210px
        }

    .text-danger {
        color: #f33155
    }

    .text-muted {
        color: #8d9ea7
    }

    .m-b-40 {
        margin-bottom: 40px !important
    }

    .m-t-30 {
        margin-top: 30px !important
    }

    .m-b-30 {
        margin-bottom: 30px !important
    }

    @media only screen and (max-width: 520px) {
        .error-body h1 {
            font-size: 110px;
            font-weight: 700;
            line-height: 110px
        }
    }
</style>