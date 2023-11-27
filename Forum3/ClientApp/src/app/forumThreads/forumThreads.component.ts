import {Component, OnInit} from "@angular/core";
import {ForumCategoriesService} from "../services/forumCategories.service";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThreadsService} from "../services/forumThreads.service";
import {ForumThreadViewModel} from "../models/forumThreadView.model";
import {ActivatedRoute} from "@angular/router";
import {ForumCategoriesComponent} from "../forumCategories/forumCategories.component";
import {inject} from "@angular/core/testing";
import {ForumThread} from "../models/forumThread/forumThread.model";
import {ForumPost} from "../models/forumPost.model";
import {ForumPostsService} from "../services/forumPosts.service";
import {ForumCategoryDetailsModel} from "../models/forumCategoryDetails.model";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-forumThreads-component',
  templateUrl: './forumThreads.component.html',
  styleUrls: ['./forumThreads.component.css']
})

export class ForumThreadsComponent implements OnInit{
  public isAuthenticated?: Observable<boolean>;
  threadsInCategory: ForumThread[] = [];
  pinnedThreadsInCategory: ForumThread[] = [];
  unpinnedThreadsInCategory: ForumThread[] = [];
  categoryDetails: ForumCategoryDetailsModel;
  categoryId:number = 0;
  constructor(private forumThreadsServices: ForumThreadsService, private route:ActivatedRoute, private authorizeService: AuthorizeService) { }
  ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.route.params.subscribe(params => this.categoryId = +params['id']);

    this.forumThreadsServices.ForumThreadsOfCategory(this.categoryId).subscribe({
      next:(threads) => {
        let pinnedThreads: ForumThread[] = [];
        let unpinnedThreads: ForumThread[] = [];
        threads.forEach(function(thread){
          console.log("Testing thread...")
          console.log(thread)
          if(thread.isPinned){
            pinnedThreads.push(thread);
          }else{
            unpinnedThreads.push(thread);
          }
        });
        this.pinnedThreadsInCategory = pinnedThreads;
        this.unpinnedThreadsInCategory = unpinnedThreads;
        console.log("All threads")
        console.log(threads);
        console.log("Pinned threads")
        console.log(pinnedThreads);
        console.log("Unpinned threads")
        console.log(unpinnedThreads);
      },
      error:(response) =>{
        console.log(response);
      }
    });

    this.forumThreadsServices.GetCategoryDetails(this.categoryId).subscribe({
      next:(category) =>{
        console.log(category);
        this.categoryDetails = category;
      }
    })
  }
}
