import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorProfileInfoComponent } from './vendor-profile-info.component';

describe('VendorProfileInfoComponent', () => {
  let component: VendorProfileInfoComponent;
  let fixture: ComponentFixture<VendorProfileInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorProfileInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorProfileInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
