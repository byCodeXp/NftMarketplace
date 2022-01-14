import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import accountSlice from '../reducers/account';
const store = configureStore({
    reducer: {accountSlice},
});

export default store;

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
    ReturnType,
    RootState,
    unknown,
    Action<string>
>;
