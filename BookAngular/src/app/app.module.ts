  import { NgModule } from '@angular/core';
  import { BrowserModule } from '@angular/platform-browser';
  import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
  import { AppRoutingModule } from './app-routing.module';
  import { AppComponent } from './app.component';

  import { SidebarComponent } from './sharedModule/sidebar/sidebar.component';
  import { HeaderComponent } from './sharedModule/header/header.component';
  import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
  import { BookModule } from "./book/book.module";
  import { PagerTemplateDirective } from '@progress/kendo-angular-grid';
  import { Routes, RouterModule } from '@angular/router'
  import { ListbookComponent } from './book/listbook/listbook.component';
  import { BookComponent } from './book/book.component';
  import { DialogsModule } from '@progress/kendo-angular-dialog';
  import { NotificationModule } from '@progress/kendo-angular-notification';
  import { LoginComponent } from './login/login.component';
  import { FormsModule, ReactiveFormsModule } from '@angular/forms';
  import { InputsModule } from '@progress/kendo-angular-inputs';
  import { LabelModule } from '@progress/kendo-angular-label';
  import { Authgard } from './helper/authgard';
  import { JWTInterceptor } from './helper/JWTInterceptor';

  const routes: Routes = [
    { path: 'book', component: ListbookComponent, canActivate: [Authgard] },
    { path: 'login', component: LoginComponent },
  ];

  @NgModule({
    declarations: [
      AppComponent,
      SidebarComponent,
      HeaderComponent,
      BookComponent,
      LoginComponent
    ],
    imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      ReactiveFormsModule,
      InputsModule,
      BookModule,
      RouterModule.forRoot(routes),
      BrowserAnimationsModule,
      DialogsModule,
      NotificationModule,
      FormsModule,
      LabelModule

    ],
    exports: [RouterModule],
    providers: [
      PagerTemplateDirective,
      { provide: HTTP_INTERCEPTORS, useClass: JWTInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
  })
  export class AppModule { }
