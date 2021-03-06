import { UserService } from './../../core/services/user.service';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from './../../core/services/movie.service';
import { Movie } from './../../shared/models/movie';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movieId: number;
  movie: Movie;
  isPurchased:boolean;
  isFavorited:boolean;
  constructor(private movieService: MovieService, private userService:UserService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      params => {
        this.movieId = +params.get('id');
        this.movieService.getMovieById(this.movieId)
          .subscribe(m => {
            this.movie = m.movie;
            this.isFavorited = m.favorited;
            this.isPurchased = m.purchased;
          });
      });
      console.log(this.movie);
  }
}
//  two ways to share data between related components
//  for sending data from Parent component to child componnet we use @input decorator
//  for emitting data from child to parent we use @output decorator