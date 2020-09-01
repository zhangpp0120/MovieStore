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

  reutrnUrl: string;
  user: User;
  userLogin: Login = {
    password: '',
    email: ''
  }
  invalidLogin: boolean;

  // ActivatedRoute, provides access to information about a route associated with a component that is loaded in a outlet.
  // params     params from route

  // Router, A service that provides navigation among views and URL manipulation capabilities.

  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => this.reutrnUrl = params.returnUrl || '/');
  }

  login() {
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
