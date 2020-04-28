<template>
    <b-container>
        <b-row>
            <b-col cols="8">
                <caption>{{header}}</caption>
            </b-col>
            <b-col cols="4">
                <slot name="toolbarContent">
                    <div class="d-flex flex-row justify-content-end mt-3">
                        <b-button v-if="isAddPossible" variant="outline-primary" class="m-2" v-on:click="clickAddButton">
                            <b-icon-plus-circle></b-icon-plus-circle> Добавить
                        </b-button>
                        <b-button v-if="isUpdatePossible" variant="outline-primary" class="m-2" v-on:click="clickRefreshButton">
                            <b-icon-arrow-clockwise></b-icon-arrow-clockwise> Обновить
                        </b-button>
                    </div>
                </slot>
            </b-col>
        </b-row>
        <b-row>
            <b-table-simple hover caption-top responsive>
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
        </b-row>
    </b-container>
</template>

<script>
    export default {
        props: {
            keys: {

            }, 
            items: {

            },
            header: {
                default: "Таблица"
            },
            isAddPossible: {
                default: false
            },
            isUpdatePossible: {
                default: true
            }
        },           
        methods: {
            getValueOfItem(item, k, index)  {
                let n = k.key;
                if (n == 'index') {
                    return index + 1;   
                }
                return item[n];
            }, 

            clickRefreshButton() {
                this.$emit('update-trigger');
            }
        }
    }
</script>

<style scoped>
</style>