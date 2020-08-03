import { Observable } from 'rxjs';
import { Login } from './../../shared/models/login';
import { JwtStorageService } from './jwt-storage.service';
import { ApiService } from './api.service';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private apiService:ApiService, private jwtStorageService:JwtStorageService) { }

  Login(userLogin:Login):Observable<boolean>{
    return this.apiService.create('/account/login',userLogin)
      .pipe(map(response=>{
        if (response){
          this.jwtStorageService.saveToken(response.token);
          // this.
          return true;
        }
        return false;
      }))
  }
}
