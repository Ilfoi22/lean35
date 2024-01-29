import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Todo } from 'src/app/models/todo.model';
import { TodoService } from 'src/app/services/todo.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-update-todo',
  templateUrl: './update-todo.component.html',
  styleUrls: ['./update-todo.component.css']
})
export class UpdateTodoComponent implements OnInit {
  updateOldTodo: Todo = {
    id: '',
    title: '',
    description: '',
    category: '',
    createdDate: new Date(),
    isCompleted: false,
    completedDate: new Date(),
    isDeleted: false,
    deletedDate: new Date(),
    userId: ''
  }

  categories: string[] = ['Work', 'Personal', 'Shopping', 'Others']; 

  constructor(
    private todoService: TodoService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id !== null) {
      this.todoService.getTodoById(id)
        .subscribe({
          next: (todo) => {
            this.updateOldTodo = todo;
          }
        });
    }
  }

  updateTodo() {
    this.todoService.updateTodo(this.updateOldTodo.id, this.updateOldTodo)
      .subscribe({
        next: (response) => {
          this.toastr.success('Task updated successfully!', 'Success');
          console.log('Todo updated successfully:', response);
          this.router.navigate(['/todos']);
        },
        error: (error) => {
          this.toastr.error('An error occurred while updating the task.', 'Error');
          console.error('Error updating todo:', error);
        }
      });
  }
}
