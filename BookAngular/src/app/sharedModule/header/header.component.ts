  import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/model/UserModel';
import { AuthenticateService } from 'src/app/services/authenticate.service';

  @Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
  })
  export class HeaderComponent implements OnInit {
    currentUser : UserModel;
    constructor(private authenticateServices : AuthenticateService,
      private router : Router
      ) {
      this.authenticateServices.currentUser.subscribe((user : UserModel) => {this.currentUser = user})
    };

    ngOnInit() {
    }
    
    logout() {
      this.authenticateServices.logout();
      this.router.navigate(['/login']);
    }
  }
