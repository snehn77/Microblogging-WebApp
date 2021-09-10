import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnalyticsComponent } from './analytics/analytics.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FollowersComponent } from './followers/followers.component';
import { FollowingComponent } from './following/following.component';
import { LoginComponent } from './login/login.component';
import { PlaygroundComponent } from './playground/playground.component';
import { SearchComponent } from './search/search.component';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
  {path:'login' , component:LoginComponent},
  {path:'signup' , component:SignupComponent},
  {path:'playground' , component:PlaygroundComponent},
  {path :'dashboard' , component:DashboardComponent},
  {path :'Followers' , component:FollowersComponent},
  {path :'Following' , component:FollowingComponent},
  {path:'Search' , component:SearchComponent},
  {path :'analytics' , component:AnalyticsComponent},
  {path:'**' , redirectTo:'login',pathMatch:'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
