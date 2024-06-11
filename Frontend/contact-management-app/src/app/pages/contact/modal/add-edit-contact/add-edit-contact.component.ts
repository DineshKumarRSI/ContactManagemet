import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IContact } from 'src/app/core/interface/contact';
import { Modal } from 'bootstrap';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-add-edit-contact',
  templateUrl: './add-edit-contact.component.html',
  styleUrls: ['./add-edit-contact.component.scss']
})
export class AddEditContactComponent implements OnInit {

  @Input() title: string | undefined;
  @Input() type: string | undefined;
  @Input() contact: Observable<IContact> | undefined;

  @Output() saveNewContactEvent = new EventEmitter<IContact>();
  @Output() updateContactEvent = new EventEmitter<IContact>();

  formGroup: FormGroup = {} as FormGroup;

  private modal: Modal | undefined;

  constructor(private formBuilder: FormBuilder, private elementRef: ElementRef) {
  }

  // ngAfterViewInit(): void {
  //   const modalElement = this.elementRef.nativeElement.querySelector('#addEditContact');
  //   this.modal = new Modal(modalElement);
  // }

  ngOnInit(): void {

    if (this.contact != null && this.contact) {
      var self = this;
      this.contact.subscribe({
        next: (val: IContact) => {
          setTimeout(function(){
            if(Object.keys(val).length > 0){
              self.setContactValues(val)
            }else{
              self.formGroup.reset();
            }
          }, 100)
        }
      });
    }

    this.initForm();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.formGroup.controls
  }

  initForm() {
    this.formGroup = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      id: []
    })
  }

  setContactValues(contact: IContact) {
    this.reset();
    if (this.formGroup) {
      this.formGroup.setValue({
        firstName: contact?.firstName,
        lastName: contact?.lastName,
        email: contact?.email,
        id: contact?.id
      });
    }

  }

  addContact() {
    this.contact = this.formGroup.value
    this.saveNewContactEvent.emit(this.formGroup.value);
  }

  updateContact() {
    this.contact = this.formGroup.value
    this.updateContactEvent.emit(this.formGroup.value);
  }


  reset() {
    if (this.formGroup) {
      this.formGroup.reset();
    }
  }

}
