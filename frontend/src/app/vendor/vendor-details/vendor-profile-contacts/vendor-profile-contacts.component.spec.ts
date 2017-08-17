import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorProfileContactsComponent } from './vendor-profile-contacts.component';

describe('VendorProfileContactsComponent', () => {
  let component: VendorProfileContactsComponent;
  let fixture: ComponentFixture<VendorProfileContactsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorProfileContactsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorProfileContactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
