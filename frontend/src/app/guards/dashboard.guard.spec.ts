import { TestBed, async, inject } from '@angular/core/testing';

import { DashboardGuard } from './dashboard.guard';

describe('DashboardGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DashboardGuard]
    });
  });

  it('should ...', inject([DashboardGuard], (guard: DashboardGuard) => {
    expect(guard).toBeTruthy();
  }));
});
