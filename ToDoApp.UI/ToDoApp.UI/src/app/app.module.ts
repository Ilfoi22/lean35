import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TodosComponent } from './components/todos/todos.component';
import { UpdateTodoComponent } from './components/update-todo/update-todo.component';
import { CreateTodoComponent } from './components/create-todo/create-todo.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { DeletedTodosComponent } from './components/deleted-todos/deleted-todos.component';
import { CompletedTodosComponent } from './components/completed-todos/completed-todos.component';

@NgModule({
  declarations: [
    AppComponent,
    TodosComponent,
    UpdateTodoComponent,
    CreateTodoComponent,
    LoginComponent,
    SignupComponent,
    DeletedTodosComponent,
    CompletedTodosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
