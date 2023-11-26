import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {ForumCategoriesService} from "../../services/forumCategories.service";
import {ForumCategory} from "../../models/forumCategory.model";
import {ForumThreadsService} from "../../services/forumThreads.service";
import {ForumPostsService} from "../../services/forumPosts.service";
import {ForumThread} from "../../models/forumThread/forumThread.model";
import {
  ForumCategoryListItemNoThreadsInCategoryComponent
} from "./forum-category-list-item-no-threads-in-category/forum-category-list-item-no-threads-in-category.component";
import {
  ForumCategoryListItemThreadHasNoPostsComponent
} from "./forum-category-list-item-thread-has-no-posts/forum-category-list-item-thread-has-no-posts.component";
import {
  ForumCategoryListItemNewestThreadComponent
} from "./forum-category-list-item-newest-thread/forum-category-list-item-newest-thread.component";

@Component({
  selector: 'app-forum-category-list-item',
  standalone: true,
  imports: [CommonModule, RouterLink, ForumCategoryListItemNoThreadsInCategoryComponent, ForumCategoryListItemThreadHasNoPostsComponent, ForumCategoryListItemNewestThreadComponent],
  templateUrl: './forum-category-list-item.component.html',
  styleUrl: './forum-category-list-item.component.css'
})
export class ForumCategoryListItemComponent {
  @Input() currentCategory: ForumCategory;
  totalPosts: number;
  totalThreads: number;
  newestThread: ForumThread;
  constructor(private forumThreadsServices: ForumThreadsService,private forumPostsServices: ForumPostsService,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.forumThreadsServices.getData(this.currentCategory.id).subscribe({
      next:(data) =>{
        this.totalThreads = data.forumThreads.length;
        this.newestThread = data.newestThread;
      },
      error:(response) =>{
        console.log(response);
      }
    });
  }
}
