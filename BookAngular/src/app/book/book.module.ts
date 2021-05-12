import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { ListbookComponent } from './listbook/listbook.component';
import { AppRoutingModule } from '../app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule,  } from '@progress/kendo-angular-grid';
import { SliderModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { Routes, RouterModule } from '@angular/router';
import { PagerModule } from '@progress/kendo-angular-pager';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { UpsertBookComponent } from './upsert-book/upsert-book.component';
import { ShareModule } from '../share/share.module';

const routes: Routes = [
  // { path: 'addbook', component: CreateBookComponent },
  // { path: 'editbook/:id', component: UpdateBookComponent}
];
@NgModule({
    declarations: [									     
        ListbookComponent,
        UpsertBookComponent
     ],
    imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      ButtonsModule,
      GridModule,
      PagerModule,
      SliderModule ,
      FormsModule,
      ReactiveFormsModule,
      InputsModule, 
      LabelModule,
      DialogModule,
      ShareModule,
      NotificationModule,
      RouterModule.forRoot(routes),
      BrowserAnimationsModule,
    ],
    exports: [
        ListbookComponent,
        UpsertBookComponent,
        RouterModule   
    ],
    providers: [],
    bootstrap: []
  })
  export class BookModule { }