import { Component, OnInit } from '@angular/core';
import { Todo } from 'src/app/models/todo.model';
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

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.getAllTodos();
  }

  getAllTodos() {
    this.todoService.getAllTodos(this.currentPage, this.pageSize)
      .subscribe({
        next: (todos) => {
          this.todos = todos;
          this.updatePaginationButtons();
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

  deleteTodo(id: string) {
    this.todoService.deleteTodo(id)
      .subscribe({
        next: (response) => {
          this.getAllTodos();
        }
      });
  }

  private updatePaginationButtons() {
    this.isPreviousDisabled = this.currentPage === 1;
    this.isNextDisabled = this.todos.length < this.pageSize;
  }
}
