import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { collectionService, ICreateRequest } from "../api/services/collectionService";


interface IState {
    name ?: string;
    status : 'idle' | 'pending' | 'rejected' | 'fulfilled'
}

const initialState: IState = {
    name: undefined,
    status: 'idle'
}

export const createActionAsync = createAsyncThunk('createActionAsync', async(request: ICreateRequest) => {
    let response;
    try {
        response = await collectionService.createAsync(request);
    }
    catch(error) {
        throw new Error("The same collection already exists");
    }
    return response.data.name;
})

const collectionSlice = createSlice({
    name: 'collection',
    initialState,
    reducers: {

    },
    extraReducers: {
        // create
        [createActionAsync.pending.type] : (state, action) => {
            state.status = 'pending';
        },
        [createActionAsync.rejected.type] : (state, action) => {
            state.status = 'rejected';
        },
        [createActionAsync.fulfilled.type] : (state, action) => {
            state.status = 'fulfilled';
            state.name = action.payload;
        }
    }
})

export default collectionSlice.reducer;