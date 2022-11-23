import { Component, Injectable, OnInit } from '@angular/core';
import { FormControl,Validator, FormBuilder } from '@angular/forms';
import { registermodel } from 'src/app/Models/userRegister.model';
import { RegisterService } from 'src/app/Services/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private registerService : RegisterService) { }

  ngOnInit(): void {
  }
  controlname : FormControl = new FormControl();
  controlpassword : FormControl = new FormControl();
  controlemile : FormControl = new FormControl();

  newmodel : registermodel = new registermodel();

 

  onClickButton(){
   this.registerService.onclickButton(this.newmodel)
   
  }
  
}
