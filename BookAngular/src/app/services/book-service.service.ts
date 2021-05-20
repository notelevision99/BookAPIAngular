  import { Injectable } from '@angular/core';
  import { HttpClient } from "@angular/common/http";
  import { BookModel } from '../model/BookModel';
  import { Book } from "../model/Book";
  import { Observable, Observer } from 'rxjs';
  @Injectable({
    providedIn: 'root'
  })

  export class BookServiceService  {
    constructor(private http: HttpClient) { }
    listBooks: BookModel[];
    baseUrl: string = "https://localhost:44313/api/books"; 
    GetBook(pageSize?: number, pageNumber?: number, searchString? : string): Observable<BookModel> {
      let result: any;
      let url : string;
      if(searchString == null){
        url = `${this.baseUrl}/?pageSize=${pageSize}&pageNumber=${pageNumber}`;
        if (pageSize !== undefined && pageNumber !== undefined) {
          result = this.http.get(url,{withCredentials:true});
          return result;
        } else {
          pageSize = 5;
          pageNumber = 1;
          result = this.http.get(url);
          return result;
        }
      }
      else{
        url = `${this.baseUrl}/?pageSize=${pageSize}&pageNumber=${pageNumber}&searchString=${searchString}`;
        if (pageSize !== undefined && pageNumber !== undefined) {      
          result = this.http.get(url);
          return result;
        } else {
          pageSize = 5;
          pageNumber = 1;
          result = this.http.get(`${this.baseUrl}/?pageSize=${pageSize}&pageNumber=${pageNumber}&searchString=${searchString}`);
          return result;
        } 
      }
    }
    GetBookById(id : string) : Observable<any> {
      let result : any
      if (id !== null){
        return result = this.http.get(this.baseUrl + `/${id}`)
      }
      else return null;
    }
    CreateBook(book: Book) : Observable<any> {
      const headers = { 'Content-Type': 'application/json' }; // ... Set content type to JSON
      return this.http.post(this.baseUrl, JSON.stringify(book), {'headers': headers});
    }
    EditBook(id : string,book: Book) : Observable<any> {
      if(id != null){
      const headers = { 'Content-Type': 'application/json' }; // ... Set content type to JSON
      return this.http.put(this.baseUrl + `/${id}`, JSON.stringify(book), {'headers': headers});   
      }
      return null;
      
    }
    DeleteBook(id: string) : Observable<any>{
      if(id != null)
      {
        return this.http.delete(this.baseUrl + `/${id}`)
      }
    }
    
  }
