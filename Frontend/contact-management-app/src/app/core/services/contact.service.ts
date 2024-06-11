import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { TableModel } from '../interface/table-model';
import { Observable } from 'rxjs';
import { IContact, IContactResult } from '../interface/contact';


@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private baseURL: string = environment.base;
  constructor(private httpClient: HttpClient) { }

  getAllContact(tableModel: TableModel): Observable<IContactResult> {
    return this.httpClient.post<IContactResult>(this.baseURL + "contact/customers", tableModel)
  }

  addContact(contact:IContact):Observable<boolean>{
    return this.httpClient.post<boolean>(this.baseURL + "contact/add/customer", contact)
  }

  updateContact(contact:IContact):Observable<boolean>{
    return this.httpClient.post<boolean>(this.baseURL + "contact/update/customer", contact)
  }

  deleteContact(id:number):Observable<boolean>{
    return this.httpClient.delete<boolean>(this.baseURL + "contact/delete/customer/"+id)
  }

}
