import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumThreadListNoPostsComponent } from './forum-thread-list-no-posts.component';

describe('ForumThreadListNoPostsComponent', () => {
  let component: ForumThreadListNoPostsComponent;
  let fixture: ComponentFixture<ForumThreadListNoPostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForumThreadListNoPostsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForumThreadListNoPostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
