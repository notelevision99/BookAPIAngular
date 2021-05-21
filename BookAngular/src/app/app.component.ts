  import { Component } from '@angular/core';
  import { Router } from '@angular/router';
  import { UserModel } from './model/UserModel';
  import { AuthenticateService } from './services/authenticate.service';

  @Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
  })

  export class AppComponent {
    public currentUser: UserModel;
    constructor(private router: Router,
      private authenticateService: AuthenticateService
    ) {
      this.authenticateService.currentUser.subscribe((user: UserModel) => { this.currentUser = user });
    }

    ngOnInit() {
      this.checkExpiredToken();
    }

    checkExpiredToken() {
      var token = JSON.parse(localStorage.getItem('currentUser'));
      if (token) {
        var dateTimeToken = token.expiration;
        var dateCompare = new Date();
        var dateTimeNow = dateCompare.toISOString();
        (dateTimeNow < dateTimeToken) ? "" : (localStorage.removeItem('currentUser'), this.reloadCurrentRoute());
      }
    }

    reloadCurrentRoute() {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/book']);
      });
    }
    
    title = 'BookStore';
  }
