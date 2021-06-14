import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Professor } from '../models/Professor';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {

  baseURL = `${environment.mainUrlAPI}professor`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.baseURL);
  }

  getById(id: number): Observable<Professor> {
    return this.http.get<Professor>(`${this.baseURL}/${id}`);
  }

  getByAlunoId(id: number): Observable<Professor[]> {
    return this.http.get<Professor[]>(`${this.baseURL}/ByAluno/${id}`);
  }

  post(professor: Professor) {
    return this.http.post(this.baseURL, Professor);
  }

  put(professor: Professor) {
    return this.http.put(`${this.baseURL}/${professor.id}`, Professor);
  }

  trocarEstado(professorId: number, ativo: boolean) {
    return this.http.patch(`${this.baseURL}/${professorId}/trocarEstado`, { estado: ativo });
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
