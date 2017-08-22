import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserMainReviewsComponent } from './user-main-reviews.component';

describe('UserMainReviewsComponent', () => {
  let component: UserMainReviewsComponent;
  let fixture: ComponentFixture<UserMainReviewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserMainReviewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserMainReviewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
