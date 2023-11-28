import {Component} from "@angular/core";
import {LookupPost} from "../../models/lookup/lookupPost.model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {SearchService} from "../../services/search.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: "app-search-posts",
  templateUrl: "./search-posts.component.html"
})
export class SearchPostsComponent {
  isLoading: boolean = true;
  isError: boolean = false;

  posts: LookupPost[] = [];
  searchForm: FormGroup;

  query: string = "";

  constructor(
    private searchService: SearchService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder
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
      this.searchService.searchPosts(this.query).subscribe({
        next: (results) => {
          this.posts = results;
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
}
