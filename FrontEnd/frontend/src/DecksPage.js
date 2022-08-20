import React from "react";
import {useState} from "react";

function DecksPage(){
    const [search, setSearch] = useState("");
    const [decks, setDecks] = useState([]);

    return(
        <div>
            {/*Update this tag with the user's username*/}
            <h1 className="deckPage">Your Decks Page</h1>
            <div>
                {/*List of all the decks that user has made*/}
            </div>
        </div>
    );
}

export default DecksPage;