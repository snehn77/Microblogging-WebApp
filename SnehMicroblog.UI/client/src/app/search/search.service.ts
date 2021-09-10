import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  readonly WebAPIUrl="http://localhost:59901/api";
  constructor(private http : HttpClient) { }

  GetAllUsers(val : any){
    return this.http.post(this.WebAPIUrl + "/user/searchUser" , val);
  }

  GetAllTags(val : any){
    return this.http.post(this.WebAPIUrl + "/user/searchHashTag" , val);
  }
}
