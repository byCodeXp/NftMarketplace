import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { About } from './components/About';
import { AccountInfo } from './components/AccountInfo';
import { CreateCollection } from './components/CreateCollection';
import Home from './components/Home';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { NotFound } from './components/NotFound';
import { Register } from './components/Register';
import { useAppSelector } from './store/hooks';

// Bob_12345

export const App = () => {
    
    const user = useAppSelector((state) => state.accountSlice.user);
    return (
        <>
            <Routes>
                <Route path="/" element={<Layout/>}>
                    <Route index element={<Home/>}></Route>
                    <Route path="/login" element={<Login/>}></Route>
                    <Route path="/register" element={<Register/>}></Route>
                    <Route path="/about" element={<About/>}></Route>
                    <Route path="/collection/create" element={<CreateCollection/>}></Route>
                    <Route path="/account" element={<AccountInfo/>}></Route>
                    <Route path="*" element={<NotFound/>}></Route>
                </Route>
            </Routes>
        </>
    );
};
