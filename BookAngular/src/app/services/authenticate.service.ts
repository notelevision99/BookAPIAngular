    import { HttpClient } from '@angular/common/http';
    import { Injectable } from '@angular/core';
    import { BehaviorSubject, Observable } from 'rxjs';
    import { UserModel } from '../model/UserModel';
    import { map } from 'rxjs/operators';
    @Injectable({
      providedIn: 'root'
    })
    export class AuthenticateService {
      private loginUrl = "https://localhost:44313/api/auth/login";
      private currentUserSubject : BehaviorSubject<UserModel>;
      public currentUser : Observable<UserModel>;

      constructor(private http : HttpClient) {
        this.currentUserSubject = new BehaviorSubject<UserModel>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
      }
      public get currentUserValue() : UserModel{
        return this.currentUserSubject.value;
      }
      login(userName: string, password: string) {
        return this.http.post<any>(this.loginUrl, {userName, password}).pipe(map(user => {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
          return user;
        }))
      }
      logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

  }
