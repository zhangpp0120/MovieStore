import { User } from './../../shared/models/user';
import { Observable, BehaviorSubject } from 'rxjs';
import { Login } from './../../shared/models/login';
import { JwtStorageService } from './jwt-storage.service';
import { ApiService } from './api.service';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private user: User;
  private currentUserSubject = new BehaviorSubject<User>({} as User); // subject
  public currentUser = this.currentUserSubject.asObservable(); // make it public and observable, 
// observable make connections between unrelated components, related components use @input @output
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor(private apiService: ApiService, private jwtStorageService: JwtStorageService) { }

  Login(userLogin: Login): Observable<boolean> {
    return this.apiService.create('account/login', userLogin)
      .pipe(map(response => {
        if (response) {
          this.jwtStorageService.saveToken(response.token); // you can check in postman for response 
          this.populateUserInfo();
          return true;
        }
        return false; //return code 400 sth will return false
      }));
  }

  logout() {
    this.jwtStorageService.destroyToken();
    //set current user to empty;
    this.currentUserSubject.next({} as User);
    //set auth status to false;
    this.isAuthenticatedSubject.next(false);
  }

  // add data to a previously empty section 
  populateUserInfo() {
    if (this.jwtStorageService.getToken()) {
      const token = this.jwtStorageService.getToken();
      const decodedToken = this.decodedToken();
      this.currentUserSubject.next(decodedToken);
      this.isAuthenticatedSubject.next(true);
    }
  }

  // when you refresh your browser, app component is the first one to run.
  // that means that angular will reload everything, so we have to make sure if the token is present in the AppComponent init method.

  private decodedToken() {
    const token = this.jwtStorageService.getToken();
    if (!token || new JwtHelperService().isTokenExpired(token)) {
      this.logout();
      return null;
    }

    const decodedToken = new JwtHelperService().decodeToken(token);
    this.user = decodedToken;
    return this.user;
  }

  getCurrentUser(): User {
    return this.currentUserSubject.value; // get the value of this subject
  }




}
