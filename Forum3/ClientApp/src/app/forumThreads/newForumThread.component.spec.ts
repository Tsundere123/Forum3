import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewForumThreadComponent } from "./newForumThread.component";

describe('NewForumThreadComponent', () => {
  let component: NewForumThreadComponent;
  let fixture: ComponentFixture<NewForumThreadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewForumThreadComponent ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(NewForumThreadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
