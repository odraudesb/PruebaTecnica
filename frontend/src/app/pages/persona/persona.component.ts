import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';  // Asegúrate de importar ReactiveFormsModule
import { PersonaService } from '../../services/persona.service';
import { Persona } from '../../interfaces/Persona';

import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';  // Importar Angular Material
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
@Component({
  selector: 'app-persona',
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
  templateUrl: './persona.component.html',
  styleUrls: ['./persona.component.css']
})
export class PersonaComponent {
  personaForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private personaService: PersonaService,
    public dialogRef: MatDialogRef<PersonaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Persona | null
  ) {
    // Inicialización del formulario
    this.personaForm = this.fb.group({
      id: [data ? data.id : 0],
      nombres: [data ? data.nombres : '', Validators.required],
      apellidos: [data ? data.apellidos : '', Validators.required],
      numeroIdentificacion: [data ? data.numeroIdentificacion : '', [Validators.required, Validators.maxLength(12)]],
      email: [data ? data.email : '', [Validators.required, Validators.email]],
      tipoIdentificacion: [data ? data.tipoIdentificacion : '', Validators.required],
      fechaCreacion: new Date()
    });
  }

  onSubmit() {
    if (this.personaForm.valid) {
    
        const persona: Persona = this.personaForm.value;
    
        if (persona.id) {
          this.personaService.updatePersona(persona).subscribe(() => {
            this.dialogRef.close(true);  
          });
        } else {
        
          this.personaService.createPersona(persona).subscribe(() => {
            this.dialogRef.close(true);  
          });
        }
      }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
