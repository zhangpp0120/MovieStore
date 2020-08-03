import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtStorageService {

  // constructor() { }
  getToken():string{
    return localStorage.getItem('token');
  }

  saveToken(token:string){
    return localStorage.setItem('token',token);
  }

  destroyToken(){
    localStorage.removeItem('token');
  }

}
