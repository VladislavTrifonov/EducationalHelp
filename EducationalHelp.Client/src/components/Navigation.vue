<template>
    <b-navbar toggleable="lg" type="dark" variant="primary">
        <b-navbar-brand href="#">Помощник по обучению</b-navbar-brand>
        <b-navbar-toggle class="navbar-toggler" type="button" data-toggle="collapse" target="nav-collapse" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav style="cursor: pointer">
                <router-link class="nav-link" tag="li" active-class="active" to="/" exact>Главная</router-link>
                <router-link class="nav-link" tag="li" active-class="active" to="/subjects">Предметы</router-link>
            </b-navbar-nav >
        </b-collapse>
    </b-navbar>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { Component, Prop } from 'vue-property-decorator';

    @Component({
        components: {
           
        }
    })
    export default class Navigation extends Vue {
        
    }
</script>

<style scoped>
</style>