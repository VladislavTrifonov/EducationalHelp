<template>
    <ul>
        <li v-for="subject in subjects"
            :key="subject.id">
            {{ subject.name }} - {{ subject.teacher }}
            <br>
            
        </li>
    </ul>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import Subject from '@/api/models/Subject';
    import Store from '@/store/index';
    import { mapState, mapActions } from 'vuex';
    import SubjectsState from '@/store/modules/subjects/state';

    @Component({
        computed: mapState('subjects', {
            // @ts-ignore
            subjects: state => state.all,
        }),
    })
    export default class SubjectsPageComponent extends Vue {

        public subjects!: Array<Subject>;
        
    }

</script>

<style scoped>
</style>