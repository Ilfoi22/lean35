import { Component, OnInit } from '@angular/core';
import { Todo } from 'src/app/models/todo.model';
import { AuthService } from 'src/app/services/auth.service';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {
  todos: Todo[] = [];
  currentPage = 1;
  pageSize = 10;
  isPreviousDisabled = true;
  isNextDisabled = false;
  totalItems = 0;
  totalPages: number = 0;

  constructor(private todoService: TodoService, private auth: AuthService) { }

  ngOnInit(): void {
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
    this.getAllTodos();
  }

  getAllTodos() {
    this.todoService.getAllTodos(this.currentPage, this.pageSize)
      .subscribe({
        next: (response) => {
          this.todos = response.items.filter(todo => !todo.isCompleted && !todo.isDeleted);
          this.totalItems = response.totalCount;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize);
          this.updatePaginationButtons();
        },
        error: (err) => {
          console.error('Error fetching todos:', err);
        }
      });
  }

  searchByCategory(category: string) {
    if (!category) {
      this.getAllTodos();
    } else {
      this.todoService.searchByCategory(category)
        .subscribe({
          next: (todos) => {
            this.todos = todos;
            this.updatePaginationButtons();
          }
        });
    }
  }

  goToPage(page: number) {
    this.currentPage = page;
    this.getAllTodos();
  }

  onCompletedChange(id: string, todo: Todo) {
    todo.isCompleted = !todo.isCompleted;
    this.todoService.updateTodo(id, todo)
      .subscribe({
        next: (response) => {
          this.getAllTodos();
        }
      });
  }

  completeTodo(id: string, todo: Todo) {
    const updatedTodo = { ...todo, isCompleted: true };
    this.todoService.updateTodo(id, updatedTodo).subscribe({
      next: (response) => {
        this.todos = this.todos.filter(t => t.id !== id);
        this.totalItems--;
        this.totalPages = Math.ceil(this.totalItems / this.pageSize);
        this.updatePaginationButtons();
      },
      error: (err) => {
        console.error('Error completing todo:', err);
      }
    });
  }

  deleteTodo(id: string) {
    const todo = this.todos.find(t => t.id === id);
    if (todo) {
      const updatedTodo = { ...todo, isDeleted: true };

      this.todoService.updateTodo(id, updatedTodo).subscribe({
        next: (response) => {
          this.todos = this.todos.filter(t => t.id !== id);
          this.totalItems--;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize); 
          this.updatePaginationButtons();
        },
        error: (err) => {
          console.error('Error updating todo:', err);
        }
      });
    } else {
      console.error('Todo item not found');
    }
  }

  private updatePaginationButtons() {
    this.isPreviousDisabled = this.currentPage === 1;
    this.isNextDisabled = this.todos.length < this.pageSize;
  }
}
