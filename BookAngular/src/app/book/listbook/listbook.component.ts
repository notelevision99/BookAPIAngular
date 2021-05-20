  import { Component, OnInit, } from '@angular/core';
  import { FormControl, FormGroup } from '@angular/forms';
  import { Router } from '@angular/router';
  import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
  import { PageSizeChangeEvent } from '@progress/kendo-angular-pager';
  import { Book } from 'src/app/model/Book';
  import { BookModel } from 'src/app/model/BookModel';
  import { PagingModel } from 'src/app/model/PagingModel';
  import { BookServiceService } from '../../services/book-service.service';


  @Component({
    selector: 'app-listbook',
    templateUrl: './listbook.component.html',
    styleUrls: ['./listbook.component.scss']
  })
  export class ListbookComponent implements OnInit {
    bookModel: BookModel;
    booksDataGrid: any;
    book: Book;
    pagingModel: PagingModel = new PagingModel();
    gridView: GridDataResult;
    idUpDel: string;
    searchString: string;

    public isActiveDialogUpsert = false;
    public isNew = false;
    public isActiveDialogDelete = false;
    public searchForm: FormGroup = new FormGroup({
      searchString: new FormControl()
    })
    constructor(public service: BookServiceService,
      private router: Router
    ) { }

    ngOnInit() {
      this.loadBooks();
    }

    onPageChange(e: PageChangeEvent) {
      this.pagingModel.skip = e.skip;
      this.pagingModel.pageSize = e.take;
      this.pagingModel.currentPage = Math.ceil((this.pagingModel.skip / this.pagingModel.pageSize) + 1)
      this.loadBooks();
    }

    onPageSizeChange(e: PageSizeChangeEvent) {
      this.pagingModel.pageSize = Number(e.newPageSize);
      this.loadBooks();
    }

    addHandler() {
      this.isActiveDialogUpsert = true
      this.book = new Book();
      this.isNew = true;
    }

    editHandler({ dataItem }) {
      this.idUpDel = dataItem.bookId
      this.book = dataItem
      this.isActiveDialogUpsert = true;
      this.isNew = false;
    }

    removeHandler({ dataItem }) {
      this.idUpDel = dataItem.bookId
      this.isActiveDialogDelete = true
      this.book = dataItem
      this.isActiveDialogDelete = true
    }

    cancelHandler() {
      this.isActiveDialogUpsert = false;
      this.isActiveDialogDelete = false;
      this.book = new Book();
    }

    onEditBook() {
      this.isActiveDialogUpsert = false;
    }

    onSearch() {
      var searchText = this.searchForm.controls['searchString'].value;
      this.searchString = searchText;
      this.loadBooks(this.searchString);
    }


    public loadBooks(searchString?: string) {
      var getBookCallback;
      if (searchString == null) {
        getBookCallback = (this.pagingModel.skip === 0 && this.pagingModel.pageSize == 2) ? this.service.GetBook() : this.service.GetBook(this.pagingModel.pageSize, this.pagingModel.currentPage);
      }
      else {
        getBookCallback = (this.pagingModel.skip === 0 && this.pagingModel.pageSize == 2) ? this.service.GetBook() : this.service.GetBook(this.pagingModel.pageSize, this.pagingModel.currentPage, this.searchString);
      }
      getBookCallback.subscribe((book: BookModel) => {
        this.bookModel = book;
        this.pagingModel.totalCount = this.bookModel.totalCount
        this.booksDataGrid = this.bookModel.books
        this.gridView = this.booksDataGrid;
      });
    }
  }
