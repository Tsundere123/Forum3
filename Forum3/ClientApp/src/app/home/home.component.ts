import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild
} from '@angular/core';
import { Observable } from "rxjs";
import { AuthorizeService } from "../../api-authorization/authorize.service";
import KeenSlider, { KeenSliderInstance } from "keen-slider";
import { HomeService } from "../services/home.service";
import { LookupMember } from "../models/lookup/lookup-member.model";
import { LookupThread } from "../models/lookup/lookup-thread.model";
import { LookupPost } from "../models/lookup/lookup-post.model";

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

  isLoading: boolean = true;
  isError: boolean = false;

  @ViewChild("threadsRef") threadsRef: ElementRef<HTMLElement>;
  @ViewChild("postsRef") postsRef: ElementRef<HTMLElement>;
  @ViewChild("membersRef") membersRef: ElementRef<HTMLElement>;

  public isAuthenticated?: Observable<boolean>;

  threadsSlider: KeenSliderInstance = null;
  postsSlider: KeenSliderInstance = null;
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

        this.isLoading = false;
      },
      error:(response) =>{
        console.log(response);
        this.isError = true;
        this.isLoading = false;
      }
    })
  }

  ngAfterViewInit() {
    this.threadsSlider = new KeenSlider(this.threadsRef.nativeElement, {
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
      breakpoints: {
        "(min-width: 400px)": {
          slides: { perView: 2, spacing: 5 },
        },
        "(min-width: 1000px)": {
          slides: { perView: 3, spacing: 10 },
        },
      },
    });
  }

  ngOnDestroy() {
    if (this.threadsSlider) this.threadsSlider.destroy();
    if (this.postsSlider) this.postsSlider.destroy();
    if (this.membersSlider) this.membersSlider.destroy();
  }
}
