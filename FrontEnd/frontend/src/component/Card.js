import React from 'react'

const Card = ({card}) => {
  return (
    <div className='cardName'>
      { card.Name != 'N/A' ?
        <div className='CardImage'>
          <img src={card.Image} alt='card image' />
        </div> : ''
      }
      <div className='CardDetails'>
        <div className='CardTitle'>{card.Name}</div>
        <div><label>Mana Cost</label> {card.ManaCost}</div>
        <div><label>CMC</label> {card.ConvertedManaCost}</div>
        <div><label>Card Colors</label> {card.CardColors}</div>
        <div><label>Type</label> {card.Type}</div>
        <div><label>Set</label> {card.Set}</div>
      </div>
    </div>
  )
}

export default Movie