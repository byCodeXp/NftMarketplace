import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { About } from './components/About';
import Home from './components/Home';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Register } from './components/Register';


export const App = () => {
    return (
        <>
            <Routes>
                <Route path="/" element={<Layout/>}>
                    <Route index element={<Home/>}></Route>
                    <Route path="/login" element={<Login></Login>}></Route>
                    <Route path="/register" element={<Register></Register>}></Route>
                    <Route path="/about" element={<About></About>}></Route>
                </Route>
            </Routes>
        </>
    );
};
