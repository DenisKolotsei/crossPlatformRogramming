import { useEffect, useState}from 'react'
import '../styles/Lab.css';
import {solveLab1 } from '../api';

const Lab1 = () => {
    const [res, setRes] = useState(0);
    const [str1, setStr1] = useState("");
    const [str2, setStr2] = useState("");

    function solve(){
        solveLab1(str1, str2).then((res) => {
            setRes(res.data.res1)
        }).catch((err) => {
            window.alert('Щось пішло не так');
        })
    }

    return (
        <div className='Lab'>
            <h2>Лабораторна робота 1</h2>
            <h4>Дано два шаблони: p1 і p2. Розглянемо множину S1 рядків, які можуть бути отримані з p1
за описаними правилами, і множина S2 рядків, які можуть бути отримані з p2. Необхідно
знайти кількість рядків, що входять в обидва ці множини.</h4>
            <div className="inputsPlace">
                <input type="text" value={str1} onChange={e => setStr1(e.target.value)}/>
                <input type="text" value={str2} onChange={e => setStr2(e.target.value)}/>
            </div>
            <button className='solveLabButton' onClick={solve}>solve</button>
            <div className="resPlace">
                Результат: {res}
            </div>
        </div>
    )
}

export default Lab1;