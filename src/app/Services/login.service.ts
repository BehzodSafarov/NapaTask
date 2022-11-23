import { Injectable } from '@angular/core';
import { userLogin } from '../Models/userLogin.model';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map, catchError, of } from 'rxjs';

@Injectable()
export class LoginService {

  constructor(private http : HttpClient) { }
  responce : boolean = false;

  OncickButton(loginModel : userLogin){
   this.http.post(environment.loginUrl,loginModel).pipe(
    map((x) => { return x;}),
    catchError((err: HttpErrorResponse)=>{ 
      return of(err.status);
    })
  ).subscribe(result =>{
    if(typeof result == "object")
    this.responce = true;
    console.log(result)
    localStorage.setItem("Key",result.toString())
   })
   
   return this.responce;
  }
}
