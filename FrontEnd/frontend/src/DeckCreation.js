import React from "react";
import {useState} from "react";



function Deck(){
    const [deckName, setDeckName] = useState("");
    const deckList = [];

    const listItems = deckList.map((card) => <li>{card}</li>);

    const handleChange = (e) => {
        const {id, value} = e.target;

        if(id === "deckName"){
            setDeckName(value);
        }
    }

    const handleAddCard = (e) =>{
        //Get the card from 
    }

    const handleSubmit = () => {
        console.log(deckName, deckList);
    }


    return(
        <div id="root">
            <h1 id="deckTitle">Deck Creation</h1>
            <div>
                <input className="input" value={deckName} onChange= {(e) => handleChange(e)} placeholder="Deck Name" id="deckName"></input>
                <button onClick={() => handleSubmit()} type="submit" className="button" >Create Deck</button>
            </div>
            <div>
                <div>
                    <input className="input" type={"text"} value={cardSearch} id="cardSearch" placeholder="Search for Cards"></input>
                    <button onClick={() => handleAddCard()} type="submit" className="button" >Add Card</button>
                </div>
                <ul>
                    {listItems}
                </ul>
            </div>
        </div>
    );
}

export default Deck;