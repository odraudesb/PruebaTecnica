import { Routes } from '@angular/router';
import { LoginComponent } from '../app/pages/login/login.component';
import { RegistroComponent } from '../app/pages/registro/registro.component';
import { InicioComponent } from '../app/pages/inicio/inicio.component';
import { authGuard } from '../custom/aut.guard';
import { UsuarioComponent } from '../app/pages/usuario/usuario.component';  
import { PersonaComponent } from '../app/pages/persona/persona.component';  

export const routes: Routes = [
  { path: '', component: LoginComponent },  // Ruta principal apunta al login
  { path: 'registro', component: RegistroComponent },
  { path: 'inicio', component: InicioComponent, canActivate: [authGuard] },
  { path: 'usuario', component: UsuarioComponent }, 
  { path: 'persona', component: PersonaComponent }, 
];
