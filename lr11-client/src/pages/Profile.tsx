import React from 'react'
import "../styles/Profile.css"
import { useEffect, useState}from 'react'
import { profile } from '../api';

const Profile = () => {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [phone, setPhone] = useState("");
    useEffect(() => {
        profile().then((res) => {
            setName(res.data.name);
            setEmail(res.data.email);
            setPhone(res.data.phone);
        }).then((err) => {

        })
    }, []);

    return (
        <div className='Profile'>
            <div>name: {name}</div>
            <div>email: {email}</div>
            <div>phone: {phone}</div>
        </div>
    )
}

export default Profile;