import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  //properties
  test: any[] =[];


  constructor(private http: HttpClient) {
   
  }


  Login(email: string, password: string){
    if(email.length < 1){
        alert(email);
        return;
    }
     
    this.http.post('https://localhost:7177/api/token','{"email":"erik","pssword":"pas1234"}')
    .subscribe((resp) => {
      console.log(resp);
    });

    // this.http.get<any[]>('https://localhost:7177/api/token')
    // .subscribe((resp: any[]) =>{
    //   this.test = resp;
    //   console.log(this.test);
    // });
  }





}
