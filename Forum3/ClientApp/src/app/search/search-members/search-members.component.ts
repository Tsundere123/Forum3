import {Component} from "@angular/core";
import {LookupMember} from "../../models/lookup/lookup-member.model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {SearchService} from "../../services/search.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: "app-search-members",
  templateUrl: "./search-members.component.html"
})
export class SearchMembersComponent {
  isLoading: boolean = true;
  isError: boolean = false;

  members: LookupMember[] = [];
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
      this.searchService.searchMembers(this.query).subscribe({
        next: (results) => {
          this.members = results;
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
