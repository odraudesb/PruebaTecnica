import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';  // Asegúrate de importar ReactiveFormsModule
import { AccesoService } from '../../services/acceso.services';
import { Usuario } from '../../interfaces/Usuario';

import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';  // Importar Angular Material
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,   
    MatFormFieldModule, 
    MatInputModule, 
    MatButtonModule,
    MatDialogModule,
    MatSelectModule
  ],
  templateUrl: './usuarioAddEdit.component.html',
  styleUrls: ['./usuarioAddEdit.component.css']
})
export class UsuarioAddEditComponent {
    usuarioForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private usuarioService: AccesoService,
    public dialogRef: MatDialogRef<UsuarioAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Usuario | null
  ) {
    // Inicialización del formulario
    this.usuarioForm = this.fb.group({
      id: [data ? data.id : 0],
      usuarioNombre: [data ? data.usuarioNombre : '', Validators.required],
      pass: [data ? data.pass : '', Validators.required],
      fechaCreacion: new Date()
    });
  }


  onSubmit() {
    if (this.usuarioForm.valid) {
    
        const usuario: Usuario = this.usuarioForm.value;
    
        if (usuario.id) {
          this.usuarioService.updateUsuario(usuario).subscribe(() => {
            this.dialogRef.close(true);  
          });
        } else {
        
          this.usuarioService.registrarse(usuario).subscribe(() => {
            this.dialogRef.close(true);  
          });
        }
      }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
