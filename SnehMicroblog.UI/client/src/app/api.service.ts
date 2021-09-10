import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http : HttpClient) { }

  readonly WebAPIUrl="http://localhost:59901/api";
  readonly PhotoUrl = "http://localhost:59901/Photos/";

  Signup(val : any){
    return this.http.post(this.WebAPIUrl + "/User" , val);
  }

  Login(val : any){
    return this.http.post(this.WebAPIUrl + "/login" , val);
  }

  userToFollow(val:any){
    return this.http.post(this.WebAPIUrl + "/user/follow" , val)
  }

  userToUnfollow(val:any){
    return this.http.post(this.WebAPIUrl + "/user/unfollow" , val)
  }

  getFollowers(userId : string){
    return this.http.get(this.WebAPIUrl + "/user/followers/" + userId);
  }

  getFollowing(userId : string){
    return this.http.get(this.WebAPIUrl + "/user/following/" + userId);
  }

  // LoggedInUserID and TweetId
  LikeTweet(val : any){
    return this.http.post(this.WebAPIUrl + '/user/like' , val);
  }

  DisLikeTweet(UserId : string , TweetID : string){
    return this.http.delete(`${this.WebAPIUrl}/user/dislike/${UserId}/${TweetID}`);
  }

  getAnalysis(){
    return this.http.get(this.WebAPIUrl + "/analytics")
  }

  UploadPhoto(val:any){
    return this.http.post(this.WebAPIUrl+'/user/SaveFile',val);
  }

}
