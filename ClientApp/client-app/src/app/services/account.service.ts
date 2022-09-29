import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import jwt_decode from "jwt-decode";
import {sweetPosition,sweetBackground,sweetColor} from 'src/app/enums/alert/sweetEnums'
import { SwalAlertsService } from 'src/app/services/swalAlerts.service';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  //#region Properties
  token!: string;
  //#endregion
   
  //#region Contructor  
  constructor(private http: HttpClient,public alerts: SwalAlertsService) {
   
  }
  //#endregion

  //#region Methods and functions 
  Login(email: string, password: string):any{    
    this.http.post(`${environment.urlService}/token`,{email: email, password: password})
    .subscribe((resp: any) => {         
          localStorage.setItem('token', resp);
          this.alerts.CloseLoader();
          return this.token = resp;           
    }, (err) => {
      this.alerts.ShowToast(err.error,sweetPosition.top, sweetBackground.error); 
    });
  }
  
  Logout(){
    
  }

  Register(){

  }
  
  RefreshToken(){

  }

  JwtDecode(token: string){
    //decode jwt
    var decoded = jwt_decode(token);
    return decoded;
  }
  //#endregion



}


