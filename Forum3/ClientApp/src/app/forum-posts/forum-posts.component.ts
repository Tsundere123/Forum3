import {Component, OnInit} from '@angular/core';
import {ForumPost} from "../models/forum-post.model";
import {ForumPostsService} from "../services/forumPosts.service";
import {ActivatedRoute} from "@angular/router";
import {Observable} from "rxjs";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {ForumThread} from "../models/forum-thread/forum-thread.model";
import {ForumThreadsService} from "../services/forumThreads.service";
import {ForumThreadDetailsModel} from "../models/forum-thread-details.model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-forum-posts',
  templateUrl: './forum-posts.component.html',
})
export class ForumPostsComponent implements OnInit{
  isLoading: boolean = true;
  isError: boolean = false;

  editThreadForm: FormGroup;

  userName?: string;
  isAuthenticated?: Observable<boolean>;

  display: boolean = true;

  postsInThread: ForumPost[] = [];
  threadId:number;
  displayDelete:boolean;
  threadDetails: ForumThreadDetailsModel;

  constructor(
    private formBuilder: FormBuilder,
    private forumThreadsServices: ForumThreadsService,
    private forumPostsServices: ForumPostsService,
    private activatedRoute: ActivatedRoute,
    private authorizeService: AuthorizeService
  ) {
    this.editThreadForm = formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(120)]],
      userName: ''
    });
  }
  ngOnInit(): void {

    this.displayDelete = false;

    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.activatedRoute.params.subscribe(params => this.threadId = +params['id']);
    this.authorizeService.getUser().subscribe(user => this.userName = user.name)
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
  editCurrentThread(){
    this.editThreadForm.patchValue({userName: this.userName});
    this.forumThreadsServices.EditCurrentThread(this.threadId, this.editThreadForm.value).subscribe(
      () => location.reload(),
      error => console.error(error)
    );
  }
  toggleEdit(){
    if(this.display) this.display = false;
    else this.display = true;
    this.editThreadForm.patchValue({ title: this.threadDetails.title })
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
