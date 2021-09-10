import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { IFollow } from './following';

@Component({
  selector: 'app-following',
  templateUrl: './following.component.html',
  styleUrls: ['./following.component.css']
})
export class FollowingComponent implements OnInit {

  following : any;
  count : number = 0;
  loggedIn! : boolean;
  PhotoFilePath : string = this.apiService.PhotoUrl + "/";
  constructor(private apiService : ApiService , private router : Router) { }

  ngOnInit(): void {
    var ID = localStorage.getItem('ID') || '';
    console.log(ID);
    console.log('Id is : ' , ID);
    if(ID !=null){
      this.loggedIn = true;
      this.apiService.getFollowing(ID).subscribe((data : any) => {
        console.log("This is data: " + data);
        this.following = data;
        var i =0 ;
        data.forEach((element:any) => {
          i++;
        });
        this.count = i;
      });
    }
    else{    
      this.loggedIn = false;
  }
 }

 Unfollow(userToUnfollowID : any){
  var user : IFollow = {
    UserID : localStorage.getItem("ID") || '',
    UserToFollowID : userToUnfollowID
  }
  console.log(user);
  this.apiService.userToUnfollow(user).subscribe(data=>{
    window.location.reload();
    this.router.navigate(['./Following'])
  })
 }
}


