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

    fetch(url)
      .then(resp => resp.json())
      .then(data => {

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