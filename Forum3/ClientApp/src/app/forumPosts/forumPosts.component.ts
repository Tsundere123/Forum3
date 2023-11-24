import {Component, OnInit} from '@angular/core';
import {ForumPost} from "../models/forumPost.model";
import {ForumPostsService} from "../services/forumPosts.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-forum-posts',
  templateUrl: './forumPosts.component.html',
  styleUrls: ['./forumPosts.component.css']
})
export class ForumPostsComponent implements OnInit{
  postsInThread: ForumPost[] = [];
  threadId:number = 0;

  constructor(private forumPostsServices: ForumPostsService, private activatedRoute: ActivatedRoute) {  }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => this.threadId = +params['id']);

    this.forumPostsServices.getAllPostsOfThread(this.threadId).subscribe({
      next:(posts) => {
        console.log(posts);
        this.postsInThread = posts;
      },
      error:(response) =>{
        console.log(response);
      }
    })
  }
}
