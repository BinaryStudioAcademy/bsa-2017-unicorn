import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditInfoComponent } from './vendor-edit-info.component';

describe('VendorEditInfoComponent', () => {
  let component: VendorEditInfoComponent;
  let fixture: ComponentFixture<VendorEditInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
