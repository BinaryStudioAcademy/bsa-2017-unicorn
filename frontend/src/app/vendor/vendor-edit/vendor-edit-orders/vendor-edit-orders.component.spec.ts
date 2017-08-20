import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditOrdersComponent } from './vendor-edit-orders.component';

describe('VendorEditOrdersComponent', () => {
  let component: VendorEditOrdersComponent;
  let fixture: ComponentFixture<VendorEditOrdersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditOrdersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
