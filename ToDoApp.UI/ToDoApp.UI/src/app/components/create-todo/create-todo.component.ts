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
    completedDate: new Date(),
    isDeleted: false,
    deletedDate: new Date(),
    userId: ''
  }

  categories: string[] = ['Work', 'Personal', 'Shopping', 'Others']; 

  constructor(private todoService: TodoService, private toastr: ToastrService) { }

  ngOnInit(): void {

  }

  addTodo() {
    const userId = localStorage.getItem('userId');

    if (userId === null) {
      console.error('User ID is null. User must be logged in to add a todo.');
      return;
    }

    this.newTodo.userId = userId;
    this.todoService.addTodo(this.newTodo)
      .subscribe({
        next: (todo) => {
          console.log('Todo added:', todo);
          this.toastr.success('Task created successfully!', 'Success');
          this.resetForm();
        },
        error: (error) => {
          console.error('Error adding todo:', error);
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
      completedDate: new Date(),
      isDeleted: false,
      deletedDate: new Date(),
      userId: ''
    };
  }

}
