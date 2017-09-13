import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyCalendarComponent } from './company-calendar.component';

describe('CompanyCalendarComponent', () => {
  let component: CompanyCalendarComponent;
  let fixture: ComponentFixture<CompanyCalendarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
