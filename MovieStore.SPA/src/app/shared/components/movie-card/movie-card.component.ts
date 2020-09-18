import { Component, OnInit, Input } from '@angular/core';
import { Movie } from './../../models/movie';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {
  @Input() movie:Movie;

  constructor() { }

  ngOnInit(): void {
  }

}

//  two ways to share data between related components
//  for sending data from Parent component to child componnet we use @input decorator
//  for emitting data from child to parent we use @output decorator