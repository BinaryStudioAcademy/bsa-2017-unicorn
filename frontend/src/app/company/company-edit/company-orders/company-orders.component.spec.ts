import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyOrdersComponent } from './company-orders.component';

describe('CompanyOrdersComponent', () => {
  let component: CompanyOrdersComponent;
  let fixture: ComponentFixture<CompanyOrdersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyOrdersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
