import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MiniChatComponent } from './mini-chat.component';

describe('MiniChatComponent', () => {
  let component: MiniChatComponent;
  let fixture: ComponentFixture<MiniChatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MiniChatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MiniChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
