import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SignBlockComponent } from './sign-block.component';

describe('SignBlockComponent', () => {
  let component: SignBlockComponent;
  let fixture: ComponentFixture<SignBlockComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SignBlockComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
