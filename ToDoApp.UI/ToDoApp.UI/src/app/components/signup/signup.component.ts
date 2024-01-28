import { Component } from '@angular/core';
import { SignupModel } from 'src/app/models/signup.model';
import { AuthService } from 'src/app/services/auth.service';

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

  constructor(private authService: AuthService) { }

  ngOnInit(): void { }

  onRegister() {
    this.authService.register(this.signUpData).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);
        console.log('Registration successful');
      },
      error: (err) => console.error(err)
    });
  }
}