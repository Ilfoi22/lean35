import { Component, OnInit } from '@angular/core';
import { Todo } from 'src/app/models/todo.model';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-completed-todos',
  templateUrl: './completed-todos.component.html',
  styleUrls: ['./completed-todos.component.css']
})
export class CompletedTodosComponent implements OnInit {
  todos: Todo[] = [];
  currentPage = 1;
  pageSize = 10;
  isPreviousDisabled = true;
  isNextDisabled = false;

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.fetchCompletedTodos();
  }

  fetchCompletedTodos() {
    this.todoService.getCompletedTodos(this.currentPage, this.pageSize).subscribe({
      next: (todos) => {
        this.todos = todos || [];
        this.updatePaginationButtons();
      },
      error: (err) => {
        console.error('Error fetching completed todos:', err);
        this.todos = [];
        this.updatePaginationButtons();
      }
    });
  }

  goToPage(page: number) {
    this.currentPage = page;
    this.fetchCompletedTodos();
  }

  private updatePaginationButtons() {
    this.isPreviousDisabled = this.currentPage === 1;
    this.isNextDisabled = this.todos.length < this.pageSize;
  }
}
