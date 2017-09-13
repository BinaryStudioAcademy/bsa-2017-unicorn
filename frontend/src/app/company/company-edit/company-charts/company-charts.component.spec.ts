import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyChartsComponent } from './company-charts.component';

describe('CompanyChartsComponent', () => {
  let component: CompanyChartsComponent;
  let fixture: ComponentFixture<CompanyChartsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyChartsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyChartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
