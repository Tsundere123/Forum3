import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumCategoryListItemThreadHasNoPostsComponent } from './forum-category-list-item-thread-has-no-posts.component';

describe('ForumCategoryListItemThreadHasNoPostsComponent', () => {
  let component: ForumCategoryListItemThreadHasNoPostsComponent;
  let fixture: ComponentFixture<ForumCategoryListItemThreadHasNoPostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForumCategoryListItemThreadHasNoPostsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForumCategoryListItemThreadHasNoPostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
