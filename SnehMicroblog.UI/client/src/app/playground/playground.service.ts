import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PlaygroundService {

  readonly WebAPIUrl="http://localhost:59901/api";
  constructor(private http : HttpClient) { }

  addTweet(val : any){
    return this.http.post(this.WebAPIUrl + '/user/newTweet' , val);
  }

  getAllTweets(userId : string){
    return this.http.get(`${this.WebAPIUrl}/user/playground/${userId}`);
  }

  editTweet(val : any){
    return this.http.put(this.WebAPIUrl + "/user/updatetweet" , val);
  }

  deleteTweet(UserId : string , TweetId : string){
    return this.http.delete(`${this.WebAPIUrl}/user/deleteTweet/${UserId}/${TweetId}`);
  }

  
}
