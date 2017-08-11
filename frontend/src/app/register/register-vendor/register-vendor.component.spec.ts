import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterVendorComponent } from './register-vendor.component';

describe('RegisterVendorComponent', () => {
  let component: RegisterVendorComponent;
  let fixture: ComponentFixture<RegisterVendorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterVendorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterVendorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
