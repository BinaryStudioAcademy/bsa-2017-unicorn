import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PopularTasksComponent } from './popular-tasks.component';

describe('PopularTasksComponent', () => {
  let component: PopularTasksComponent;
  let fixture: ComponentFixture<PopularTasksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PopularTasksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PopularTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
