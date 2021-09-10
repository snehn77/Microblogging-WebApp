import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { ILogin } from './login';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  invalidLoginCredentials : boolean = false;
  showValidationMessages! : boolean;
  private user : any;
  userLogin : ILogin = {
    email : '',
    password:''
  }
  constructor(private apiService : ApiService , private router : Router, private toastr : ToastrService) { }

  ngOnInit(): void {
  }

  login(){
    this.apiService.Login(this.userLogin).subscribe(
      data=>{
        this.user = data;
        console.log("This is data:" +  this.user.ID);
        localStorage.setItem('ID',this.user.ID);
        localStorage.setItem('Username' , this.user.FirstName + " "+  this.user.LastName);
        localStorage.setItem('Image' , this.user.Image);
        localStorage.setItem('Country' , this.user.Country);
        localStorage.setItem('PhoneNo' , this.user.PhoneNumber);
        this.invalidLoginCredentials = false;
        this.router.navigate(['/playground']);
      },
      error =>{
         this.showErrorToast();
      }
      )
  }

  showErrorToast():void{
    this.toastr.error('No Such Email Id registered','Error' , {
        progressBar:true,
        timeOut:2000,
        progressAnimation:'increasing',
    });
  }

}
