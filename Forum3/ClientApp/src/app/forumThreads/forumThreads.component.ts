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

@Component({
  selector: 'app-forumThreads-component',
  templateUrl: './forumThreads.component.html',
  styleUrls: ['./forumThreads.component.css']
})

export class ForumThreadsComponent implements OnInit{
  threadsInCategory: ForumThread[] = [];
  categoryDetails: ForumCategoryDetailsModel;
  categoryId:number = 0;
  constructor(private forumThreadsServices: ForumThreadsService, private route:ActivatedRoute) { }
  ngOnInit(): void {
    this.route.params.subscribe(params => this.categoryId = +params['id']);

    this.forumThreadsServices.ForumThreadsOfCategory(this.categoryId).subscribe({
      next:(threads) => {
        console.log(threads);
        this.threadsInCategory = threads;
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
