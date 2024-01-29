import { Component } from '@angular/core';
import { SignupModel } from 'src/app/models/signup.model';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  signUpData: SignupModel = {
    firstName: '',
    secondName: '',
    email: '',
    username: '',
    password: '',
    token: ''
  };

  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router,
  ) { }

  ngOnInit(): void { }

  onRegister() {
    if (this.signUpData.firstName && this.signUpData.secondName && this.signUpData.email &&
      this.signUpData.username && this.signUpData.password) {
      this.authService.register(this.signUpData).subscribe({
        next: (response) => {
          localStorage.setItem('token', response.token);
          this.toastr.success('Registration successful', 'Success');
          console.log('Registration successful');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          this.toastr.error('Registration failed', 'Error');
          console.error(err);
        }
      });
    } else {
      this.toastr.error('Form is invalid', 'Error');
      console.log('Form is invalid');
    }
  }
}