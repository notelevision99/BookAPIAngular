import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
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

const routes: Routes = [
  { path: 'book', component: ListbookComponent },
];

@NgModule({
  declarations: [									
    AppComponent,
      SidebarComponent,
      HeaderComponent,
      BookComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BookModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    DialogsModule,
    NotificationModule,
   
  ],
  exports: [RouterModule],
  providers: [PagerTemplateDirective],
  bootstrap: [AppComponent]
})
export class AppModule { }
