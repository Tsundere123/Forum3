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
  ForumCategoryListItemNewestThreadComponent
} from "./forum-category-list-item-newest-thread/forum-category-list-item-newest-thread.component";

@Component({
  selector: 'app-forum-category-list-item',
  standalone: true,
  imports: [CommonModule, RouterLink, ForumCategoryListItemNoThreadsInCategoryComponent, ForumCategoryListItemNewestThreadComponent],
  templateUrl: './forum-category-list-item.component.html',
  styleUrl: './forum-category-list-item.component.css'
})
export class ForumCategoryListItemComponent {
  @Input() currentCategory: ForumCategory;
  totalPosts: number;
  totalThreads: number;
  newestThread: ForumThread;
  ngOnInit(): void {
  }
}
