import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { ContactComponent } from './contact.component';
import { HttpClientModule } from '@angular/common/http';
import { AddEditContactComponent } from './modal/add-edit-contact/add-edit-contact.component';
import { ConfirmationComponent } from 'src/app/shared/confirmation/confirmation.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ContactService } from 'src/app/core/services/contact.service';
import { IContact } from 'src/app/core/interface/contact';
import { Order, Sorting, TableModel } from 'src/app/core/interface/table-model';

describe('ContactComponent', () => {
  let component: ContactComponent;
  let fixture: ComponentFixture<ContactComponent>;
  let contactServiceMock: any;
  let tableModel: TableModel = {} as TableModel;
  let contacts:IContact[] = [];
  let paginatorArray: number[] = [];

  beforeEach(async () => {

    await TestBed.configureTestingModule({
      declarations: [ContactComponent, AddEditContactComponent, ConfirmationComponent],
      imports: [HttpClientModule, FormsModule, ReactiveFormsModule],
    })
      .compileComponents();

    fixture = TestBed.createComponent(ContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    contactServiceMock = TestBed.inject(ContactService)
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch all contacts on init', () => {
    var spy = spyOn<ContactComponent, any>(component, 'fetchAllContact');
    component.ngOnInit();
    expect(spy).toHaveBeenCalled();
  });

  it('should fetch all contacts', () => {

    var spyGetAllContact = spyOn(contactServiceMock, 'getAllContact').and.callFake(() => {
      contacts.push({ id: 1, firstName: 'John', lastName: 'Doe', email:'john@example.com' }) ;
      paginatorArray.push(1)
    });

    var spyFetchAllContact = spyOn(component, 'fetchAllContact').and.callFake(()=>{
      tableModel.pageNumber = 1;
      tableModel.pageSize = 5;
      tableModel.search = 'Test';
      contactServiceMock.getAllContact();
    });

    component.ngOnInit();
    expect(spyFetchAllContact).toHaveBeenCalled();
    expect(spyGetAllContact).toHaveBeenCalled();
    expect(tableModel.pageNumber).toBe(1);
    expect(tableModel.pageSize).toBe(5);
    expect(tableModel.search).toBe('Test');
    expect(contacts.length).toBe(1);
    expect(paginatorArray.length).toBe(1);

  });

  it('should open add modal', () => {
    var spy = spyOn(component.selectedContact, 'next')    
    component.openAddModal();
    expect(spy).toHaveBeenCalled();
  });


  it('should open edit modal', () => {
    const contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com' };
    component.contacts = [contact];
    var spy = spyOn(component.selectedContact, 'next')    
    component.openEditModal(1);
    expect(spy).toHaveBeenCalled();
  });

  it('should add new contact', () => {

    var spyFetchAllContact = spyOn(component, 'fetchAllContact')
    var spyContactServiceMock = spyOn(contactServiceMock,'addContact').and.callFake(()=>{
      component.fetchAllContact();
    });
    var spyAddNewContact = spyOn(component, 'addNewContact').and.callFake(()=> {
      contactServiceMock.addContact()
    })

    const contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email:'john@example.com' };
    component.addNewContact(contact);

    expect(spyAddNewContact).toHaveBeenCalled();
    expect(spyContactServiceMock).toHaveBeenCalled();
    expect(spyFetchAllContact).toHaveBeenCalled();

  });

  it('should update contact', () => {
    var spyFetchAllContact = spyOn(component, 'fetchAllContact')
    var spyContactServiceMock = spyOn(contactServiceMock,'updateContact').and.callFake(()=>{
      component.fetchAllContact();
    });

    const contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email:'john@example.com' };

    var spyUpdateContact = spyOn(component, 'updateContact').and.callFake(()=> {
      contactServiceMock.updateContact(contact)
    })

    component.updateContact(contact);

    expect(spyUpdateContact).toHaveBeenCalled();
    expect(spyContactServiceMock).toHaveBeenCalled();
    expect(spyFetchAllContact).toHaveBeenCalled();

  });

  it('should delete contact', () => {
    var spyFetchAllContact = spyOn(component, 'fetchAllContact')
    var spyContactServiceMock = spyOn(contactServiceMock,'deleteContact').and.callFake(()=>{
      component.fetchAllContact();
    });


    var spyDeleteContact = spyOn(component, 'deleteContact').and.callFake(()=> {
      contactServiceMock.deleteContact(1)
    })

    component.deleteContact(1);

    expect(spyDeleteContact).toHaveBeenCalled();
    expect(spyContactServiceMock).toHaveBeenCalled();
    expect(spyFetchAllContact).toHaveBeenCalled();

  });


  it('should be sort by column', () => {
    
    var spyFetchAllContact = spyOn(component, 'fetchAllContact')

    var spySortColumnBy = spyOn(component, 'sortColumnBy').and.callFake(()=> {
      component.tableModel.sorting = {} as Sorting;
      component.tableModel.sorting.columnName = "Id";
      component.tableModel.sorting.order = Order.Descending;
      component.fetchAllContact();
    })

    component.sortColumnBy("Id");

    expect(spySortColumnBy).toHaveBeenCalled();
    expect(component.tableModel.sorting.columnName).toBe("Id");
    expect(component.tableModel.sorting.order).toBe(Order.Descending);
    expect(spyFetchAllContact).toHaveBeenCalled();

  });


  it('should be move to previous page', () => {
    
    component.tableModel.pageNumber = 2

    var spyFetchAllContact = spyOn(component, 'fetchAllContact').and.callFake((val:number)=>{
      component.tableModel.pageNumber = val; 
    });

    var spyPreviousPage = spyOn(component, 'previousPage').and.callFake(()=>{
      component.fetchAllContact(component.tableModel.pageNumber - 1);
    });
   
    component.previousPage();

    expect(spyPreviousPage).toHaveBeenCalled();
    expect(spyFetchAllContact).toHaveBeenCalled();
    expect(component.tableModel.pageNumber).toBe(1);

  });

  it('should be move to next page', () => {
    
    component.tableModel.pageNumber = 2

    var spyFetchAllContact = spyOn(component, 'fetchAllContact').and.callFake((val:number)=>{
      component.tableModel.pageNumber = val; 
    });

    var spyPreviousPage = spyOn(component, 'previousPage').and.callFake(()=>{
      component.fetchAllContact(component.tableModel.pageNumber + 1);
    });
   
    component.previousPage();

    expect(spyPreviousPage).toHaveBeenCalled();
    expect(spyFetchAllContact).toHaveBeenCalled();
    expect(component.tableModel.pageNumber).toBe(3);

  });


});
