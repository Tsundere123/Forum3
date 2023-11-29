import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumThreadListNormalIconsComponent } from './forum-thread-list-normal-icons.component';

describe('ForumThreadListNormalIconsComponent', () => {
  let component: ForumThreadListNormalIconsComponent;
  let fixture: ComponentFixture<ForumThreadListNormalIconsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForumThreadListNormalIconsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForumThreadListNormalIconsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
