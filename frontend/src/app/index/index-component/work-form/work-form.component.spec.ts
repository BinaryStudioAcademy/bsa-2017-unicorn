import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkFormComponent } from './work-form.component';

describe('WorkFormComponent', () => {
  let component: WorkFormComponent;
  let fixture: ComponentFixture<WorkFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
