import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyVendorsComponent } from './company-vendors.component';

describe('CompanyVendorsComponent', () => {
  let component: CompanyVendorsComponent;
  let fixture: ComponentFixture<CompanyVendorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyVendorsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyVendorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
