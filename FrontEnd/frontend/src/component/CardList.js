import Card from './Card.js'

const CardList = (props) => {
  
  console.log(props)

  var cards = props.cardList

  //console.log("Start of cardlist: " + cards.Array);

  console.log("Incoming card list: " + cards);

  var cardArray = Array.from(cards);
  
  console.log("Card Array: " + cardArray);

  return (
    <div className='cardList'>
        {
          props.cardList.map((card) => (
            <div key={card.Name}>
              <Card card={card} />  
            </div>
          ))
        }
    </div>
  )
}

export default CardList