import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmationComponent } from './confirmation.component';
import { HttpClientModule } from '@angular/common/http';

describe('ConfirmationComponent', () => {
  let component: ConfirmationComponent;
  let fixture: ComponentFixture<ConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmationComponent ],
      imports:[HttpClientModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('emit delete ', () => {
    var spyEmit = spyOn(component.deleteContactEvent, 'emit');      

    var spyDelete = spyOn(component, 'delete').and.callFake(()=>{
      component.deleteContactEvent.emit(1)
    });

    component.delete();

    expect(spyDelete).toHaveBeenCalled();
    expect(spyEmit).toHaveBeenCalled();

  });
});
