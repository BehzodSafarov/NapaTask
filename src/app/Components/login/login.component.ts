import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/login.service';
import { FormControl,Validator, FormBuilder } from '@angular/forms';
import { userLogin } from 'src/app/Models/userLogin.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginservice : LoginService) { }

  ngOnInit(): void {
  }
  controlname : FormControl = new FormControl();
  controlpassword : FormControl = new FormControl();

  newLoginmodel : userLogin = new userLogin()
  responce? : boolean
  OnClickButton(){
    
  this.responce = this.loginservice.OncickButton(this.newLoginmodel)
  }

}
