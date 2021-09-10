import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { ISearch } from './search';
import { ISearchFilter } from './search-filter';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  ID : any;
  loggedIn! : boolean;
  tweets : any;
  users : any;

  SearchFilter : ISearchFilter = {
    SearchOption : '',
    SearchString : ''
  }
  constructor(private searchService : SearchService , private apiService : ApiService , private router : Router) { }

  ngOnInit(): void {
    this.ID = localStorage.getItem('ID');
    if(this.ID != null){
      this.loggedIn = true;
    }
    else{
      this.loggedIn = false;
    }
  }

  public Follow(UserToFollowID : string){
    var user : ISearch = {
      UserID :  this.ID,
      UserToFollowID : UserToFollowID
    }
    console.log(user);
    this.apiService.userToFollow(user).subscribe(
      data=>{
        window.location.reload();
        this.router.navigate(['/Search']);
      });
  }

  SearchUser(){
    if(this.SearchFilter.SearchOption == 'posts'){
      var tagModel = {
        UserID : this.ID,
        SearchString : this.SearchFilter.SearchString
      }
      this.searchService.GetAllTags(tagModel).subscribe((data : any)=>{
        this.tweets = data;
        this.users = [];
      })
    }
    else{
      var userModel = {
        UserID : this.ID,
        SearchString : this.SearchFilter.SearchString
      }
      this.searchService.GetAllUsers(userModel).subscribe((data : any)=>{
        this.users = data;
        this.tweets = [];
      })
    }
  }
  Option(val : any){
    this.SearchFilter.SearchOption = val;
    console.log("This is Options: " + val);
  }


  public unFollow(UserToUnfollowID : string){
    var user : ISearch = {
      UserID :  this.ID,
      UserToFollowID : UserToUnfollowID
    }
    this.apiService.userToUnfollow(user).subscribe(
      data=>{
        window.location.reload();
        this.router.navigate(['/Search']);
      });
  }

  

}
