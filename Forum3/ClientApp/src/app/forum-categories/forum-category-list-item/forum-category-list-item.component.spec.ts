import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumCategoryListItemComponent } from './forum-category-list-item.component';

describe('ForumCategoryListItemComponent', () => {
  let component: ForumCategoryListItemComponent;
  let fixture: ComponentFixture<ForumCategoryListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForumCategoryListItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForumCategoryListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
