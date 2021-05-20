import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Book } from 'src/app/model/Book';
import { BookServiceService } from 'src/app/services/book-service.service';

import { NotificationService } from '@progress/kendo-angular-notification';
import { Router } from '@angular/router';
@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {
  @Input() isActive = false
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Input() bookData: Book;
  constructor(private service: BookServiceService,
    private notificationService: NotificationService,
    private router: Router
  ) { }
  ngOnInit() {
    console.log(this.bookData)
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
      console.log(currentUrl);
    });
  }
  closeForm() {
    this.isActive = false
    this.cancel.emit();
  }

  onCancel() {
    this.isActive = false
    this.cancel.emit();

  }
  show(): void {
    this.notificationService.show({
      content: 'Your data has been saved',
      cssClass: 'button-notification',
      animation: { type: 'slide', duration: 400 },
      position: { horizontal: 'center', vertical: 'bottom' },
      type: { style: 'success', icon: true },
      hideAfter: 1000,
    });
  }
  onSave() {
    this.isActive = false
    this.cancel.emit();
    if (this.bookData.bookId !== null) {
      this.service.DeleteBook(this.bookData.bookId.toString()).subscribe(() => this.reloadCurrentRoute());
    }
    this.show();
  }
}
