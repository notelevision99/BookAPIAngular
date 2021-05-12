import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShareComponent } from './share.component';
import { DialogComponent } from './dialog/dialog.component';
import { ButtonModule } from '@progress/kendo-angular-buttons';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { NotificationModule } from '@progress/kendo-angular-notification';
@NgModule({
  
  imports: [
    CommonModule,
    DialogModule,
    ButtonModule,
    NotificationModule
  ],
  declarations: [
    ShareComponent,
    DialogComponent
  ],
  exports: [
    DialogComponent
  ]
})
export class ShareModule { }
