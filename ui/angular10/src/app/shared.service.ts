import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly API_URL = 'http://localhost:5000/api';
  readonly PHOTO_URL = 'http://localhost:5000/Photos';
  constructor(private http: HttpClient) { }

  getDepList(): Observable<any[]>{
    return this.http.get<any>(this.API_URL + '/department');
  }
}

