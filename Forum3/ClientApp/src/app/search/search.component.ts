import {Component, OnInit} from "@angular/core";
import {Search} from "../models/search.model";
import {SearchService} from "../services/search.service";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {LookupThread} from "../models/lookup/lookupThread.model";
import {LookupPost} from "../models/lookup/lookupPost.model";
import {LookupMember} from "../models/lookup/lookupMember.model";

@Component({
    selector: "app-search",
    templateUrl: "./search.component.html"
  })
export class SearchComponent implements OnInit {
  isLoading: boolean = true;
  isError: boolean = false;

  threads: LookupThread[] = [];
  posts: LookupPost[] = [];
  members: LookupMember[] = [];
  searchForm: FormGroup;

  query: string = "";

  constructor(
    private searchService: SearchService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.searchForm = formBuilder.group({
      query: ['', Validators.required]
    });
  }

  onSubmit() {
    this.query = this.searchForm.value.query;
    this.fetch();
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => this.query = params['query']);

    if (this.query != "") {
      this.searchForm.patchValue({
        query: this.query
      });
    }

    this.fetch();
  }

  fetch(): void {
    if (this.query != "") {
      this.searchService.searchIndex(this.query).subscribe({
        next: (results) => {
          this.threads = results.threads;
          this.posts = results.posts;
          this.members = results.members;

          this.isLoading = false;
        },
        error: (response) => {
          console.log(response);
          this.isError = true;
          this.isLoading = false;
        }
      })
    }
  }

  goToThreadSearch(): void {
    this.router.navigate(['/search/threads', this.query]);
  }

  goToPostSearch(): void {
    this.router.navigate(['/search/posts', this.query]);
  }

  goToMemberSearch(): void {
    this.router.navigate(['/search/members', this.query]);
  }
}
