import { Component } from '@angular/core';
import { Input } from "@angular/core";
import { ForumPost } from "../../models/forumPost.model";
import {ForumPostsComponent} from "../forumPosts.component";

@Component({
  selector: 'app-forum-post-card',
  templateUrl: './forum-post-card.component.html',
  styleUrls: ['./forum-post-card.component.css']
})

export class ForumPostCardComponent {
  @Input() currentPost: ForumPost;
}
