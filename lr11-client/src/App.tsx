import './App.css';
import React, { useEffect, useLayoutEffect}from 'react'
import { BrowserRouter, Routes, Route} from 'react-router-dom';
import { publicRoutes, privateRoutes } from './routes';
import NavBar from './components/NavBar';
import { isAuted } from './api';
import { useAuth } from './AuthContext';
import Home from './pages/Home';

function App() {
  const { isAuthenticated, login, logout} = useAuth();
  useEffect(() => {
    isAuted().then((res) => {
      if(res.data.authed){
        login();
      }
      else if(!res.data.authed){
        logout();
      }
    }).then(() => {
      logout();
    })
  }, []);

  return (
    <div className="App">
      <BrowserRouter>
        <NavBar/>
        <Routes>
          {
            isAuthenticated
            ?  
            <>{privateRoutes.map((route, index) => (
              <Route key={index} path={route.path} element={<route.component/>}/>
            ))}</>
            :
            <>{publicRoutes.map((route, index) => (
              <Route key={index} path={route.path} element={<route.component/>}/>
            ))}</>
          }
          <Route path="/*" element={<Home/>}/>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
