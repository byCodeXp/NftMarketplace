import { makeClient } from '../client';


const client = makeClient("identity");

const loginAsync = (loginRequest: ILoginRequest) => {
    return client.post("login", loginRequest);
}

export interface ILoginRequest {
    userName : string;
    password : string;
    remember: boolean;
}

export const accountService = {
    loginAsync
}
