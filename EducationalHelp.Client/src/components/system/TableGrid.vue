<template>
    <b-table-simple hover caption-top responsive>
        <caption>{{header}}</caption>
        <b-thead head-variant="dark">
            <b-tr>
                <slot name="headerContent">
                    <b-th v-for="(k, index) in keys" :key="index">
                        {{k.head}}
                    </b-th>
                </slot>
            </b-tr>
        </b-thead>
        <b-tbody>
            <b-tr v-for="(item, index) in items" :key="index">
                <slot name="rowContent" v-bind:item="item" v-bind:index="index">
                    <b-td v-for="k in keys" :key="k.key">
                        {{getValueOfItem(item, k, index)}}
                    </b-td>
                </slot>
            </b-tr>
        </b-tbody>
    </b-table-simple>
</template>

<script>
    export default {
        props: ["keys", "items", "header"],
        methods: {
            getValueOfItem: (item, k, index) => {
                let n = k.key;
                if (n == 'index') {
                    return index + 1;   
                }
                return item[n];
            }
        }
    }
</script>

<style scoped>
</style>