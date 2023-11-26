import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumCategoryListItemNewestThreadComponent } from './forum-category-list-item-newest-thread.component';

describe('ForumCategoryListItemNewestThreadComponent', () => {
  let component: ForumCategoryListItemNewestThreadComponent;
  let fixture: ComponentFixture<ForumCategoryListItemNewestThreadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForumCategoryListItemNewestThreadComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForumCategoryListItemNewestThreadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
