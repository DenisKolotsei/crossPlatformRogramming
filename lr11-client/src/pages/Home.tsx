import {Link} from 'react-router-dom';
import { GetUrl} from '../api';
import { useAuth } from '../AuthContext';
import React, { useEffect }from 'react'
import "../styles/Home.css";

const Home = () => {
    const { isAuthenticated } = useAuth();

    function Login(){
        GetUrl().then((res) => {
            window.location.href = res.data.url;

        }).catch((err) => {
            console.log("error")
        })
    }

    return (
        <div className='Home'>
            <h1>Тут можна виконати три лабораторні роботи</h1>
            {isAuthenticated
            ?
            <>
                <Link className="lablink" to="/lab1">lab1</Link><br />
                <Link className="lablink" to="/lab2">lab2</Link><br />
                <Link className="lablink" to="/lab3">lab3</Link>
            </>
            :
            <button className="loginButton" onClick={Login}>Увійти</button>
            }
        </div>
    )
}

export default Home;