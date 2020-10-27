import axios from '@/axiosconf';

export default class FileAPI {
    constructor() {

    }

    deleteFile(fileId: string): Promise<any> {
        return new Promise<any>((resolve, reject) => {
            axios.delete('api/files/' + fileId).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            })
        })
    }

}