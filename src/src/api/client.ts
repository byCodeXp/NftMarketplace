import axios, { AxiosInstance } from 'axios';
import {tokenUtility} from "../utils/tokenUtility";

const BASE_URL = 'https://localhost:7089/api';

function makeAxiosInstance(baseRoute : string) {
    const client = axios.create({
        baseURL: `${BASE_URL}/${baseRoute}/`
    });

    client.interceptors.request.use(
        config => {
            const token = tokenUtility.getBearerToken();
            if (token) {
                config.headers = { ...config.headers, Authorization: token }
            }
            return config;
        },
        error => Promise.reject(error)
    );

    return client;
}

class Client
{
    private _client: AxiosInstance;
    constructor(baseRoute : string) {
        this._client = makeAxiosInstance(baseRoute);
    }

    get(url: string) {
        return this._client.get(url);
    }
    post(url: string, data: any) {
        return this._client.post(url, data);
    }
    put(url: string, data: any) {
        return this._client.put(url, data);
    }
    delete(url: string) {
        return this._client.delete(url);
    }
}

export function makeClient(baseRoute: string) {
    return new Client(baseRoute);
}