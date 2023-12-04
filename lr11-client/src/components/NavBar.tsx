import React from 'react'
import '../styles/NavBar.css';
import {Link} from 'react-router-dom';
import Home from '../pages/Home';
import Profile from '../pages/Profile';

const NavBar = () => {
    

    return (
        <div className="NavBar">
            <Link className="link" to="/">Home</Link>
            <Link className="link" to="/profile">Profile</Link>
        </div>
    )
}

export default NavBar;