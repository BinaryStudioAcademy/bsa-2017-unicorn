import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyWorksComponent } from './company-works.component';

describe('CompanyCategoriesComponent', () => {
  let component: CompanyWorksComponent;
  let fixture: ComponentFixture<CompanyWorksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyWorksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyWorksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
