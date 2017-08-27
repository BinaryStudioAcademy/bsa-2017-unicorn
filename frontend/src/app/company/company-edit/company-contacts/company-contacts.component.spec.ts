import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyContactsComponent } from './company-contacts.component';

describe('CompanyContactsComponent', () => {
  let component: CompanyContactsComponent;
  let fixture: ComponentFixture<CompanyContactsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyContactsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyContactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
