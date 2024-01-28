import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo.model';
import { TodoResponse } from '../models/todo.response.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  baseApiUrl: string = "http://localhost:5096"

  constructor(private http: HttpClient) { }

  getHttpOptions() {
    const token = localStorage.getItem('token');
    if (token) {
      return {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${token}`
        })
      };
    } else {
      return {};
    }
  }

  getAllTodos(page: number = 1, pageSize: number = 10): Observable<TodoResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<TodoResponse>(`${this.baseApiUrl}/api/TodoItem`, {
      params: params,
      ...this.getHttpOptions()
    });
  }

  getCompletedTodos(page: number = 1, pageSize: number = 10): Observable<Todo[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<Todo[]>(`${this.baseApiUrl}/api/TodoItem/completed`, {
      params: params,
      ...this.getHttpOptions()
    });
  }

  getDeletedTodos(page: number = 1, pageSize: number = 10): Observable<Todo[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<Todo[]>(`${this.baseApiUrl}/api/TodoItem/deleted`, {
      params: params,
      ...this.getHttpOptions()
    });
  }

  getTodoById(id: string): Observable<Todo> {
    return this.http.get<Todo>(`${this.baseApiUrl}/api/TodoItem/${id}`);
  }

  searchByCategory(category: string): Observable<Todo[]> {
    const params = new HttpParams().set('category', category);
    return this.http.get<Todo[]>(`${this.baseApiUrl}/api/TodoItem/search`, {
      params,
      ...this.getHttpOptions()
    });
  }

  addTodo(todo: Todo): Observable<Todo> {
    return this.http.post<Todo>(`${this.baseApiUrl}/api/TodoItem`, todo, this.getHttpOptions());
  }

  updateTodo(id: string, todo: Todo): Observable<Todo> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.put<Todo>(`${this.baseApiUrl}/api/TodoItem/${id}`, todo, { headers });
  }

  deleteTodo(id: string): Observable<Todo> {
    return this.http.delete<Todo>(`${this.baseApiUrl}/api/TodoItem/${id}`, this.getHttpOptions());
  }
}
