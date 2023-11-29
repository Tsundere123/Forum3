import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumThreadListNewestSoftDeletedComponent } from './forum-thread-list-newest-soft-deleted.component';

describe('ForumThreadListNewestSoftDeletedComponent', () => {
  let component: ForumThreadListNewestSoftDeletedComponent;
  let fixture: ComponentFixture<ForumThreadListNewestSoftDeletedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForumThreadListNewestSoftDeletedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForumThreadListNewestSoftDeletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
