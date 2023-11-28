import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupThreadComponent } from "./lookup-thread.component";

describe('LookupThreadComponent', () => {
  let component: LookupThreadComponent;
  let fixture: ComponentFixture<LookupThreadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LookupThreadComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(LookupThreadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
