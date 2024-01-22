import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  baseApiUrl: string = "http://localhost:5096"

  constructor(private http: HttpClient) { }

  getAllTodos(page: number = 1, pageSize: number = 10): Observable<Todo[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<Todo[]>(`${this.baseApiUrl}/api/TodoItem`, { params });
  }

  getTodoById(id: string): Observable<Todo> {
    return this.http.get<Todo>(`${this.baseApiUrl}/api/TodoItem/${id}`);
  }

  searchByCategory(category: string): Observable<Todo[]> {
    const params = new HttpParams().set('category', category);
    return this.http.get<Todo[]>(`${this.baseApiUrl}/api/TodoItem/search`, { params });
  }

  addTodo(newTodo: Todo): Observable<Todo> {
    newTodo.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Todo>(`${this.baseApiUrl}/api/TodoItem`, newTodo);
  }

  updateTodo(id: string, todo: Todo): Observable<Todo> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.put<Todo>(`${this.baseApiUrl}/api/TodoItem/${id}`, todo, { headers });
  }

  deleteTodo(id: string): Observable<Todo> {
    return this.http.delete<Todo>(`${this.baseApiUrl}/api/TodoItem/${id}`);
  }
}
