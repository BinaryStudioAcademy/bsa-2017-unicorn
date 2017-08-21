import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditPortfolioComponent } from './vendor-edit-portfolio.component';

describe('VendorEditPortfolioComponent', () => {
  let component: VendorEditPortfolioComponent;
  let fixture: ComponentFixture<VendorEditPortfolioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditPortfolioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditPortfolioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
