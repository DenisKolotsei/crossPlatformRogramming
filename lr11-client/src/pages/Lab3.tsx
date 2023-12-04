import React, { useEffect, useState}from 'react'
import '../styles/Lab.css';
import { LabResult, solveLab3 } from '../api';

const Lab3 = () => {
    const [res, setRes] = useState(0);
    const [num1, setNum1] = useState(0);
    const [num2, setNum2] = useState(0);
    const [num3, setNum3] = useState(0);
    const [num4, setNum4] = useState(0);
    const [num5, setNum5] = useState(0);

    function solve(){
        solveLab3(num1, num2, num3, num4, num5).then((res) => {
            setRes(res.data.res1)
        }).catch((err) => {
            window.alert('Щось пішло не так');
        })
    }

    return (
        <div className='Lab'>
            <h2>Лабораторна робота 3</h2>
            <h4>На шахівниці N×N в клітці (x1, y1) стоїть голодний шаховий кінь. Він хоче
потрапити до клітини (x2, y2), де росте смачна шахова трава. Яку найменшу
кількість ходів він має для цього зробити?</h4>
            <div className="inputsPlace">
                <input type="number" value={num1} onChange={e => setNum1(parseInt(e.target.value))}/>
                <input type="number" value={num2} onChange={e => setNum2(parseInt(e.target.value))}/>
                <input type="number" value={num3} onChange={e => setNum3(parseInt(e.target.value))}/>
                <input type="number" value={num4} onChange={e => setNum4(parseInt(e.target.value))}/>
                <input type="number" value={num5} onChange={e => setNum5(parseInt(e.target.value))}/>
            </div>
            <button className='solveLabButton' onClick={solve}>solve</button>
            <div className="resPlace">
                Результат: {res}
            </div>
        </div>
    )
}

export default Lab3;