import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchThreadsComponent } from "./search-threads.component";

describe('SearchThreadsComponent', () => {
  let component: SearchThreadsComponent;
  let fixture: ComponentFixture<SearchThreadsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchThreadsComponent ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(SearchThreadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
