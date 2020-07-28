import { Component } from '@angular/core';

// every component will has a view
// this decorator will make this component
@Component({
  selector: 'app-root',   // this is the <app-root></app-root> in the index.html
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MovieStoreSPA';
}
// like partial view in MVC