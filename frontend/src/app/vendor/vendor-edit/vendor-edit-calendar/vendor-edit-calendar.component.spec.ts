import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditCalendarComponent } from './vendor-edit-calendar.component';

describe('VendorEditCalendarComponent', () => {
  let component: VendorEditCalendarComponent;
  let fixture: ComponentFixture<VendorEditCalendarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
