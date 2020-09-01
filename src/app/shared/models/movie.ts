import { MovieCasts } from './movieCasts';
import { MovieGenres } from './movieGenres';
import { Cast } from './cast';
export interface Movie {
  id: number;
  title: string;
  overview: string;
  budget: number;
  revenue: number;
  imdbUrl: string;
  tmdbUrl: string;
  tagline: string;
  posterUrl: string;
  backdropUrl: string;
  originalLanguage: string;
  releaseDate: Date;
  runTime: number;
  price: number;
  rating: number;
  trailer: string;
  movieCasts: MovieCasts[];
  movieGenres: MovieGenres[];
}