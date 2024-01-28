import { AuthService } from './../services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth : AuthService, private router: Router, private toastr: ToastrService){

  }
  canActivate():boolean{
    if(this.auth.isLoggedIn()){
      return true
    }else{
      this.toastr.error('Please login first.', 'Error');
      this.router.navigate(['login'])
      return false;
    }
  }

}