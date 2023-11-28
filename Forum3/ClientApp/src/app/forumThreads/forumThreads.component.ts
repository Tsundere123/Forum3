import { Component, OnInit } from "@angular/core";
import { ForumThreadsService } from "../services/forumThreads.service";
import { ActivatedRoute } from "@angular/router";
import { ForumThread } from "../models/forumThread/forumThread.model";
import { ForumCategoryDetailsModel } from "../models/forumCategoryDetails.model";
import { AuthorizeService } from "../../api-authorization/authorize.service";
import { Observable } from "rxjs";

@Component({
  selector: 'app-forumThreads-component',
  templateUrl: './forumThreads.component.html'
})

export class ForumThreadsComponent implements OnInit{
  isLoading: boolean = true;
  isError: boolean = false;

  isAuthenticated?: Observable<boolean>;

  pinnedThreadsInCategory: ForumThread[] = [];
  unpinnedThreadsInCategory: ForumThread[] = [];

  categoryId: number = 0;
  categoryDetails: ForumCategoryDetailsModel;

  constructor(
    private authorizeService: AuthorizeService,
    private forumThreadsServices: ForumThreadsService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.route.params.subscribe(params => this.categoryId = +params['id']);

    this.forumThreadsServices.ForumThreadsOfCategory(this.categoryId).subscribe({
      next:(threads) => {
        this.pinnedThreadsInCategory = threads.filter(thread => thread.isPinned);
        this.unpinnedThreadsInCategory = threads.filter(thread => !thread.isPinned);

        this.isLoading = false;
      },
      error:(response) =>{
        this.isError = true;
        this.isLoading = false;
        console.log(response);
      }
    });

    this.forumThreadsServices.GetCategoryDetails(this.categoryId).subscribe({
      next:(category) =>{
        this.categoryDetails = category;
      },
      error:(response) =>{
        this.isError = true;
        console.log(response);
      }
    });
  }
}
