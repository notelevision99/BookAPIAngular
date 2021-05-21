  import { Injectable } from '@angular/core';
  import { HttpClient } from "@angular/common/http";
  import { BookModel } from '../model/BookModel';
  import { Book } from "../model/Book";
  import { Observable, Observer } from 'rxjs';
  @Injectable({
    providedIn: 'root'
  })

  export class BookServiceService {
    constructor(private http: HttpClient) { }
    listBooks: BookModel[];
    baseUrl: string = "https://localhost:44313/api/books";

    GetBook(pageSize?: number, pageNumber?: number, searchString?: string): Observable<BookModel> {
      let url = (!searchString)
      ?`${this.baseUrl}/?pageSize=${pageSize}&pageNumber=${pageNumber}`
      : `${this.baseUrl}/?pageSize=${pageSize}&pageNumber=${pageNumber}&searchString=${searchString}`;

      // set defaut page size and page number.
      if (!(pageSize || pageNumber)) {
        pageSize = 5;
        pageNumber = 1;
      }
      return this.http.get<BookModel>(url);
    }
    
    GetBookById(id: string): Observable<any> {
      let result: any
      if (id !== null) {
        return result = this.http.get(this.baseUrl + `/${id}`)
      }
      else return null;
    }

    CreateBook(book: Book): Observable<any> {
      const headers = { 'Content-Type': 'application/json' }; 
      return this.http.post(this.baseUrl, JSON.stringify(book), { 'headers': headers });
    }

    EditBook(id: string, book: Book): Observable<any> {
      if (id != null) {
        const headers = { 'Content-Type': 'application/json' }; 
        return this.http.put(this.baseUrl + `/${id}`, JSON.stringify(book), { 'headers': headers });
      }
      return null;
    }

    DeleteBook(id: string): Observable<any> {
      if (id != null) {
        return this.http.delete(this.baseUrl + `/${id}`)
      }
    }

  }
