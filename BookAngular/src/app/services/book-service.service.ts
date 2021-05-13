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
  baseUrl: string = "https://localhost:44317/api/ProjectCompare/book";
  baseUrlPaging: string = "https://localhost:44317/api/ProjectCompare/book?pageSize=5&pageNumber=1";
  
  GetBook(pageSize?: number, pageNumber?: number, searchString? : string): Observable<BookModel> {
    let result: any;
    //Non Search
    if(searchString == null){
      if (pageSize !== undefined && pageNumber !== undefined) {
        const urlPaging = `${this.baseUrl}/?pageSize=${pageSize}&pageNumber=${pageNumber}`
        result = this.http.get(urlPaging);
        return result;
      } else {
        result = this.http.get(this.baseUrlPaging);
        return result
      }
    }
    //Search
    else if(searchString != null){
      if (pageSize !== undefined && pageNumber !== undefined) {      
        const urlPaging =  `https://localhost:44317/api/ProjectCompare/book?pageSize=${pageSize}&pageNumber=${pageNumber}&searchString=${searchString}`
        result = this.http.get(urlPaging);
        return result;
      } else {
        const urlPaging = `https://localhost:44317/api/ProjectCompare/book?${searchString}`
        result = this.http.get(urlPaging);
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
