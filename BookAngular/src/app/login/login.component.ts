  import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
  import { FormControl, FormGroup } from '@angular/forms';
  import { ActivatedRoute, Router } from '@angular/router';
  import { TextBoxComponent } from '@progress/kendo-angular-inputs';
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
      private authenticateService: AuthenticateService
    ) {
      if(this.authenticateService.currentUserValue){
        this.router.navigate['/book'];
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
      this.authenticateService.login(this.f['userName'].value,this.f['password'].value)
      .subscribe(
        () => {
          this.router.navigate([this.returnUrl]);
        }
      )
    }
    public clearForm(){
      this.loginForm.reset();
    }

    private ngAfterViewInit(): void {
      this.textbox.input.nativeElement.type = "password";
    }
    private toggleVisibility(): void {
      const inputEl = this.textbox.input.nativeElement;
      inputEl.type = inputEl.type === "password" ? "text" : "password";
    }
  
    
  }
