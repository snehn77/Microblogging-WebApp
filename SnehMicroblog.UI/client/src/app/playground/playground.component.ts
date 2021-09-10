import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { IAddTweet } from './add-tweet';
import { IEditTweet } from './edit-tweet';
import { PlaygroundService } from './playground.service';

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.css']
})
export class PlaygroundComponent implements OnInit {
  loggedIn : any;
  Username : any;
  selected : any ;
  addTweetModel : IAddTweet = {
    UserID : '',
    Message : ''
  }
  editTweetModel : IEditTweet = {
    UserID : '',
    Message : '',
    TweetID : ''
  };
  ModalTitle:string = '';
  UpdatedMessageID : string = '';
  UpdatedMessage : string = '';
  ActivateTweetComp:boolean=false;
  id = localStorage.getItem('ID')?.toString();
  constructor(private router : Router , private playgroundService : PlaygroundService , private apiService : ApiService) { }

  ngOnInit(): void {
    this.loggedIn = localStorage.getItem('ID');
    console.log("This is login Id:" + this.loggedIn);
    this.Username = localStorage.getItem('Username');
    this.getTweets();
    console.log(localStorage.getItem('Image'));
  }

  getTweets(){
    var id = this.loggedIn.toString();
    this.playgroundService.getAllTweets(id).subscribe((data : any)=>{
      this.selected =data;
    });
  }
  newTweet(){
    this.addTweetModel.UserID = this.id || '';
    this.playgroundService.addTweet(this.addTweetModel).subscribe((data)=>{
      window.location.reload();
      this.router.navigate(['/dashboard']);
    })
    window.location.reload();
    this.router.navigate(['/playground']);
  }

  editTweet(TweetID : string){
    this.editTweetModel.UserID = this.id || '';
    this.editTweetModel.TweetID = this.UpdatedMessageID;
    console.log('Edit this is TweetID: ' + this.UpdatedMessageID);
    this.playgroundService.editTweet(this.editTweetModel).subscribe((data)=>{
      window.location.reload();
      this.router.navigate(['/playground']);
    })
  }

  deleteTweet(TweetID : string){
    if(this.id != null){
      this.playgroundService.deleteTweet(this.id, TweetID).subscribe(data=>{
        window.location.reload();
        this.router.navigate(['/playground']);
      });
    }
  }

  LikeTweet(TweetID : string){
    var likeTweetModel = {
      LoggedInUserID : this.id,
      TweetID : TweetID
    }
    console.log(likeTweetModel);
    this.apiService.LikeTweet(likeTweetModel).subscribe(data=>{
      window.location.reload();
      this.router.navigate(['/playground']);
    })
  }

  DislikeTweet(TweetID : string){
    if(this.id != null){
      var UserID = this.id
    this.apiService.DisLikeTweet(UserID, TweetID).subscribe(data=>{
      window.location.reload();
      this.router.navigate(['/playground']);
    })
   }
  }

  addClick(){
    this.ModalTitle="Compose New Tweet(Max 240 characters)";
    this.ActivateTweetComp=true;
  }

  closeClick(){
    this.ActivateTweetComp = false;
    this.getTweets();
  }

  openPopup(TweetID : string , Message : string){
   console.log('In edit');
   this.UpdatedMessageID = TweetID;
   this.editTweetModel.Message = Message;
   console.log(this.UpdatedMessage);
   this.ModalTitle="Edit Tweet(Max 240 characters)";
   this.ActivateTweetComp = true;
  }

  logout(){
    localStorage.removeItem('ID');
    localStorage.removeItem('Username');
    localStorage.removeItem('Image');
    localStorage.removeItem('Country');
    localStorage.removeItem('PhoneNo');
    this.loggedIn = localStorage.removeItem('ID');
    this.Username = localStorage.removeItem('Username');
    this.router.navigate(['/login']);
  }
}
