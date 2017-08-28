import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardFinishedComponent } from './dashboard-finished.component';

describe('DashboardFinishedComponent', () => {
  let component: DashboardFinishedComponent;
  let fixture: ComponentFixture<DashboardFinishedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardFinishedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardFinishedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
