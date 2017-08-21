import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditWorksComponent } from './vendor-edit-works.component';

describe('VendorEditWorksComponent', () => {
  let component: VendorEditWorksComponent;
  let fixture: ComponentFixture<VendorEditWorksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditWorksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditWorksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
