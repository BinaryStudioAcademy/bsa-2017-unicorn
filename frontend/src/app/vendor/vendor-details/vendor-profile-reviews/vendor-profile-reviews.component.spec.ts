import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorProfileReviewsComponent } from './vendor-profile-reviews.component';

describe('VendorProfileReviewsComponent', () => {
  let component: VendorProfileReviewsComponent;
  let fixture: ComponentFixture<VendorProfileReviewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorProfileReviewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorProfileReviewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
