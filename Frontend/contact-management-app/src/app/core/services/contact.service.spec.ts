import { TestBed } from '@angular/core/testing';

import { ContactService } from './contact.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Component } from '@angular/core';
import { IContact, IContactResult } from '../interface/contact';
import { of } from 'rxjs/internal/observable/of';
import { TableModel } from '../interface/table-model';

describe('ContactService', () => {
  let service: ContactService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],

    });
    service = TestBed.inject(ContactService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all contacts', () =>{
    let contactResult : IContactResult = {} as IContactResult;
    contactResult.data = [];
    contactResult.data.push({ id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com' })
    contactResult.totalRecord = contactResult.data.length;

    let tableModel:TableModel = {} as TableModel;

    var spyGetAllContact = spyOn(service, 'getAllContact').and.returnValue(of(contactResult))

    service.getAllContact(tableModel).subscribe({
      next:(val:IContactResult)=>{
        expect(val).toEqual(contactResult);
      }
    })

    expect(spyGetAllContact).toHaveBeenCalled()
  })

  it('should add new contact', () =>{

    var spyAddContact = spyOn(service, 'addContact').and.returnValue(of(true))
    let contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com' };

    service.addContact(contact).subscribe({
      next:(val:boolean) =>
        expect(val).toEqual(true)
    })
    expect(spyAddContact).toHaveBeenCalled();

  })

  it('should update contact', () =>{

    var spyUpdateContact = spyOn(service, 'updateContact').and.returnValue(of(true))
    let contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com' };

    service.updateContact(contact).subscribe({
      next:(val:boolean) =>
        expect(val).toEqual(true)
    })
    expect(spyUpdateContact).toHaveBeenCalled();

  })

  it('should delete contact', () =>{

    var spyDeleteContact = spyOn(service, 'deleteContact').and.returnValue(of(true))

    service.deleteContact(1).subscribe({
      next:(val:boolean) =>
        expect(val).toEqual(true)
    })
    expect(spyDeleteContact).toHaveBeenCalled();

  })

});
