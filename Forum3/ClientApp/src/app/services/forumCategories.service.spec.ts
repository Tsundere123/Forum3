import { TestBed } from '@angular/core/testing';

import { ForumCategoriesService } from './forumCategories.service';

describe('CategoriesService', () => {
  let service: ForumCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForumCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
