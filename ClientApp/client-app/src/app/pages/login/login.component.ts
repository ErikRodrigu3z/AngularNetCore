import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LoginModel } from 'src/app/models/login.model';
import { AccountService } from '../../services/account.service';
import {sweetPosition,sweetBackground,sweetColor} from 'src/app/enums/alert/sweetEnums'
import { SwalAlertsService } from '../../services/swalAlerts.service';
import Swal from 'sweetalert2';
import jwt_decode from 'jwt-decode';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  //#region properties
  login!: LoginModel;
  token!: string;  
  //#endregion                   

  constructor(private route: ActivatedRoute,
    public accountService: AccountService,
    public alerts: SwalAlertsService) { }


  ngOnInit(): void {
    this.login = new LoginModel();    
    this.login.email = 'erik@gmail.com';
    this.login.password =  'pass1234';
  }

  async Login(form: NgForm){
    //event?.preventDefault();
    var token2 = localStorage.getItem('token');
     
    if (form.invalid) {       
        this.alerts.ShowToast('Datos invalidos',sweetPosition.top, sweetBackground.error);      
        return;
    }
    this.alerts.ShowLoader();
    this.token = await this.accountService.Login(this.login.email, this.login.password);
    console.log(this.accountService.JwtDecode(token2!.toString()));
  }

}
