import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupMemberComponent } from "./lookup-member.component";

describe('LookupMemberComponent', () => {
  let component: LookupMemberComponent;
  let fixture: ComponentFixture<LookupMemberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LookupMemberComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(LookupMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
