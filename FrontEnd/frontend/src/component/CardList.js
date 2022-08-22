import Card from './Card.js'

// rafce
const CardList = (props) => {
  return (
    <div className='cardList'>
        {
          props.cards?.map((card) => (
            <div key={card}>
              <Card card={card} />
            </div>
          ))
        }
    </div>
  )
}

export default CardList