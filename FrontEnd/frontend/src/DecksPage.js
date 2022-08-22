import React from "react";
import DeckList from "./component/DeckList";
import {useState} from "react";
import Deck from "./component/Deck";

function DecksPage(){
    const [search, setSearch] = useState("");
    const [decks, setDecks] = useState([]);

    const getData = () => {
        const url = `http://localhost:80/webscraper/deck/getallitems`;

        fetch(url)
            .then(resp => resp.json())
            .then(data => {
                console.log(data);
                setDecks(data.search);
            })
    }

    return(
        <div>
            {/*Update this tag with the user's username*/}
            <h1 className="deckPage">Your Decks Page</h1>
            <div>
                <DeckList decks={decks} />
            </div>
        </div>
    );
}

export default DecksPage;