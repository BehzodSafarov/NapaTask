import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { registermodel } from '../Models/userRegister.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { map, catchError, of } from 'rxjs';

@Injectable()
export class RegisterService{

  constructor( private http : HttpClient, private router : Router) { }

  responce : boolean = true;
  onclickButton(registermodel? : registermodel) : boolean {
   
   this.http.post(environment.registerUrl, registermodel).pipe(
    map((x) => { return x;}),
    catchError((err: HttpErrorResponse)=>{ 
      return of(err.status);
    })
  ).subscribe(result => {
    if(typeof result==="number")
    this.responce = true
  })
  if(this.responce)
  this.router.navigateByUrl('app-login')
  return this.responce;

  }


}
