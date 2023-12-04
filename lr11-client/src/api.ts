import axios, { AxiosError, AxiosResponse, isAxiosError } from 'axios';

const host = "https://localhost:7034/"

export interface LabResult{
    res1: number;
}
export interface ProfileData{
    name: string;
    email: string;
    phone: string;
}

export interface loginRes{
    registered: boolean;
}

export interface redirectUrl{
    url: string;
}

export function solveLab1(str1 : string, str2 : string):Promise<any>{
    const params = {
        str1, str2
    };
    const res = axios.get(host + "Labs/Lab1", {
        params,
        withCredentials: true
    })
    return res;
} 

export function solveLab2(number : number):Promise<any>{
    const params = {
        number
    };
    const res = axios.get(host + "Labs/Lab2", {
        params,
        withCredentials: true
    })
    return res;
} 

export function solveLab3(N:number, x1:number, y1:number, x2:number, y2:number):Promise<any>{
    const params = {
        N, x1, y1, x2, y2
    };
    const res = axios.get(host + "Labs/Lab3", {
        params,
        withCredentials: true
    })
    return res;
} 

export function profile():Promise<any>{
    const res = axios.get(host + "Home/Profile", {withCredentials: true})
    return res;
} 

export function GetUrl():Promise<any>{
    const res = axios.get(host + "OAuth/GetUrl", {withCredentials: true})
    return res;
} 

export function getCode(code: string):Promise<any>{
    const params = {
        code: code,
    };
    const res = axios.post(host + "OAuth/Code", {}, {params, withCredentials: true  })
    return res;
} 

export function Reg(phone: string):Promise<any>{
    const params = {
        phone
    };
    const res = axios.post(host + "OAuth/Reg", {}, {
        params,
        withCredentials: true
    })
    return res;
}

export function isAuted():Promise<any>{
    const res = axios.get(host + "OAuth/isAuted", {
        withCredentials: true
    })
    return res;
}