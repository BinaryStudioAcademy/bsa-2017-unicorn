import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardPendingsComponent } from './dashboard-pendings.component';

describe('DashboardPendingsComponent', () => {
  let component: DashboardPendingsComponent;
  let fixture: ComponentFixture<DashboardPendingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardPendingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardPendingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
