import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyMessagesComponent } from './company-messages.component';

describe('CompanyMessagesComponent', () => {
  let component: CompanyMessagesComponent;
  let fixture: ComponentFixture<CompanyMessagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyMessagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyMessagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
