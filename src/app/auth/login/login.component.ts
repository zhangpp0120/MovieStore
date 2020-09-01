import { Login } from './../../shared/models/login';
import { User } from './../../shared/models/user';
import { AuthenticationService } from './../../core/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
// you went to user/purchase page => it redirect to login page
// after successfully login go back to the original page that it came from (user/purchase page699999)
  reutrnUrl: string;
  user: User;
  userLogin: Login = {
    password: '',
    email: ''
  }
  // this property to display message in the UI
  invalidLogin: boolean;

  // ActivatedRoute, provides access to information about a route associated with a component that is loaded in a outlet.
  // params     params from route
  // Router, A service that provides navigation among views and URL manipulation capabilities.

  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => this.reutrnUrl = params.returnUrl || '/');
    // returnUrl go back to  the component it came from or if(null) go back to '/' home page.
  }

  login() {
    // console.log("login button !")
    // console.log(this.userLogin)

    // send userLogin object to authService login service, return boolean.
    this.authService.Login(this.userLogin).subscribe(response => {
      if (response) {
        this.router.navigate([this.reutrnUrl]);
      } else {
        this.invalidLogin = true;
      }
    },
      (err: any) => {
        this.invalidLogin = true, console.log(err);
      });
  }

}
