import { Component } from "@angular/core";
import { LookupThread } from "../../models/lookup/lookup-thread.model";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { SearchService } from "../../services/search.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-search-threads",
  templateUrl: "./search-threads.component.html"
})
export class SearchThreadsComponent {
  isLoading: boolean = true;
  isError: boolean = false;

  threads: LookupThread[] = [];
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
      this.searchService.searchThreads(this.query).subscribe({
        next: (results) => {
          this.threads = results;
          this.isLoading = false;
        },
        error: (response) => {
          console.log(response);
          this.isError = true;
          this.isLoading = false;
        }
      });
    }
  }
}
