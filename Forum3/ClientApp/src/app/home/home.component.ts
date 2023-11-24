import {AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {Observable} from "rxjs";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import KeenSlider, {KeenSliderInstance} from "keen-slider";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [
    "../../../node_modules/keen-slider/keen-slider.min.css",
    "./home.component.css"
  ]
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  threads: any[] = [
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
  ];

  posts: any[] = [
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
    {
      title: "Thread 1",
      description: "Thread 1 description"
    },
  ];

  members: any[] = [
    {
      username: "Ceno",
      avatar: "default.png"
    },
    {
      username: "Ceno",
      avatar: "default.png"
    },
    {
      username: "Ceno",
      avatar: "default.png"
    },
    {
      username: "Ceno",
      avatar: "default.png"
    },
    {
      username: "Ceno",
      avatar: "default.png"
    },
    {
      username: "Ceno",
      avatar: "default.png"
    },
  ]

  @ViewChild("threadsRef") threadsRef: ElementRef<HTMLElement>;
  @ViewChild("postsRef") postsRef: ElementRef<HTMLElement>;
  @ViewChild("membersRef") membersRef: ElementRef<HTMLElement>;

  public isAuthenticated?: Observable<boolean>;

  threadsCurrentSlide: number = 1;
  threadsSlider: KeenSliderInstance = null;

  postsCurrentSlide: number = 1;
  postsSlider: KeenSliderInstance = null;

  membersCurrentSlide: number = 1;
  membersSlider: KeenSliderInstance = null;

  constructor(private authorizeService: AuthorizeService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.threadsSlider = new KeenSlider(this.threadsRef.nativeElement, {
        initial: this.threadsCurrentSlide,
        slideChanged: (s) => {
          this.threadsCurrentSlide = s.track.details.rel
        },
        breakpoints: {
          "(min-width: 400px)": {
            slides: { perView: 2, spacing: 5 },
          },
          "(min-width: 1000px)": {
            slides: { perView: 3, spacing: 10 },
          },
        },
      });

      this.postsSlider = new KeenSlider(this.postsRef.nativeElement, {
        initial: this.postsCurrentSlide,
        slideChanged: (s) => {
          this.postsCurrentSlide = s.track.details.rel
        },
        breakpoints: {
          "(min-width: 400px)": {
            slides: { perView: 2, spacing: 5 },
          },
          "(min-width: 1000px)": {
            slides: { perView: 3, spacing: 10 },
          },
        },
      });

      this.membersSlider = new KeenSlider(this.membersRef.nativeElement, {
        initial: this.membersCurrentSlide,
        slideChanged: (s) => {
          this.membersCurrentSlide = s.track.details.rel
        },
        breakpoints: {
          "(min-width: 400px)": {
            slides: { perView: 2, spacing: 5 },
          },
          "(min-width: 1000px)": {
            slides: { perView: 3, spacing: 10 },
          },
        },
      });
    })
  }

  ngOnDestroy() {
    if (this.threadsSlider) this.threadsSlider.destroy();
    if (this.postsSlider) this.postsSlider.destroy();
    if (this.membersSlider) this.membersSlider.destroy();
  }
}
