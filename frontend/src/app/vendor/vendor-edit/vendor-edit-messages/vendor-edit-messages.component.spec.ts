import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditMessagesComponent } from './vendor-edit-messages.component';

describe('VendorEditMessagesComponent', () => {
  let component: VendorEditMessagesComponent;
  let fixture: ComponentFixture<VendorEditMessagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditMessagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditMessagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
