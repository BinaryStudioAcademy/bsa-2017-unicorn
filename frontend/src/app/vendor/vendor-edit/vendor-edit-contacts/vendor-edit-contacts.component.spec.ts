import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditContactsComponent } from './vendor-edit-contacts.component';

describe('VendorEditContactsComponent', () => {
  let component: VendorEditContactsComponent;
  let fixture: ComponentFixture<VendorEditContactsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditContactsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditContactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
