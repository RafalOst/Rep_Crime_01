import { HttpClient, HttpHeaders, HttpClientModule, HttpParams, HttpErrorResponse} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { IPostCrime, IReadCrime } from '../ICrime';
import { catchError, retry } from 'rxjs/operators';

export class BaseHttpService {
  public handleError(err: any): Observable<never> {
    return throwError(err);
  }

  public handleRequest<T = any>(requestObservable: Observable<T>): Observable<T> {
    return requestObservable.pipe(catchError(err => this.handleError(err)));
  }
}

@Injectable({
  providedIn: 'root'
})
export class Crime_listService extends BaseHttpService {

  apiUrlCrimes: string = 'http://rep-crime.com/api/crime/';
  //apiUrlCrimes: string = 'http://localhost:5282/api/Crime';
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  postTEST = {
    "crimeType": 2,
    "description": " nowy test aaaaaaaaaaaaaaaaaaaaaaaaa",
    "placeOfEvent": "testowa ulica k8s"
  }

  constructor(private http: HttpClient) { super()}


  showCrimes() {
    return this.http.get<JSON>(`${this.apiUrlCrimes}`);
  }

  postCrime(newCrime: IPostCrime): Observable<any> {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(newCrime);
    console.log(body);
    const request = this.http.post<any>(`${this.apiUrlCrimes}`, { ...newCrime });
    return this.handleRequest(request);
  }
}



