import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupPostComponent } from "./lookup-post.component";

describe('LookupPostComponent', () => {
  let component: LookupPostComponent;
  let fixture: ComponentFixture<LookupPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LookupPostComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(LookupPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
