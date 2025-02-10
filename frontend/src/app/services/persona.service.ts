// persona.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { appsettings } from '../settings/appsettings';
import { Persona } from '../interfaces/Persona'

@Injectable({
  providedIn: 'root'
})
export class PersonaService {
  private apiUrl = `${appsettings.apiUrl}Persona`;

  constructor(private http: HttpClient) {}

  lista(): Observable<Persona[]> {
    return this.http.get<Persona[]>(`${this.apiUrl}`);
  }

  createPersona(persona: Persona): Observable<Persona> {
    return this.http.post<Persona>(`${this.apiUrl}`, persona);
  }

  updatePersona(persona: Persona): Observable<Persona> {
    return this.http.put<Persona>(`${this.apiUrl}/${persona.id}`, persona);
  }
  deletePersona(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  
}
