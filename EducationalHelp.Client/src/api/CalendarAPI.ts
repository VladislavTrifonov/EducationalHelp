import axios from '@/axiosconf';
import CalendarEvent from "@/api/models/CalendarEvent";

export default class FileAPI {
    constructor() {

    }

    getEvents(startDate: Date, endDate: Date): Promise<CalendarEvent> {
        return new Promise<any>((resolve, reject) => {
            axios.get('api/calendar/events', {
                params: {
                    dateStart: startDate,
                    dateEnd: endDate
                }
            }).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            })
        })
    }

}