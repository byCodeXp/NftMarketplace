import { createSlice, configureStore, createAsyncThunk } from '@reduxjs/toolkit';
import { accountService, ILoginRequest } from '../api/services/accountService';
import { tokenUtility } from '../utils/tokenUtility';



interface IState {
    user ?: IUser;
    status : 'idle' | 'pending' | 'rejected' | 'fulfilled'
}

const initialState: IState = {
    user: undefined,
    status: 'idle'
};

export const loginActionAsync = createAsyncThunk('loginActionAsync', async (request: ILoginRequest) => {
    let response;
    try {
        response = await accountService.loginAsync(request);
    }
    catch(error) {
        throw new Error("Something went wrong while login");
    }
    tokenUtility.setToken(response.data.token);
    return response.data.user;
});



const accountSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
        logoutAction: (state) => {
            state.user = undefined;
        }
    },
    extraReducers: {
        [loginActionAsync.pending.type] : (state, action) => {
            state.status = 'pending';
        },
        [loginActionAsync.rejected.type] : (state, action) => {
            state.status = 'rejected';
        },
        [loginActionAsync.fulfilled.type] : (state, action) => {
            state.user = action.payload;
            state.status = 'fulfilled';
        }
    }
});

export const { logoutAction } = accountSlice.actions;

export default accountSlice.reducer;

