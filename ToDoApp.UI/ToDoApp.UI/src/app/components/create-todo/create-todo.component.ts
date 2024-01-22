import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Todo } from 'src/app/models/todo.model';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-create-todo',
  templateUrl: './create-todo.component.html',
  styleUrls: ['./create-todo.component.css']
})
export class CreateTodoComponent implements OnInit {
  newTodo: Todo = {
    id: '',
    title: '',
    description: '',
    category: '',
    createdDate: new Date(),
    isCompleted: false,
    completedDate: new Date()
  }

  constructor(private todoService: TodoService, private toastr: ToastrService) { }

  ngOnInit(): void {

  }

  addTodo() {
    this.todoService.addTodo(this.newTodo)
      .subscribe({
        next: (todo) => {
          this.toastr.success('Task created successfully!', 'Success');
          this.resetForm();
        },
        error: (error) => {
          this.toastr.error('An error occurred while creating the task.', 'Error');
        }
      });
  }

  resetForm() {
    this.newTodo = {
      id: '',
      title: '',
      description: '',
      category: '',
      createdDate: new Date(),
      isCompleted: false,
      completedDate: new Date()
    };
  }

}
