import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtStorageService {

  // constructor() { }
  // localstorage will not clear when you close your browser, is more permanent
  // sessioinstorage will be cleared when you close your browser.
  // sessioinStorage and localstorage are introduced in HTML5
  // cookies were there since the begining of HTML,
  getToken():string{
    return localStorage.getItem('token');
    // sessionStorage
  }

  saveToken(token:string){
    return localStorage.setItem('token',token);
  }

  destroyToken(){
    localStorage.removeItem('token');
  }

}
