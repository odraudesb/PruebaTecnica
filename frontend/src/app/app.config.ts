import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HttpClientModule, provideHttpClient, withInterceptors, withFetch } from '@angular/common/http';
import { authInterceptor } from '../custom/auth.interceptor';


import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    importProvidersFrom(
      HttpClientModule,
      MatDialogModule,   
      MatButtonModule,
      MatFormFieldModule,
      MatInputModule,
      MatSelectModule,
      ReactiveFormsModule
    ),
    provideHttpClient(withFetch(), withInterceptors([authInterceptor]))
  ]
};
