import { Injectable } from '@angular/core';
import {ApiService} from './api.service';
import {Observable, throwError} from 'rxjs';
import { Genre } from './../../shared/models/genre';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private apiService:ApiService ) { }
  getAllGenres(): Observable<Genre[]>{
    return this.apiService.getAll('genres');
  }
}
