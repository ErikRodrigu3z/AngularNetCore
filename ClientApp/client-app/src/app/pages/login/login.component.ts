import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LoginModel } from 'src/app/models/login.model';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  //#region properties
  login!: LoginModel; 
  //#endregion
 

  constructor(private route: ActivatedRoute,
    public accountService: AccountService) { }


  ngOnInit(): void {
    this.login = new LoginModel();
    this.login.email = 'erik@gmail.com';
    this.login.password =  'pass1234';
  }

  Login(form: NgForm){
    event?.preventDefault();
    
    if (form.invalid) {
      return;
    }

    this.accountService.Login(this.login.email, this.login.password);
    

  }

}
