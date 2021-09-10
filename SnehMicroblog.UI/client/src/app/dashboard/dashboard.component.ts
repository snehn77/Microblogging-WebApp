import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../api.service';
import { IDashboard } from './dashboard';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  loggedIn : any;
  userID = localStorage.getItem('ID');
  userName = localStorage.getItem('Username');
  Username : any = this.userName;
  Country = localStorage.getItem('Country');
  PhoneNumber = localStorage.getItem('PhoneNo');
  PhotoFilePath : string = this.apiService.PhotoUrl + "/" + localStorage.getItem('Image');
  user : IDashboard = {
    ID :'',
    loggedIn : false,
    followers : 0,
    following : 0
  }

  constructor(private route : Router , private router : ActivatedRoute , private apiService : ApiService) { }

  ngOnInit(): void {
    this.user.ID = this.userID || this.user.ID;
    console.log('This is UserId : ' , this.user.ID);
    this.apiService.getFollowers(this.user.ID).subscribe((data : any) => {
      var i = 0;
      data.forEach((element: string)=>{
        i++;
      });
      this.user.followers = i;
    });

    this.apiService.getFollowing(this.user.ID).subscribe((data : any) => {
      var j = 0;
      data.forEach((element: string)=>{
        j++;
      });
      this.user.following = j;
    });

    this.user.ID = localStorage.getItem('ID') || '';
    if(this.user.ID != ''){
      this.user.loggedIn = true;
    }
    else{
      this.user.loggedIn = false;
    }
  }

  logout(){
    localStorage.removeItem('ID');
    localStorage.removeItem('Username');
    localStorage.removeItem('Image');
    localStorage.removeItem('Country');
    localStorage.removeItem('PhoneNo');
    this.loggedIn = localStorage.removeItem('ID');
    this.Username = localStorage.removeItem('Username');
    this.route.navigate(['/login']);
  }

}
