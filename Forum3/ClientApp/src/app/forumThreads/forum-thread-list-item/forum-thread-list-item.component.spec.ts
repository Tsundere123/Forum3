import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumThreadListItemComponent } from './forum-thread-list-item.component';

describe('ForumThreadListItemComponent', () => {
  let component: ForumThreadListItemComponent;
  let fixture: ComponentFixture<ForumThreadListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForumThreadListItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForumThreadListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
