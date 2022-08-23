import Deck from './Deck';

const DeckList = (props) => {
    return (
        <div className='deckList'>
            {
            props.decks.map((deck) => (
                <div key={deck.deckId}>
                <Deck deck={deck} />
                </div>
            ))
            }
        </div>
    )
}

export default DeckList