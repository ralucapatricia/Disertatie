import React from 'react';
import logo from './logo.svg';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './App.css';
import SignUp from './sign-up/SignUp';
import SignIn from './sign-in/SignIn';

function App() {
  return (
    <div className="App">
      <header className="App-header">
      <Router>
      <Routes>
      <Route path="/" element={<SignIn />} />
      <Route path="/sign-up" element={<SignUp />} />

     </Routes>
     </Router>
      </header>
    </div>
  
  );
}

export default App;
