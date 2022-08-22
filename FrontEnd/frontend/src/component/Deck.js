import React from 'react'

const Deck = ({deck}) => {
    return (
        <div className='deckName'>
        { deck.Name != 'N/A' ?
            <div className='DeckImage'>
            <img src={deck.Image} alt='deck image' />
            </div> : ''
        }
            <div className='DeckDetails'>

            </div>
        </div>
    );
}

export default Deck