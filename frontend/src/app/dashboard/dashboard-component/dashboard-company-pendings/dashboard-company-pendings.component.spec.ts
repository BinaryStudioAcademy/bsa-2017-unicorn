import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardCompanyPendingsComponent } from './dashboard-company-pendings.component';

describe('DashboardCompanyPendingsComponent', () => {
  let component: DashboardCompanyPendingsComponent;
  let fixture: ComponentFixture<DashboardCompanyPendingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardCompanyPendingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardCompanyPendingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
