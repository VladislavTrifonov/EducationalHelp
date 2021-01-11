import axios from '@/axiosconf'
import Group from "@/api/models/Group";

export default class GroupsAPI {

    static getAllGroups(): Promise<Array<Group>> {
        return new Promise((resolve, reject) => {
            axios.get("api/groups").then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            })
        })
    }

}
