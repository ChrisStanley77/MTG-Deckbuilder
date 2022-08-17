import React from "react";
import {useState} from "react";
import './Account.css';

function Login(){
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const handleChange = (e) => {
        const {id, value} = e.target;

        if(id === "username"){
            setUsername(value);
        }

        if(id === "password"){
            setPassword(value);
        }
    }

    const handleSubmit = () => {
        console.log(username, password);
    }

    return (
        <div>
            <h1 className="LoginTitle">Log In</h1>
            <div className="username">
                <label className="label" >Username</label>
                <div>
                    <input className="input" type={"text"} value={username} onChange= {(e) => handleChange(e)} id="username" placeholder="Username"></input>
                </div>
            </div>
            <div className="password">
                <label className="label" >Password</label>
                <div>
                    <input className="input" type={"password"} value={password} onChange= {(e) => handleChange(e)} id="password" placeholder="Password"></input>
                </div>
            </div>
            <div className="footer">
                <button onClick={() => handleSubmit()} type="submit" className="button">Log In</button>
            </div>
        </div>
    );
}

export default Login;