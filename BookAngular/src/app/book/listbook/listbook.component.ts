import { Component, OnInit, ViewChild, } from '@angular/core';
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
  private bookModel: BookModel;
  private booksDataGrid: any;
  private idUpDel: string;
  public searchString: string = "";

  public pagingModel: PagingModel = new PagingModel();
  public gridView: GridDataResult;
  public book: Book;
  public isActiveDialogUpsert = false;
  public isNew = false;
  public isActiveDialogDelete = false;
  public searchForm: FormGroup = new FormGroup({
    searchString: new FormControl()
  })

  constructor(public service: BookServiceService,
    private router : Router
  ) { }

  ngOnInit() {
    this.loadBooks();
  }

  public onPageChange(e: PageChangeEvent) {
    this.pagingModel.skip = e.skip;
    this.pagingModel.pageSize = e.take;
    this.pagingModel.currentPage = Math.ceil((this.pagingModel.skip / this.pagingModel.pageSize) + 1); 
    this.searchString == undefined ? this.loadBooks() : this.loadBooks(this.searchString);
    
  }

  public onPageSizeChange(e: PageSizeChangeEvent) {
    this.pagingModel.pageSize = Number(e.newPageSize);
    this.loadBooks();
  }

  public addHandler() {
    this.isActiveDialogUpsert = true;
    this.book = new Book();
    this.isNew = true;
  }

  public editHandler({ dataItem }) {
    var bookUpdate = Object.assign({}, dataItem);
    this.idUpDel = bookUpdate.bookId;
    this.book = bookUpdate;
    this.isActiveDialogUpsert = true;
    this.isNew = false;
  }

  public removeHandler({ dataItem }) {
    this.idUpDel = dataItem.bookId;
    this.isActiveDialogDelete = true;
    this.book = dataItem;
    this.isActiveDialogDelete = true;
  }

  public cancelHandler() {
    this.isActiveDialogUpsert = false;
    this.isActiveDialogDelete = false;
    this.book = new Book();
  }

  public onSearch() {
    //Back to pageNumber = 1 and skip = 0;
    this.pagingModel.skip = 0;
    this.pagingModel.currentPage = 1;
    this.loadBooks(this.searchString);
   
  }

  private loadBooks(searchString?: string) {
    var getBookCallback = (this.pagingModel.skip === 0 && this.pagingModel.pageSize == 2) ? this.service.GetBook() : this.service.GetBook(this.pagingModel.pageSize, this.pagingModel.currentPage, searchString);
    getBookCallback.subscribe((book: BookModel) => {
      this.bookModel = book;
      this.pagingModel.totalCount = this.bookModel.totalCount;
      this.booksDataGrid = this.bookModel.books;
      this.gridView = this.booksDataGrid;
    });
  }
}
