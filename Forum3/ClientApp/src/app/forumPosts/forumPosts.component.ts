import {Component, OnInit} from '@angular/core';
import {ForumPost} from "../models/forumPost.model";
import {ForumPostsService} from "../services/forumPosts.service";
import {ActivatedRoute} from "@angular/router";
import {Observable} from "rxjs";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {ForumThread} from "../models/forumThread/forumThread.model";
import {ForumThreadsService} from "../services/forumThreads.service";
import {ForumThreadDetailsModel} from "../models/forumThreadDetails.model";

@Component({
  selector: 'app-forum-posts',
  templateUrl: './forumPosts.component.html',
  styleUrls: ['./forumPosts.component.css']
})
export class ForumPostsComponent implements OnInit{
  isLoading: boolean = true;
  isError: boolean = false;

  isAuthenticated?: Observable<boolean>;
  postsInThread: ForumPost[] = [];
  threadId:number;
  displayDelete:boolean;
  threadDetails: ForumThreadDetailsModel;

  constructor(
    private forumThreadsServices: ForumThreadsService,
    private forumPostsServices: ForumPostsService,
    private activatedRoute: ActivatedRoute,
    private authorizeService: AuthorizeService
  ) {  }
  ngOnInit(): void {

    this.displayDelete = false;

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
    });

    this.forumThreadsServices.GetThreadDetails(this.threadId).subscribe({
      next:(thread) =>{
        this.threadDetails = thread;
      },
      error:(response) =>{
        this.isError = true;
        console.log(response);
      }
    });
  }
  deleteToggle(){
    if(this.displayDelete == true) this.displayDelete = false;
    else this.displayDelete = true;
  }
  permaDeleteCurrentThread(){
    this.forumThreadsServices.PermaDeleteCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }
  softDeleteCurrentThread(){
    this.forumThreadsServices.SoftDeleteCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }
  unSoftDeleteCurrentThread(){
    this.forumThreadsServices.UnSoftDeleteCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }

  pinCurrentThread(){
    this.forumThreadsServices.pinCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }
  unpinCurrentThread(){
    this.forumThreadsServices.unpinCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }

  lockCurrentThread(){
    this.forumThreadsServices.lockCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }
  unlockCurrentThread(){
    this.forumThreadsServices.unlockCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }

}
