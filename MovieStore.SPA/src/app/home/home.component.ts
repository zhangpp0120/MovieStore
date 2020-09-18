import { Movie } from './../shared/models/movie';
import { MovieService } from './../core/services/movie.service';
import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  movies: Movie[];
  constructor(private movieService: MovieService) {}
  ngOnInit(): void {
    this.movieService.getTopMovies().subscribe((m) => {
      this.movies = m;
      console.log('show top movies');
      console.log(this.movies);
    });
  }
}