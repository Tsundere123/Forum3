import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingContentComponent } from "./loading-content.component";

describe('LoadingContentComponent', () => {
  let component: LoadingContentComponent;
  let fixture: ComponentFixture<LoadingContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadingContentComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(LoadingContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
