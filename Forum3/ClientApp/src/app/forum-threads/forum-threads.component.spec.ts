import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumThreadsComponent } from './forum-threads.component';

describe('ThreadssComponent', () => {
  let component: ForumThreadsComponent;
  let fixture: ComponentFixture<ForumThreadsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForumThreadsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForumThreadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
