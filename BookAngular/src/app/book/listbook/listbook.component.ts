import { Component, OnInit, } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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

    // dialog field 
    public isActive = false;
    //check dialog create or edit
    public isNew = false;
    //loadBook toggle
    public isReloadBooks = false;
    public isActiveDeleteDialog = false;
    public searchForm: FormGroup = new FormGroup({
      searchString: new FormControl()
    })
    constructor(public service: BookServiceService
      ) {}
    
    ngOnInit() {
        this.loadBooks();
        console.log("On init", this.isReloadBooks)
        if(this.isReloadBooks)
        {
          this.loadBooks();
        }
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

    addHandler(){
      this.isActive = true
      this.book = new Book();
      this.isNew = true;
      this.isReloadBooks = true;
    }

    editHandler({ dataItem }) {
      this.idUpDel = dataItem.bookId 
      this.book = dataItem
      this.isActive = true;
      this.isNew = false;
      this.isReloadBooks = true;
    }

    removeHandler({ dataItem }) {
      this.idUpDel = dataItem.bookId
      this.isActiveDeleteDialog = true
      this.book = dataItem  
      this.isReloadBooks = true;
    }

    cancelHandler(){
      this.isActive = false;
      this.isActiveDeleteDialog = false;
      //Empty Data  
      this.book = new Book();
    }

    onEditBook() {
      this.isActive = false;
    }

    onSearch() {
      var searchText = this.searchForm.controls['searchString'].value;
      this.searchString = searchText;
      this.loadBooks(this.searchString);
      this.loadBooks();
    }

    public loadBooks(searchString?: string) {
      var getBookCallback;
      if(searchString == null)
      {
        getBookCallback = (this.pagingModel.skip === 0 && this.pagingModel.pageSize == 2 ) ? this.service.GetBook() : this.service.GetBook(this.pagingModel.pageSize, this.pagingModel.currentPage);
      }
      else if(searchString !== null){
        getBookCallback = (this.pagingModel.skip === 0 && this.pagingModel.pageSize == 2 ) ? this.service.GetBook() : this.service.GetBook(this.pagingModel.pageSize, this.pagingModel.currentPage, this.searchString);
      }
      getBookCallback.subscribe((book: BookModel) => {
        this.bookModel = book;
        this.pagingModel.totalCount = this.bookModel.totalCount
        this.booksDataGrid = this.bookModel.books
        this.gridView = this.booksDataGrid;
      });
    }
  }
