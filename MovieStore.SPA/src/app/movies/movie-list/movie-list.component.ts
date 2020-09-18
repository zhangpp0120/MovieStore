import { Movie } from './../../shared/models/movie';
import { MovieService } from './../../core/services/movie.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {
  movies: Movie[];
  genreId: number;
  constructor(private movieService: MovieService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      params => {
        this.genreId = +params.get('id');     // + sign here conver   id the string to  genreId number. *******************************
        this.movieService.getMoviesByGenre(this.genreId).subscribe(
          m => {
            this.movies = m,
              console.log(this.genreId);
            console.log(this.movies);
          });
      });
  }
}
