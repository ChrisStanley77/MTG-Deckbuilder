import './App.css';
import {useState} from "react";
import AccountCreation from './AccountCreation';
import Login from './LoginPage';
import DecksPage from './DecksPage';
import Deck from './DeckCreation';
import CardList from './component/CardList';
import {BrowserRouter, Link} from "react-router-dom";
import React from "react";

function HomePage(){
  const [search, setSearch] = useState("Consuming Aberration");
  var [cards, setCards] = useState([]);
  const [type, setType] = useState("");
  const [url, setUrl] = useState(`http://localhost:80/webscraper/card/getallitems`);
  const [deck, setDeck] = useState([]);
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
  }

  const setTheUrl = () => {
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
        // var imgs = '';
        // var count = 0;
        // if(data.image){
        //   imgs += `<div>
        //   <img src="${data.image}" id="${data.name}" width="250px className='images'" margin="30px"/>
        //   <button className="button" type="submit" onClick={() => addCardToDeck()}>+</button>
        //   </div>`;
        // }else{
        //   for(let json in data){
        //     imgs += `<div>
        //     <img src="${data[json].image}" id="${data[json].name}" className='images' width="250px" margin="30px"/>
        //     <button className="button" type="submit" onClick={(e) => addCardToDeck(e)}>+</button>
        //     </div>`;
        // }
        // }
        //document.getElementById("cards").innerHTML = imgs;


        //setCards(cards);
        for(var card in cards){
          cards.pop()
        }
        
        for(var i in data){
          cards.push(data[i])
        }
        
      });
  }

  const handleClick = (e) => {
    e.preventDefault();
    setTheUrl();
    getData();
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
        <section>
          <form className='searchForm' onSubmit={handleClick}>
            <div>      
              <input className="search" type='text' value={search} onChange= {(e) => handleChange(e)} id="search" placeholder="Search"></input>
            </div>
            <div>
              <input type="radio" value="Name" name="searchType" id='Name' onChange= {(e) => handleSearchChange(e)}/> Name
              <input type="radio" value="Type" name="searchType" id='Type' onChange= {(e) => handleSearchChange(e)}/> Type
              <input type="radio" value="Set" name="searchType" id='Set' onChange= {(e) => handleSearchChange(e)}/> Set
            </div>
            <div>
              <input type='submit' value='Search'/>
            </div>
          </form>
          <CardList cardList={cards}/>
          {/* <div className="deckList" id="deck"></div>
          <div id='cards' className='cardBox'></div> */}
        </section>
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