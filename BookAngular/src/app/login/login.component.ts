  import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
  import { FormControl, FormGroup } from '@angular/forms';
  import { ActivatedRoute, Router } from '@angular/router';
  import { TextBoxComponent } from '@progress/kendo-angular-inputs';
import { NotificationService } from '@progress/kendo-angular-notification';
  import { AuthenticateService } from '../services/authenticate.service';
  @Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
  })
  export class LoginComponent implements OnInit {
    private returnUrl : string;
    public loginForm : FormGroup;
    constructor(
      private route : ActivatedRoute,
      private router: Router,
      private notificationService: NotificationService,
      private authenticateService: AuthenticateService
    ) {
      if(this.authenticateService.currentUserValue){
        this.router.navigate(['/']);
      }
    }
    @ViewChild("password") public textbox: TextBoxComponent;
    
    ngOnInit() {
      this.loginForm = new FormGroup({
        userName: new FormControl(),
        password: new FormControl()
      });
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/book';
    }
    
    private get f(){return this.loginForm.controls}


    public submitForm(){
      if(this.loginForm.valid){
        this.authenticateService.login(this.f['userName'].value,this.f['password'].value)
        .subscribe(
          () => {
            this.router.navigate([this.returnUrl]);
          },
          err => {
            this.showNotifyError(err.error.message);
          }
        )
      }   
    }
    public clearForm(){
      this.loginForm.reset();
    }

    private showNotifyError(message : string): void {
      this.notificationService.show({
        content: `${message}`,
        cssClass: 'button-notification',
        animation: { type: 'fade', duration: 500 },
        position: { horizontal: 'right', vertical: 'top' },
        type: { style: 'error', icon: true },
        hideAfter: 2000
      });
    }

    private ngAfterViewInit(): void {
      this.textbox.input.nativeElement.type = "password";
    }
    private toggleVisibility(): void {
      const inputEl = this.textbox.input.nativeElement;
      inputEl.type = inputEl.type === "password" ? "text" : "password";
    }
  
    
  }
