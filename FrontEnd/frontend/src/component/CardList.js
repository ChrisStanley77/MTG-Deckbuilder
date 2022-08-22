import Movie from './Movie'

// rafce
const MovieList = (props) => {
  return (
    <div className='movieList'>
        {
          props.movies.map((movie) => (
            <div key={movie.imdbID}>
              <Movie movie={movie} />
            </div>
          ))
        }
    </div>
  )
}

export default MovieList