import Card from './Card'

const CardList = (props) => {
  return (
    <div className='cardList'>
        {
          props.cards.map((card) => (
            <div key={card.Name}>
              <Card card={card} />
            </div>
          ))
        }
    </div>
  )
}

export default CardList