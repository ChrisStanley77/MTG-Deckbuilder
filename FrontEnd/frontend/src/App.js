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
  const [cards, setCards] = useState([]);
  const [type, setType] = useState("");
  const [deck, setDeck] = useState([]);

  const updateCards = () => {
    const apiUrl = ["name", "type", "set"].includes(type) 
      ? `http://localhost:80/webscraper/card/getby${type}/${search.replace(" ", "%20")}` 
      : `http://localhost:80/webscraper/card/getallitems`;
      
    fetch(apiUrl)
      .then(resp => resp.json())
      .then(data => {
        console.log(data);
        setCards(data);        
      });
  }

  const addCardToDeck = () => {
    console.log("Got the card");
  }

  const handleClick = (e) => {
    e.preventDefault();
    console.log("Handling click" + e.target);
    updateCards();
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
              <input className="search" type='text' value={search} onChange= {e => setSearch(e.target.value)} id="search" placeholder="Search"></input>
            </div>
            <div>
              <input type="radio" name="searchType" onChange= {() => {setType("name")}}/> Name
              <input type="radio" name="searchType" onChange= {() => {setType("type")}}/> Type
              <input type="radio" name="searchType" onChange= {() => {setType("set")}}/> Set
            </div>
            <div>
              <input type='submit' value='Search'/>
            </div>
          </form>
          <div className='currentDeck'>

          </div>
          <div>
            <CardList cardList={cards}/>
          </div>          
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