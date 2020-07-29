import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import {HttpClientModule} from '@angular/common/http';

//3rd party libraries

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { GenresComponent } from './genres/genres.component';
import { HeaderComponent } from './core/layout/header.component';
import { LoginComponent } from './auth/login/login.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { MovieCardComponent } from './shared/components/movie-card/movie-card.component';
import { from } from 'rxjs';

@NgModule({
  // controllers ->  components, if you want to use components in angular they should be declared inside atleast one module.
  declarations: [
    AppComponent,
    HomeComponent,
    GenresComponent,
    HeaderComponent,
    LoginComponent,
    SignUpComponent,
    MovieDetailsComponent,
    MovieCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  // this is for Dependency Injection
  providers: [],
  // we can select which component needs to be started when application starts
  // main --> appModule --> bootstrap AppComponent
  bootstrap: [AppComponent]
})

// it is a TypeScript class
// Decoators are like Attributes in C#
// Decorated with NgModule it is a module.
export class AppModule { }
