import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  //#region Properties
  //token = '';

  //#endregion
   
  //#region Contructor  
  constructor(private http: HttpClient) {
   
  }
  //#endregion

  //#region Methods and functions 
  Login(email: string, password: string){    
    this.http.post(`${environment.urlService}/token`,{email: email, password: password})
    .subscribe((token: any) => {
          console.log(token);          
          return token;
    }, (err) => {
      console.log(err.error);       
    });
  }
  
  Logout(){
    
  }

  Register(){

  }
    
  //#endregion




}
