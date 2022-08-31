import Card from './Card.js'
import '../Card.css';

const CardList = (props) => {
  return (
    <div className='cardList'>
        {
          props.cardList.map((card) => (
            <div key={card.id}>
              <Card card={card} /> 
            </div>
          ))
        }
    </div>
  )
}

export default CardList