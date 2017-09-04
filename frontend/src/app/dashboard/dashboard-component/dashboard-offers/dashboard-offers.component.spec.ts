import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardOffersComponent } from './dashboard-offers.component';

describe('DashboardOffersComponent', () => {
  let component: DashboardOffersComponent;
  let fixture: ComponentFixture<DashboardOffersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardOffersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardOffersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
