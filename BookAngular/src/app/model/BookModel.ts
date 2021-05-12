import { Book } from "./Book";

export class BookModel {
    totalPage: number;
    
    currentPage: number;
    pageSize: number;
    totalCount : number;
    previousPage: number;
    nextPage: number;
    books: Book[]
}
