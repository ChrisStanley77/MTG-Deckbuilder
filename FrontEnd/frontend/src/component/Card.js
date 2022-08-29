import React from 'react'
import '../Card.css';

const Card = ({card}) => {
  return (
    <div className='cardName'>
      { card.Name !== 'N/A' ?
        <div className='CardImage'>
          <img src={card.image} alt='card' />
          <input type='submit' value='+'/>
        </div> : ''
      }
    </div>
  )
}

export default Card