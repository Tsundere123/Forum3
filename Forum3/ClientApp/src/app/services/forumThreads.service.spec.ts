import { TestBed } from '@angular/core/testing';

import { ForumThreadsService } from './forumThreads.service';

describe('ThreadsService', () => {
  let service: ForumThreadsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForumThreadsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
