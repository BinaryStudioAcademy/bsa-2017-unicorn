import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardCompanyProgressComponent } from './dashboard-company-progress.component';

describe('DashboardCompanyProgressComponent', () => {
  let component: DashboardCompanyProgressComponent;
  let fixture: ComponentFixture<DashboardCompanyProgressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardCompanyProgressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardCompanyProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
