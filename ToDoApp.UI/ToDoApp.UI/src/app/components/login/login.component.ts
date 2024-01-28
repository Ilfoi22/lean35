import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginModel } from 'src/app/models/login.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData: LoginModel = {
    username: '',
    password: '',
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private auth: AuthService) { }


  onLogin() {
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        console.log('Response:', response);
        localStorage.setItem('token', response.token);
        localStorage.setItem('userId', response.userId);
        console.log('Logged in successfully');
        this.toastr.success('You are successfully logged in!', 'Success');
        this.router.navigate(['/todos']);
      },
      error: (err) => {
        this.toastr.error('An error occurred while trying to sign in.', 'Error');
        console.error('Login error:', err);
      }
    });
  }
}
