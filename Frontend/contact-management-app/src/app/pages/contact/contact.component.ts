import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IContact, IContactResult } from 'src/app/core/interface/contact';
import { ContactService } from 'src/app/core/services/contact.service';
import { Order, Sorting, TableModel } from 'src/app/core/interface/table-model';
import { AddEditContactComponent } from './modal/add-edit-contact/add-edit-contact.component';
import { ConfirmationComponent } from 'src/app/shared/confirmation/confirmation.component';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  tableModel: TableModel = {} as TableModel;
  contacts: IContact[] = [];
  paginatorArray: number[] = [];
  title: string | undefined;
  type: string | undefined;
  selectedContact: BehaviorSubject<IContact> = new BehaviorSubject<IContact>({} as IContact);
  search: string | undefined;
  message: string | undefined;
  id: number = 0  ;


  constructor(private contactService: ContactService, private elementRef: ElementRef) {
  }

  ngOnInit(): void {
    this.fetchAllContact();
  }

  fetchAllContact(pageNumber: number = 1) {

    this.tableModel.pageNumber = pageNumber;
    this.tableModel.pageSize = 5;
    this.tableModel.search = this.search!;

    if (this.tableModel.sorting == null) {
      this.tableModel.sorting = {} as Sorting
      this.tableModel.sorting.columnName = "Id";
      this.tableModel.sorting.order = Order.Descending;
    }

    this.contacts = [];
    this.paginatorArray = [];

    this.contactService.getAllContact(this.tableModel).subscribe({
      next: (contactResult: IContactResult) => {
        this.contacts = [];
        this.paginatorArray = [];
        this.contacts = contactResult.data;
        for (let index = 0; index < Math.ceil(contactResult.totalRecord / this.tableModel.pageSize); index++) {
          this.paginatorArray.push(index + 1);
        }
      }
    })
  }

  openAddModal() {
    this.title = "Add New Contact";
    this.type = 'add';
    this.selectedContact.next({} as IContact);
  }

  openEditModal(id: number) {
    this.title = "Edit Contact";
    this.type = 'edit';
    this.selectedContact.next(this.contacts.find(f => f.id == id) as IContact);
  }

  addNewContact(contact: IContact) {
    contact.id = 0;
    this.contactService.addContact(contact).subscribe({
      next: (val: boolean) => {
        alert("Record added successfully");
        this.fetchAllContact(this.tableModel.pageNumber);
      }
    })
  }

  updateContact(contact: IContact) {
    this.contactService.updateContact(contact).subscribe({
      next: (val: boolean) => {
        alert("Record saved successfully")
        this.fetchAllContact(this.tableModel.pageNumber);
      }
    })
  }

  deleteContact(id: number) {
    this.contactService.deleteContact(id).subscribe({
      next: (val: boolean) => {
        alert("Record deleted successfully.");
        this.fetchAllContact(this.tableModel.pageNumber);
      }
    })
  }

  openDeleteModal(id: number) {
    var contact  = this.contacts.find(f => f.id == id);
    this.message = contact?.firstName + " " + contact?.lastName;
    this.id = contact!.id;
  }

  sortColumnBy(columnName: string) {
    if (columnName == this.tableModel.sorting.columnName) {
      this.tableModel.sorting.order = (this.tableModel.sorting.order == Order.Ascending ? Order.Descending : Order.Ascending)
    } else {
      this.tableModel.sorting.columnName = columnName;
      this.tableModel.sorting.order = Order.Ascending;
    }
    this.fetchAllContact();
  }

  previousPage(){
    var pageNumber = (this.tableModel.pageNumber>0?(this.tableModel.pageNumber-1):1);
    this.fetchAllContact(pageNumber)
  }

  nextPage(){
    var pageNumber = (this.tableModel.pageNumber< this.paginatorArray.length ?(this.tableModel.pageNumber+1):this.paginatorArray.length);
    this.fetchAllContact(pageNumber)
  }

}
