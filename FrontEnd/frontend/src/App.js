import './App.css';
import {useState} from "react";
import { View, Image, StyleSheet } from 'react';
import AccountCreation from './AccountCreation';
import Login from './LoginPage';
import DecksPage from './DecksPage';
import Deck from './DeckCreation';
import CardList from './component/CardList';
import {BrowserRouter, Link} from "react-router-dom";
import React, { Component } from "react";

function HomePage(){
  const [search, setSearch] = useState("Consuming Aberration");
  const [type, setType] = useState("");
  const [url, setUrl] = useState(`http://localhost:80/webscraper/card/getallitems`);
  const pngs = [];

  const handleChange = (e) => {
    const {id, value} = e.target;

    if(id === "search"){
        setSearch(value);
    }
  }

  const handleSearchChange = (e) =>{
    const {id, value} = e.target;

    if(id === "Name"){
      setType(value);
      setUrl(`http://localhost:80/webscraper/card/getbyname/${search.replace(" ", "%20")}`);
    }else if(id === "Type"){
      setType(value);
      setUrl(`http://localhost:80/webscraper/card/getbytype/${search.replace(" ", "%20")}`);
    }else if(id === "Set"){
      setType(value);
      setUrl(`http://localhost:80/webscraper/card/getbyset/${search.replace(" ", "%20")}`);
    }
    //console.log(id);
  }

  const setTheUrl = () => {
    console.log(type);
    if(type === "Name"){
      setUrl(`http://localhost:80/webscraper/card/getbyname/${search.replace(" ", "%20")}`);
    }else if (type === "Type"){
      setUrl(`http://localhost:80/webscraper/card/getbytype/${search.replace(" ", "%20")}`);
    }else if (type === "Set"){
      setUrl(`http://localhost:80/webscraper/card/getbyset/${search.replace(" ", "%20")}`);
    }else{
      setUrl(`http://localhost:80/webscraper/card/getallitems`);
    }
    console.log(url);
  }

  const getData = () => {
    // Setup our URL 
    
    //console.log(url);

    // Fetch our cards
    fetch(url)
      .then(resp => resp.json())
      .then(data => {
        // Do something with our data
        // const obj = JSON.stringify(data).split("image\":\"", 100);
        // for(let str in obj){
        //   let png = obj[str].substring(0, 109)
        //   pngs.push(png);
        //   console.log(png);
        // }
        var imgs = '';
        var count = 0;
        if(data.image){
          imgs += `<img src="${data.image}" width="250px className='images' style="margin: 5px""/>`;
        }else{
          count = 0;
          for(let json in data){
            if(count < 50){
              imgs += `<img src="${data[json].image}" className='images' width="250px" style="margin: 5px"/>`;
              count++;
            }
        }
        }
        document.getElementById("cards").innerHTML = imgs;

      });
  }

  const handleClick = () => {
    setTheUrl();
    getData();
    console.log(search);
  }

  return(
    <BrowserRouter>
      <div>
        <h1 className='homeTitle'>Welcome to MTG Deck Builder</h1>
        <div className='buttonLogin'>
          <nav>
            <Link to={"/LoginPage"}>
              <button className='login' type='submit'>Login</button>
            </Link>
          </nav>
        </div>
        <div>      
          <input className="search" type={"text"} value={search} onChange= {(e) => handleChange(e)} id="search" placeholder="Search"></input>
        </div>
        <div>
          <input type="radio" value="Name" name="searchType" id='Name' onChange= {(e) => handleSearchChange(e)}/> Name
          <input type="radio" value="Type" name="searchType" id='Type' onChange= {(e) => handleSearchChange(e)}/> Type
          <input type="radio" value="Set" name="searchType" id='Set' onChange= {(e) => handleSearchChange(e)}/> Set
        </div>
        <div>
          <button onClick={() => handleClick()} className='button' type='submit'>Search</button>
          <div className='cardBox'>
            <div id="cards"></div>
          </div>
          
        </div>
      </div>
    </BrowserRouter>
  );
}

function App() {
  return (
    <div>
      {/* <AccountCreation/> */}
      {/*<Login/>*/}
      {/*<DecksPage/>*/}
      <HomePage/>
    </div>
  );
}

export default App;