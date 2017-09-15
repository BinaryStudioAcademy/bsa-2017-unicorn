import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BanListComponent } from './ban-list.component';

describe('BanListComponent', () => {
  let component: BanListComponent;
  let fixture: ComponentFixture<BanListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BanListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BanListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
