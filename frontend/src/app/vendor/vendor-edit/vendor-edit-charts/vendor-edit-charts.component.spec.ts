import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditChartsComponent } from './vendor-edit-charts.component';

describe('VendorEditChartsComponent', () => {
  let component: VendorEditChartsComponent;
  let fixture: ComponentFixture<VendorEditChartsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditChartsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditChartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
