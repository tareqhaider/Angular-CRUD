import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable, observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly apiUrl = 'http://localhost:56844/';
  readonly photoUrl = 'http://localhost:56844/Photos';
  constructor(private http: HttpClient) { }

  getDepartmentList(): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + '/Department');
  }

  addDepartment(val : any){
    return this.http.post(this.apiUrl + '/Department', val);
  }

  updateDepartment(val : any){
    return this.http.put(this.apiUrl + '/Department', val);
  }

  deleteDepartment(val : any){
    return this.http.delete(this.apiUrl + '/Department', val);
  }

  getEmployeeList(): Observable<any[]>{
    return this.http.get<any>(this.apiUrl + '/Employee');
  }

  addEmployee(val : any){
    return this.http.post(this.apiUrl + '/Employee', val);
  }

  updateEmployee(val : any){
    return this.http.put(this.apiUrl + '/Employee', val);
  }

  deleteEmployee(val : any){
    return this.http.delete(this.apiUrl + '/Employee', val);
  }
}
