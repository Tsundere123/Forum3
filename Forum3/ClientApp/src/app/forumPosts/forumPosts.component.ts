import {Component, OnInit} from '@angular/core';
import {ForumPost} from "../models/forumPost.model";
import {ForumPostsService} from "../services/forumPosts.service";
import {ActivatedRoute} from "@angular/router";
import {Observable} from "rxjs";
import {AuthorizeService} from "../../api-authorization/authorize.service";

@Component({
  selector: 'app-forum-posts',
  templateUrl: './forumPosts.component.html',
  styleUrls: ['./forumPosts.component.css']
})
export class ForumPostsComponent implements OnInit{
  public isAuthenticated?: Observable<boolean>;
  postsInThread: ForumPost[] = [];
  threadId:number = 0;

  constructor(private forumPostsServices: ForumPostsService, private activatedRoute: ActivatedRoute, private authorizeService: AuthorizeService) {  }
  ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.activatedRoute.params.subscribe(params => this.threadId = +params['id']);

    this.forumPostsServices.GetAllPostsOfThread(this.threadId).subscribe({
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
