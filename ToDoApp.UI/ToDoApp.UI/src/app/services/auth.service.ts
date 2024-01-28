import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { SignupModel } from '../models/signup.model';
import { LoginModel } from '../models/login.model';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseApiUrl: string = "http://localhost:5096";

    constructor(private http: HttpClient, private router: Router,) { }

    login(loginData: LoginModel): Observable<any> {
        return this.http.post(`${this.baseApiUrl}/api/User/login`, loginData);
    }

    logout(): void {
        localStorage.clear();
        this.router.navigate(['login'])
    }

    register(signUpData: SignupModel): Observable<any> {
        return this.http.post(`${this.baseApiUrl}/api/User/register`, signUpData);
    }

    storeToken(tokenValue: string) {
        localStorage.setItem('token', tokenValue)
    }

    getToken() {
        return localStorage.getItem('token')
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem('token')
    }
}
