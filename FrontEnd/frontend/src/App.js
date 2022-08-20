import './App.css';
import {useState} from "react";
import { View, Image, StyleSheet } from 'react-native';
import AccountCreation from './AccountCreation';
import Login from './LoginPage';
import DecksPage from './DecksPage';
import Deck from './DeckCreation';
import {BrowserRouter, Link} from "react-router-dom";

const styles = StyleSheet.create({
  container: {
    paddingTop: 50,
  },
  Card: {
    width: auto,
    height: auto,
  },
});


const DisplayAnImage = (props) => {
  return (
    <View style={styles.container}>
      <Image
        style={styles.Card}
        source={{
          uri: 'https://reactnative.dev/img/tiny_logo.png',
        }}
      />
    </View>
  );
}

function HomePage(){
  const [search, setSearch] = useState("");
  const [cards, setCards] = useState([]);

  const handleChange = (e) => {
    const {id, value} = e.target;

    if(id === "search"){
        setSearch(value);
    }
  }
  const getData = () => {
    // Setup our URL
    const url = `http://localhost/5153/card/getallitems`;

    // Fetch our movies
    fetch(url)
      .then(resp => resp.json())
      .then(data => {
        // Do something with our data
        console.log(data);
        setCards(data.Name);
      });
  }

  const handleClick = () => {
    
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
          <button onClick={() => handleClick()} className='button' type='submit'>Search</button>
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
