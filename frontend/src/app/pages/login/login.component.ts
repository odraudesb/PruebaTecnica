import { Component, inject } from '@angular/core';
import { AccesoService } from '../../services/acceso.services';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, NgIf } from '@angular/common'; 
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatError } from '@angular/material/form-field';
import { Usuario } from '../../interfaces/Usuario';  

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule,
    CommonModule, 
    NgIf],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  private accesoService = inject(AccesoService);
  private router = inject(Router);
  public formBuild = inject(FormBuilder);

  public formLogin: FormGroup = this.formBuild.group({
    usuarioNombre: ['', Validators.required],  
    pass: ['', Validators.required]             
  });

  
  matcher = new ErrorStateMatcher();

  iniciarSesion() {
    if (this.formLogin.invalid) return;

    const objeto: Usuario = {
         usuarioNombre: this.formLogin.value.usuarioNombre,
         pass: this.formLogin.value.pass,
         id: 0,
         fechaCreacion: new Date() 
    };

    this.accesoService.login(objeto).subscribe({
      next: (data) => {
        if (data.isSuccess) {
          localStorage.setItem("token", data.token);
          this.router.navigate(['inicio']);
        } else {
          alert("Credenciales incorrectas");
        }
      },
      error: (error) => {
        console.error(error.message);
        alert('Error al iniciar sesión, por favor intente nuevamente');
      }
    });
  }

  registrarse() {
    this.router.navigate(['registro']);
  }
}
