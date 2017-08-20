import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserMainInfoComponent } from './user-main-info.component';

describe('UserMainInfoComponent', () => {
  let component: UserMainInfoComponent;
  let fixture: ComponentFixture<UserMainInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserMainInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserMainInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
