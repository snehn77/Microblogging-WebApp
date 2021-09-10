import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {

  users : any;
  count : number = 0;
  ID : string  = "";
  loggedIn! : boolean ; 
  PhotoFilePath : string = this.apiService.PhotoUrl + "/";
  constructor(private apiService : ApiService) { }

  ngOnInit(): void {
    this.ID = localStorage.getItem('ID') || '';
    console.log('Id is : ' , this.ID);
    if(this.ID !=null){
      this.loggedIn = true;
      this.apiService.getFollowers(this.ID).subscribe((data : any) => {
        console.log("This is data: " + data);
        this.users = data;
        var i =0 ;
        data.forEach((element:any) => {
          i++;
        });
        this.count = i;
      })
    }
    else{
      this.loggedIn = false;
     
    }
  }

}
