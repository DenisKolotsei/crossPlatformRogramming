import React, {useState} from 'react'
import { Reg} from '../api';
import {useNavigate} from 'react-router-dom';
import { useAuth } from '../AuthContext';

const Register = () => {
    const { isAuthenticated, login, logout} = useAuth();
    const [number, setNumber] = useState<string>("");
    const navigate = useNavigate();

    function register(){
        Reg(number).then(res => {
            login();
            navigate("/");
        }).catch(res => {
            window.alert("Щось не так");
        })
    }

    return (
        <div className='Register'>
            phone: <input type="number" value={number} onChange={(e) => {setNumber(e.target.value)}}/><br/>
            <button onClick={register}>register</button>
        </div>
    )
}
export default Register;