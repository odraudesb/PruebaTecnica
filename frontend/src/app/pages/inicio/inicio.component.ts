import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon'; // Importar íconos
import { MatButtonModule } from '@angular/material/button'; // Botones
import { MatSnackBar } from '@angular/material/snack-bar'; // Notificaciones
import { PersonaService } from '../../services/persona.service';
import { Persona } from '../../interfaces/Persona';
import { PersonaComponent } from '../persona/persona.component';
import { MatDialogModule } from '@angular/material/dialog';
import { Router } from '@angular/router';
@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [
    MatCardModule, MatTableModule, CommonModule, MatInputModule, MatFormFieldModule, 
    MatIconModule, MatButtonModule, MatDialogModule
  ],
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent {
  private personaServicio = inject(PersonaService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  selectedTab: string = 'persona';  
  public displayedColumns: string[] = ['nombres', 'apellidos', 'numeroIdentificacion', 'email', 'fechaCreacion', 'acciones'];
  public dataSource = new MatTableDataSource<Persona>();
  private router = inject(Router);
  constructor() {
    this.loadPersonas();
  }

  loadPersonas() {
    this.personaServicio.lista().subscribe({
      next: (data) => {
        this.dataSource.data = data;
      },
      error: (err) => {
        console.error(err.message);
      }
    });
  }
  selectTab(tab: string): void {
    this.selectedTab = tab; 
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openPersonaModal(persona: Persona | null = null): void {
    const dialogRef = this.dialog.open(PersonaComponent, {
      width: '500px',
      data: persona
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPersonas();
      }
    });
  }

  deletePersona(id: number) {
    if (confirm('¿Estás seguro de que deseas eliminar esta persona?')) {
      this.personaServicio.deletePersona(id).subscribe({
        next: () => {
          this.snackBar.open('Persona eliminada con éxito', 'Cerrar', { duration: 3000 });
          this.loadPersonas();
        },
        error: (err) => {
          console.error(err.message);
          this.snackBar.open('Error al eliminar la persona', 'Cerrar', { duration: 3000 });
        }
      });
    }
  }
  goToUsuarios() {
    this.router.navigate(['usuario']); 
  }
}
