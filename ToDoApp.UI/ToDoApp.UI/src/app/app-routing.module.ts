import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodosComponent } from './components/todos/todos.component';
import { UpdateTodoComponent } from './components/update-todo/update-todo.component';
import { CreateTodoComponent } from './components/create-todo/create-todo.component';

const routes: Routes = [
  {
    path: '',
    component: TodosComponent
  },
  {
    path: 'todos',
    component: TodosComponent
  },
  {
    path: 'create-todos',
    component: CreateTodoComponent
  },
  {
    path: 'update-todos/:id',
    component: UpdateTodoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
