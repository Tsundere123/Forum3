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

@Component({
  selector: 'app-forumThreads-component',
  templateUrl: './forumThreads.component.html',
  styleUrls: ['./forumThreads.component.css']
})

export class ForumThreadsComponent implements OnInit{
  threadsInCategory: ForumThread[] = [];
  currentCategory: ForumCategory;
  categoryId:number = 0;
  pinnedThreads: ForumThread[];
  currentPage: number;
  totalPages: number;
  constructor(private forumThreadsServices: ForumThreadsService, private activatedRoute:ActivatedRoute) { }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => this.categoryId = +params['id']);

    this.forumThreadsServices.getData(this.categoryId).subscribe({
      next:(data) => {
        this.currentCategory = data.forumCategory;
        this.pinnedThreads = data.pinnedThreads;
        this.threadsInCategory = data.forumThreads;
        this.currentPage = data.currentPage;
        this.totalPages = data.totalPages;
        console.log(data);
      },
      error:(response) =>{
        console.log(response);
      }
    });
  }
}
