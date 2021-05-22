  import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
  import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
  import { Router } from '@angular/router';
  import { Book } from 'src/app/model/Book';
  import { BookServiceService } from 'src/app/services/book-service.service';
  import { NotificationService } from '@progress/kendo-angular-notification';
import { BookType } from 'src/app/model/BookType';
  @Component({
    selector: 'app-upsert-book',
    templateUrl: './upsert-book.component.html',
    styleUrls: ['./upsert-book.component.scss']
  })
  export class UpsertBookComponent implements OnInit {
    @Input() isActiveDialog = false;
    @Input() isNew = false;
    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Input() bookData: Book = new Book();

    public bookType : BookType = new BookType();
    public listTypes : Array<string> = this.bookType.listTypes;
    constructor(private router: Router,
      public service: BookServiceService,
      private notificationService: NotificationService
    ) { }

    ngOnInit() {
      if (this.bookData) {
        this.loadBook();
      }
    }

    public registerForm: FormGroup = new FormGroup({
      bookId: new FormControl(),
      bookName: new FormControl(this.bookData.bookName, [Validators.required,Validators.maxLength(20)]),
      bookType: new FormControl(null, [Validators.required,Validators.maxLength(20)]),
      description: new FormControl(this.bookData.description, [Validators.required,Validators.maxLength(50)])
    });

    public close() {
      this.isActiveDialog = false;
      this.cancel.emit();
    }

    private loadBook() {
      if (this.bookData != null) {
        this.registerForm.setValue({
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

    public reloadCurrentRoute() {
      let currentUrl = this.router.url;
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate([currentUrl]);
      });
    }

    public submitForm() {     
      this.registerForm.markAllAsTouched();
      if (this.registerForm.valid) {
        var bookObservable = !this.bookData.bookId ? this.service.CreateBook(this.bookData) : this.service.EditBook(this.bookData.bookId.toString(), this.bookData);
        bookObservable.subscribe(() => {
          this.reloadCurrentRoute();
          this.isActiveDialog = false;
          this.cancel.emit();
          this.showNotify();
        });
      }
    }
        
  }
