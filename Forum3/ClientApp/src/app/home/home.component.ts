import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild
} from '@angular/core';
import {Observable} from "rxjs";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import KeenSlider, {KeenSliderInstance} from "keen-slider";
import {HomeService} from "../services/home.service";
import {LookupMember} from "../models/lookup/lookupMember.model";
import {LookupThread} from "../models/lookup/lookupThread.model";
import {LookupPost} from "../models/lookup/lookupPost.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [
    "../../../node_modules/keen-slider/keen-slider.min.css",
    "./home.component.css"
  ]
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  threads: LookupThread[] = [];
  posts: LookupPost[] = [];
  members: LookupMember[] = [];

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
  constructor(private authorizeService: AuthorizeService, private homeService: HomeService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();

    this.homeService.getData().subscribe({
      next:(data) => {
        this.threads = data.threads;
        this.posts = data.posts;
        this.members = data.members;

        if (this.threadsSlider || this.postsSlider || this.membersSlider) {
          setTimeout(() => {
            this.threadsSlider?.update(undefined, 0);
            this.postsSlider?.update(undefined, 0);
            this.membersSlider?.update(undefined, 0);
          }, 1);
        }
      },
      error:(response) =>{
        console.log(response);
      }
    })
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
