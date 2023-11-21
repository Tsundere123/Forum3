import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThreadssComponent } from './threadss.component';

describe('ThreadssComponent', () => {
  let component: ThreadssComponent;
  let fixture: ComponentFixture<ThreadssComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThreadssComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ThreadssComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
