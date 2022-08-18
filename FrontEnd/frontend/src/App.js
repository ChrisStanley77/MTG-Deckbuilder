import './App.css';
import {useState} from "react";
import AccountCreation from './AccountCreation';
import Login from './LoginPage';
import DecksPage from './DecksPage';
import Deck from './DeckCreation';

function HomePage(){
  const [search, setSearch] = useState("");

  const handleChange = (e) => {
    const {id, value} = e.target;

    if(id === "search"){
        setSearch(value);
    }
  }

  const handleClick = () => {
    console.log(search);
  }


  return(
    <div>
      <h1 className='homeTitle'>Welcome to MTG Deck Builder</h1>
      <div className='buttonLogin'>
        <button className='login' type='submit'>Login</button>
      </div>
      <div>
        <input className="search" type={"text"} value={search} onChange= {(e) => handleChange(e)} id="search" placeholder="Search"></input>
      </div>
      <div>
        <button onClick={() => handleClick()} className='button' type='submit'>Search</button>
      </div>
    </div>
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
