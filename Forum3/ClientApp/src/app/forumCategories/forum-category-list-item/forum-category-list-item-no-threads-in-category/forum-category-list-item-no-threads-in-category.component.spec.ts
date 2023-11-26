import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumCategoryListItemNoThreadsInCategoryComponent } from './forum-category-list-item-no-threads-in-category.component';

describe('ForumCategoryListItemNoThreadsInCategoryComponent', () => {
  let component: ForumCategoryListItemNoThreadsInCategoryComponent;
  let fixture: ComponentFixture<ForumCategoryListItemNoThreadsInCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForumCategoryListItemNoThreadsInCategoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForumCategoryListItemNoThreadsInCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
