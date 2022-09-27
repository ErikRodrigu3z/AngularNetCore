import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  //properties


  constructor(private route: ActivatedRoute,
    public accountService: AccountService) { }


  ngOnInit(): void {
    
  }

  Login(email: string, password: string){
    event?.preventDefault();
    this.route.params
    .subscribe( params => {      
      this.accountService.Login(email, password);
       
       
    });

  }

}
