import { Component, OnInit } from '@angular/core';
import { Todo } from 'src/app/models/todo.model';
import { AuthService } from 'src/app/services/auth.service';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-deleted-todos',
  templateUrl: './deleted-todos.component.html',
  styleUrls: ['./deleted-todos.component.css']
})
export class DeletedTodosComponent implements OnInit {
  todos: Todo[] = [];
  currentPage = 1;
  pageSize = 10;
  isPreviousDisabled = true;
  isNextDisabled = false;

  constructor(private todoService: TodoService, private auth: AuthService) { }

  ngOnInit(): void {
    this.fetchDeletedTodos();
  }

  fetchDeletedTodos() {
    this.todoService.getDeletedTodos(this.currentPage, this.pageSize).subscribe({
      next: (todos) => {
        this.todos = todos || [];
        this.updatePaginationButtons();
      },
      error: (err) => {
        console.error('Error fetching deleted todos:', err);
        this.todos = [];
        this.updatePaginationButtons();
      }
    });
  }

  undoDelete(event: MouseEvent, todoId: string) {
    event.preventDefault();

    const todoIndex = this.todos.findIndex(t => t.id === todoId);
    if (todoIndex === -1) {
      console.error('Todo item not found');
      return;
    }

    const todoToUpdate = { ...this.todos[todoIndex], isDeleted: false };
    this.todoService.updateTodo(todoId, todoToUpdate).subscribe(() => {
      this.todos.splice(todoIndex, 1);
      this.updatePaginationButtons();
    });
  }

  goToPage(page: number) {
    this.currentPage = page;
    this.fetchDeletedTodos();
  }

  private updatePaginationButtons() {
    this.isPreviousDisabled = this.currentPage === 1;
    this.isNextDisabled = this.todos.length < this.pageSize;
  }
}
