import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models/user';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  currentUser: User;
  isAuthenticated: boolean;

  constructor(public authService: AuthenticationService, private router: Router ) { }

  ngOnInit() {
    this.authService.isAuthenticated.subscribe(
      a => {
        this.isAuthenticated = a;
        if(this.isAuthenticated){
          this.currentUser = this.authService.getCurrentUser();
        }
      }
    );
  }

  logout(){
    this.authService.logout(); //destory token, defined before
    this.router.navigateByUrl('/login'); // redirect user
  }

}
