import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticateService } from '../services/authenticate.service';


@Injectable()
export class JWTInterceptor implements HttpInterceptor {
    constructor(private authenticateServices : AuthenticateService){
    }
    intercept(request: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>>{
        let currentUser = this.authenticateServices.currentUserValue;
        if(currentUser && currentUser.token){
            request = request.clone({
                setHeaders : {
                    Authorization: `Bearer ${currentUser.token}`
                }
            })
        }
       
        return next.handle(request)
    }
}
