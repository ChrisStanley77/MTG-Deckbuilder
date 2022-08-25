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

// const styles = StyleSheet.create({
//   container: {
//     paddingTop: 50,
//   },
//   Card: {
//     width: 50,
//     height: 100,
//   },
// });


// const DisplayAnImage = (props) => {
//   return (
//     <View style={styles.container}>
//       <Image
//         style={styles.Card}
//         source={{
//           uri: 'https://reactnative.dev/img/tiny_logo.png',
//         }}
//       />
//     </View>
//   );
// }

function HomePage(){
  const [search, setSearch] = useState("Consuming Aberration");
  const pngs = [];

  const handleChange = (e) => {
    const {id, value} = e.target;

    if(id === "search"){
        setSearch(value);
    }
  }
  
  handleOptionChange: function (changeEvent) {
    this.setState({
      selectedOption: changeEvent.target.value
    });
  }

  const getData = (e) => {
    // Setup our URL 
    const {value} = e.target.value

    var url = "";

    if(value === "Name"){
      url = `http://localhost:80/webscraper/card/getbyname/${search.replace(" ", "%20")}`;
    }else if (value === "Type"){
      url = `http://localhost:80/webscraper/card/getbytype/${search.replace(" ", "%20")}`;
    }else if (value === "Set"){
      url = `http://localhost:80/webscraper/card/getbyset/${search.replace(" ", "%20")}`;
    }else{
      url = `http://localhost:80/webscraper/card/getallitems`;
    }
    console.log(url);

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

  const handleClick = (e) => {
    getData(e);
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
          <input type="radio" value="Name" name="searchType" checked={this.state.}/> Name
          <input type="radio" value="Type" name="searchType" checked={url = `http://localhost:80/webscraper/card/getbytype/${search.replace(" ", "%20")}`}/> Type
          <input type="radio" value="Set" name="searchType" checked={url = `http://localhost:80/webscraper/card/getbyset/${search.replace(" ", "%20")}`} /> Set
        </div>
        <div>
          <button onClick={(e) => handleClick(e)} className='button' type='submit'>Search</button>
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

// function App() {
//   return (
//     <div style={{ margin: '100px' }}>
//       <img src="https://reactjs.org/logo-og.png" alt="react logo" style={{ width: '400px', }}/>
//     </div>
//   );
// }

export default App;
