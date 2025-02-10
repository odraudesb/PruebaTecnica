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

import { UsuarioAddEditComponent } from '../usuarioAddEdit/usuarioAddEdit.component'; // Corrección aquí
import { MatDialogModule } from '@angular/material/dialog';
import { AccesoService } from '../../services/acceso.services';
import { Usuario } from '../../interfaces/Usuario';
import { Router } from '@angular/router';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [
    MatCardModule, MatTableModule, CommonModule, MatInputModule, MatFormFieldModule, 
    MatIconModule, MatButtonModule, MatDialogModule
  ],
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent {
  private usuarioServicio = inject(AccesoService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  public displayedColumns: string[] = ['usuarioNombre', 'pass', 'acciones']; // Agregamos columna de acciones
  public dataSource = new MatTableDataSource<Usuario>();

  constructor() {
    this.loadUsuarios();
  }

  loadUsuarios() {
    this.usuarioServicio.lista().subscribe({
      next: (data) => {
        this.dataSource.data = data;
      },
      error: (err) => {
        console.error(err.message);
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openUsuarioModal(usuario: Usuario | null = null): void {
    const dialogRef = this.dialog.open(UsuarioAddEditComponent, { 
      width: '500px',
      data: usuario
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadUsuarios();
      }
    });
  }

  deleteUsuario(id: number) {
    if (confirm('¿Estás seguro de que deseas eliminar este usuario?')) {
      this.usuarioServicio.deleteUsuario(id).subscribe({
        next: () => {
          this.snackBar.open('Usuario eliminado con éxito', 'Cerrar', { duration: 3000 });
          this.loadUsuarios();
        },
        error: (err) => {
          console.error(err.message);
          this.snackBar.open('Error al eliminar el usuario', 'Cerrar', { duration: 3000 });
        }
      });
    }
  }

  goToPersonas() {
    this.router.navigate(['inicio']); 
  }
}
