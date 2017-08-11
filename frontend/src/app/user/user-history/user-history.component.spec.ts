import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserHistoryComponent } from './user-history.component';

describe('UserHistoryComponent', () => {
  let component: UserHistoryComponent;
  let fixture: ComponentFixture<UserHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
