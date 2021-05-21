  import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
  import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
  import { Router } from '@angular/router';
  import { Book } from 'src/app/model/Book';
  import { BookServiceService } from 'src/app/services/book-service.service';
  import { NotificationService } from '@progress/kendo-angular-notification';
  @Component({
    selector: 'app-upsert-book',
    templateUrl: './upsert-book.component.html',
    styleUrls: ['./upsert-book.component.scss']
  })
  export class UpsertBookComponent implements OnInit {
    @Input() isActiveDialog = false;
    @Input() isNew = false;
    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Input() bookData: Book;

    constructor(private router: Router,
      public service: BookServiceService,
      private notificationService: NotificationService
    ) { }

    ngOnInit() {
      if (this.bookData !== null) {
        this.loadBook();
      }
    }

    public registerForm: FormGroup = new FormGroup({
      bookId: new FormControl(),
      bookName: new FormControl(),
      bookType: new FormControl(),
      description: new FormControl()
    });

    public close() {
      this.isActiveDialog = false;
      this.cancel.emit();
    }

    private loadBook() {
      if (this.bookData != null) {
        this.registerForm.patchValue({
          bookId: this.bookData.bookId,
          bookName: this.bookData.bookName,
          bookType: this.bookData.bookType,
          description: this.bookData.description
        })
      }
    }

    private showNotify(): void {
      this.notificationService.show({
        content: 'Your data has been saved',
        cssClass: 'button-notification',
        animation: { type: 'slide', duration: 500 },
        position: { horizontal: 'center', vertical: 'bottom' },
        type: { style: 'success', icon: true },
        hideAfter: 1000
      });
    }

    private reloadCurrentRoute() {
      let currentUrl = this.router.url;
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate([currentUrl]);
      });
    }
    
    public submitForm() {
      this.bookData = Object.assign({}, this.registerForm.value)
      if (this.bookData.bookId == null) {
        this.service.CreateBook(this.bookData).subscribe(() => {
          this.reloadCurrentRoute();
          this.showNotify();
        });
      }
      else {
        this.service.EditBook(this.bookData.bookId.toString(), this.bookData).subscribe(() => {
          this.reloadCurrentRoute();
          this.showNotify();
        })
      }
      this.isActiveDialog = false;
      this.cancel.emit();
  
    }
  }
