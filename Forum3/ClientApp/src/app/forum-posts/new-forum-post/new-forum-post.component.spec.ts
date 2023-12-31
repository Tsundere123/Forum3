import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewForumPostComponent } from './new-forum-post.component';

describe('NewForumPostComponent', () => {
  let component: NewForumPostComponent;
  let fixture: ComponentFixture<NewForumPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewForumPostComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewForumPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
