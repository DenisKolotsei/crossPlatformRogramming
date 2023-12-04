import React, { useEffect, useState}from 'react'
import '../styles/Lab.css';
import { LabResult, solveLab2 } from '../api';

const Lab2 = () => {
    const [res, setRes] = useState(0);
    const [num, setNum] = useState(0);

    function solve(){
        solveLab2(num).then((res) => {
            setRes(res.data.res1)
        }).catch((err) => {
            window.alert('Щось пішло не так');
        })
    }

    return (
        <div className='Lab'>
            <h2>Лабораторна робота 2</h2>
            <h4>Знайдіть n-й елемент послідовності, що строго зростає, яка описується
наступними правилами:
1. число 1 є елементом послідовності;
2. якщо a – елемент послідовності, то 2a, 3a, 5a також є елементами
послідовності;
3. послідовності належать лише елементи, задані правилами 1 та 2.</h4>
            <div className="inputsPlace">
                <input type="number" value={num} onChange={e => setNum(parseInt(e.target.value))}/>
            </div>
            <button className='solveLabButton' onClick={solve}>solve</button>
            <div className="resPlace">
                Результат: {res}
            </div>
        </div>
    )
}

export default Lab2;