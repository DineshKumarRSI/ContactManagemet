import {  Component, ElementRef, EventEmitter, Input, Output } from '@angular/core';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent  {
  @Input() message: string | undefined;
  @Input() id: number = 0;
  @Output() deleteContactEvent = new EventEmitter<number>();

  delete(){
    this.deleteContactEvent.emit(this.id);
  }


}
