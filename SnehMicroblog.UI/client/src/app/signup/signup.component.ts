import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from '../api.service';
import { ISignup } from './signup';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  userImage = null;
  showValidationMessages! : Boolean;
  checkName : boolean = false;
  checkEmail : boolean = false;
  checkPassword : boolean = false;
  checkPhoneNumber : boolean = false;
  checkImage : boolean = false;
  checkCountry : boolean = false;
  imagePreview! : string;
  PhotoFileName:string = '';

  newUser : ISignup = {
      FirstName :"",
      LastName : "",
      Email : "",
      Password : "",
      PhoneNumber : "",
      Image : "",
      Country : ''
  }
  constructor(private apiService : ApiService , private router : Router , private http : HttpClient ,private toastr : ToastrService ) { }

  ngOnInit(): void {
  }


  addUser(){
    let flag = true;

    if(!this.newUser.FirstName || !this.newUser.LastName){
      this.checkName = true;
      flag = false;
    }

    if(!new RegExp("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$").test(this.newUser.Email)){
      this.checkEmail = true;
      flag = false;
    }

    if(!new RegExp("[0-9]{10}").test(this.newUser.PhoneNumber)){
      this.checkPhoneNumber = true;
      flag = false;
    }

    if(!new RegExp("^(?=.*?[a-zA-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,10}$").test(this.newUser.Password)){
      this.checkPassword = true;
      flag = false;
    }

    if(!this.newUser.Image){
      this.checkImage = true;
      flag=false;
    }

    if(!this.newUser.Country){
      this.checkCountry = true;
      flag=false;
    }
    
    if(flag){
      this.newUser.Image = this.PhotoFileName;
      console.log('Image : ' + this.newUser.Image);
      this.apiService.Signup(this.newUser).subscribe(res=>{
        console.log(res);
        this.showSuccessToast();
        this.router.navigate(['/login']);
      },
      error=>{
        this.showEmailExsistsToast();
      })
    }
  }

  showEmailExsistsToast():void{
    this.toastr.error('Email Already Exsists','Error' , {
        progressBar:true,
        timeOut:2000,
        progressAnimation:'increasing',
    });
  }

  showSuccessToast():void{
    this.toastr.success('Registered Successfully','Operation' , {
        progressBar:true,
        timeOut:2000,
        progressAnimation:'increasing',
    });
  }

  uploadPhoto(event:any){
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,file.name);

    this.apiService.UploadPhoto(formData).subscribe((data:any)=>{
      this.PhotoFileName=data.toString();
    })
  }

}
