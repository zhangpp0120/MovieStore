import { Component } from '@angular/core';
import { AuthenticationService } from './core/services/authentication.service';

// every component will has a view
// this decorator will make this component
@Component({
  selector: 'app-root',   // this is the <app-root></app-root> in the index.html
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MovieStoreSPA';
  constructor(private authService:AuthenticationService) {
    
  }

  ngOnInit(){
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.authService.populateUserInfo();
  }

}
