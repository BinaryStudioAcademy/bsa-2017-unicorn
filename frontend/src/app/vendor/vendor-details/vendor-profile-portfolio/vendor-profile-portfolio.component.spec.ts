import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorProfilePortfolioComponent } from './vendor-profile-portfolio.component';

describe('VendorProfilePortfolioComponent', () => {
  let component: VendorProfilePortfolioComponent;
  let fixture: ComponentFixture<VendorProfilePortfolioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorProfilePortfolioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorProfilePortfolioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
