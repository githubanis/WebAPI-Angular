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
    return this.http.get<any>(this.API_URL + '/Department');
  }

  addDepartment(val: any) {
    return this.http.post(this.API_URL + '/Department', val);
  }

  updateDepartment(val: any) {
    return this.http.put(this.API_URL + '/Department', val);
  }

  deleteDepartment(val: any) {
    return this.http.delete(this.API_URL + '/Department/' + val);
  }

  getEmpList(): Observable<any[]>{
    return this.http.get<any>(this.API_URL + '/Employee');
  }

  addEmployee(val: any) {
    return this.http.post(this.API_URL + '/Employee', val);
  }

  updateEmployee(val: any) {
    return this.http.put(this.API_URL + '/Employee', val);
  }

  deleteEmployee(val: any) {
    return this.http.delete(this.API_URL + '/Employee/' + val);
  }

  uploadPhoto(val: any) {
    return this.http.post(this.API_URL + '/Employee/SaveFile', val);
  }

  GetAllDepartmentName(): Observable<any[]>{
    return this.http.get<any[]>(this.API_URL + 'Employee/GetAllDepartmentNames');
  }
}

