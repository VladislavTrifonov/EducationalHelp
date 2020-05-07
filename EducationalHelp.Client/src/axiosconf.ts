import axios, { AxiosRequestConfig } from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:5001',
});

instance.interceptors.request.use(request => {
    if (processing.onRequestStart != null)
        processing.onRequestStart(request); 
    return request;
});

instance.interceptors.response.use(response => {
    if (processing.onResponseResolved != null)
        processing.onResponseResolved(response);
    return response;
}, error => {
    if (error.message.includes("Network")) {
        if (processing.onNetworkError != null)
            processing.onNetworkError(error);
    }
});

export interface IRequestFunction {
    (request: AxiosRequestConfig): void;
}

export interface IResponseFunction {
    (response: any): void;
}

export class RequestAndResponseProcessing {
    public onRequestStart!: IRequestFunction;
    public onResponseResolved!: IResponseFunction;
    public onNetworkError!: IResponseFunction;
}

const processing = new RequestAndResponseProcessing(); 

export { processing };

export default instance;