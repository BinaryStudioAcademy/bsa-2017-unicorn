import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardProgressComponent } from './dashboard-progress.component';

describe('DashboardProgressComponent', () => {
  let component: DashboardProgressComponent;
  let fixture: ComponentFixture<DashboardProgressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardProgressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
