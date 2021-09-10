import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-analytics',
  templateUrl: './analytics.component.html',
  styleUrls: ['./analytics.component.css']
})
export class AnalyticsComponent implements OnInit {

  analysis : any ; 
  ID : string = '';
  loggedIn! : boolean;
  constructor(private apiService : ApiService) { }

  ngOnInit(): void {
    this.ID = localStorage.getItem('ID') || '';
    if(this.ID != null){
      this.loggedIn = true;
      this.apiService.getAnalysis().subscribe(data=>{
        this.analysis = data;
      })
    }
    else{
      this.loggedIn = false;
    }
  }

}
