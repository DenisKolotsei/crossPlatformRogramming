import React, { useEffect }from 'react'
import {useLocation, useNavigate} from 'react-router-dom';
import { getCode} from '../api';
import { useAuth } from '../AuthContext';

const CodeExchange = () => {
    const navigate = useNavigate();
    const searchParams = new URLSearchParams(useLocation().search);
    const code = searchParams.get('code');
    const { isAuthenticated, login, logout} = useAuth();

    useEffect(()=>{
        if(isAuthenticated){
            navigate("/");
            return;
        }
        if(code){
            getCode(code).then((res) => {
                if(res.data.registered){
                    login();
                    navigate("/");
                }
                else{
                    logout();
                    navigate("/registration");
                }
            }).catch((err) => {
                logout();
                navigate("/");
            })
        }
        else{
            navigate("/");
        }

    }, [])

    return (
        <div>Авторизация...</div>
    )
}

export default CodeExchange;