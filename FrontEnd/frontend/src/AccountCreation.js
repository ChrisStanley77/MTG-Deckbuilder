import React from "react";
import App from "./App";
import {useState, setState} from "react";
import './Account.css';

function AccountCreation() {
    const [email, setEmail] = useState("");
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const handleChange = (e) => {
        const {id, value} = e.target;

        if(id === "email"){
            setEmail(value);
        }

        if(id === "username"){
            setUsername(value);
        }

        if(id === "password"){
            setPassword(value);
        }

        if(id === "confirmPassword"){
            setConfirmPassword(value);
        }
    }

    const handleSubmit = () => {
        console.log(email, username, password, confirmPassword);
    }

    return(
        <div>
            <h1 className="AccountTitle">Create Account</h1>
            <div className="accountform">
                <div className="form-body">
                    <div className="email">
                        <label className="label" >Email</label>
                        <div>
                            <input className="input" type={"text"} value={email} onChange= {(e) => handleChange(e)} id="email" placeholder="Email"></input>
                        </div>
                    </div>
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
                    <div className="confirm-password">
                        <label className="label" >Confirm Password</label>
                        <div>
                        <input className="input" type={"password"} value={confirmPassword} onChange= {(e) => handleChange(e)} id="confirmPassword" placeholder="Confirm Password"></input>
                        </div>
                    </div>
                </div>
                <div className="footer">
                    <button onClick={() => handleSubmit()} type="submit" className="button">Sign Up</button>
                </div>
            </div>
        </div>
    );
}

export default AccountCreation;