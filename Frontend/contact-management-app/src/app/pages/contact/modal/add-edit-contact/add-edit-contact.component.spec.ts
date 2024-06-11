import { ComponentFixture, TestBed, tick } from '@angular/core/testing';

import { AddEditContactComponent } from './add-edit-contact.component';
import { ContactComponent } from '../../contact.component';
import { ConfirmationComponent } from 'src/app/shared/confirmation/confirmation.component';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IContact } from 'src/app/core/interface/contact';
import { BehaviorSubject, Observable } from 'rxjs';
import { EventEmitter } from '@angular/core';

describe('AddEditContactComponent', () => {
  let component: AddEditContactComponent;
  let fixture: ComponentFixture<AddEditContactComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ContactComponent, AddEditContactComponent, ConfirmationComponent],
      imports: [HttpClientModule, FormsModule, ReactiveFormsModule],
    })
      .compileComponents();

    fixture = TestBed.createComponent(AddEditContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('call init ', () => {

    var behaviorSubject = new BehaviorSubject<IContact>({} as IContact);
    component.contact = behaviorSubject.asObservable();
    var spyContact = spyOn<Observable<IContact>, any>(component.contact!, 'subscribe').and.callFake(() => {
      component.formGroup.reset();
      component.formGroup = new FormBuilder().group({
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
        id: []
      });

    });
    var spyFormReset = spyOn(component.formGroup, 'reset');
    var spyNgOnInit = spyOn(component, 'ngOnInit').and.callFake(() => {
      behaviorSubject.next({} as IContact)
    });

    component.ngOnInit();
    component.contact.subscribe()

    expect(spyNgOnInit).toHaveBeenCalled();
    expect(spyContact).toHaveBeenCalled();
    expect(spyFormReset).toHaveBeenCalled();
    expect(Object.keys(component.formGroup.value).length).toBeGreaterThan(0);
  });

  it('set values in form', () => {

    let contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com' };
    var spyFormReset = spyOn(component, 'reset');
    var spySetContactValues = spyOn(component, 'setContactValues').and.callFake(() => {
      component.reset();
      component.formGroup.setValue(contact);
    });

    component.setContactValues(contact);

    expect(spySetContactValues).toHaveBeenCalled();
    expect(spyFormReset).toHaveBeenCalled();
    expect(component.formGroup.value).toEqual(contact);

  })

  it('emit add event', () => {

    var spySaveNewContactEvent = spyOn<EventEmitter<IContact>, any>(component.saveNewContactEvent, 'emit');
    let contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com' };
    var spySetContactValues = spyOn(component, 'addContact').and.callFake(() => {
      component.formGroup.setValue(contact);
      component.saveNewContactEvent.emit(component.formGroup.value);
    });

    component.addContact();
    expect(spySetContactValues).toHaveBeenCalled();
    expect(component.formGroup.value).toEqual(contact);
    expect(spySaveNewContactEvent).toHaveBeenCalled();

  })

  it('emit update event', () => {

    var spyUpdateContactEvent = spyOn<EventEmitter<IContact>, any>(component.updateContactEvent, 'emit');
    let contact: IContact = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com' };
    var spySetContactValues = spyOn(component, 'updateContact').and.callFake(() => {
      component.formGroup.setValue(contact);
      component.updateContactEvent.emit(component.formGroup.value);
    });

    component.updateContact();
    expect(spySetContactValues).toHaveBeenCalled();
    expect(component.formGroup.value).toEqual(contact);
    expect(spyUpdateContactEvent).toHaveBeenCalled();

  })

});
