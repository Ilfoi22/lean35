import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodosComponent } from './components/todos/todos.component';
import { UpdateTodoComponent } from './components/update-todo/update-todo.component';
import { CreateTodoComponent } from './components/create-todo/create-todo.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AuthGuard } from './guards/auth.guard';
import { DeletedTodosComponent } from './components/deleted-todos/deleted-todos.component';
import { CompletedTodosComponent } from './components/completed-todos/completed-todos.component';

const routes: Routes = [
  {
    path: '',
    component: TodosComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'todos',
    component: TodosComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'completed-todos',
    component: CompletedTodosComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'deleted-todos',
    component: DeletedTodosComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'create-todos',
    component: CreateTodoComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'update-todos/:id',
    component: UpdateTodoComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'signup',
    component: SignupComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
