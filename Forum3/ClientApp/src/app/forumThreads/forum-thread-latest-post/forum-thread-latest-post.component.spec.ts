import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumThreadLatestPostComponent } from './forum-thread-latest-post.component';

describe('ForumThreadLatestPostComponent', () => {
  let component: ForumThreadLatestPostComponent;
  let fixture: ComponentFixture<ForumThreadLatestPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForumThreadLatestPostComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForumThreadLatestPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
