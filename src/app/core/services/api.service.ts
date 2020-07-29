import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {map, catchError} from 'rxjs/operators';
import {environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(protected http:HttpClient) { }
  // get movies by genres
  // get all genres
  // get movies purchased by user
  // http://localhost:58601/api/genres
  // rxjs library

  // publish/subscribe

  // new letters.. we subscribe to these news letters, whenever there is a breaking news we get notification
  // we make an http call, but that http call will only be made and will only receive data when you subscribe.
  //  Observable can be finite or infinite 
  // http call is a finite observable (one request one response)

  getAll(path : string) : Observable<any[]>{
    // we need to append the common url with path that is being processed
    // map is same as select in C# LINQ
    // map TS-> select LINQ
    // filter  TS-> where  LINQ
    return this.http
      .get(`${environment.apiUrl}${path}`)
      .pipe(
        map((resp)=>resp as any[])
    );
  }
  // get movie by movie id
  // get user by user id 
  getOne(path:string, id:number): Observable<any>{
    return this.http.get(`${environment.apiUrl}${path}`+'/'+id).pipe(map((resp)=>resp as any));
  }
  // post some infomationi
  // login, signup, create movie
  create(){

  }
  update(){}
  delete(){}

}
