import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
// concept call bootstrapping, bootstrapModule, (starting up your application)
// main.ts will bootstrap the application and calls AppModule.
// Every angular application should have atleast one Module, by default when u create Angular app it will create AppModule

// app.module.ts
// Angular can have multiple Modules and its a good practice to have multiple modules so that we can seperate our application into different modules
// AppModule
// AdminModule
// MovieModule
// ShareModule
// CoreModule