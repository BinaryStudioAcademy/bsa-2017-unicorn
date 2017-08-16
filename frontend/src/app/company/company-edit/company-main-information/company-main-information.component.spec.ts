import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyMainInformationComponent } from './company-main-information.component';

describe('CompanyMainInformationComponent', () => {
  let component: CompanyMainInformationComponent;
  let fixture: ComponentFixture<CompanyMainInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyMainInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyMainInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
