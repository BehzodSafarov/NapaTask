import { NgModule } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { Route, Router, RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './Components/register/register.component';
import { RegisterService } from './Services/register.service';
import { LoginService } from './Services/login.service';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './Components/login/login.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config:{
       tokenGetter : tokenGetter,allowedDomains : ["localhost:44342"],
       disallowedRoutes:[]
      }
    }),
  ],
  providers: [RegisterService,LoginService, HttpClient],
  bootstrap: [AppComponent]
})

export class AppModule { }

export function tokenGetter()
{
  return localStorage.getItem("Key")
}
