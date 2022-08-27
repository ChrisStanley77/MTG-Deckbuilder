import React from 'react'

const Card = ({card}) => {
  return (
    <div className='cardName'>
      { card.Name != 'N/A' ?
        <div className='CardImage'>
          <img src={card.image} alt='card image' />
          <input type='submit' value='+'/>
        </div> : ''
      }
    </div>
  )
}

export default Card