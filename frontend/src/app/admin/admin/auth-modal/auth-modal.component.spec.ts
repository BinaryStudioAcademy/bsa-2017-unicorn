import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthModalComponent } from './auth-modal.component';

describe('AuthModalComponent', () => {
  let component: AuthModalComponent;
  let fixture: ComponentFixture<AuthModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
