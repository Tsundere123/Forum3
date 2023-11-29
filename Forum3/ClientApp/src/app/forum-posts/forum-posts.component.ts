import { Component, OnInit } from '@angular/core';
import { ForumPost } from "../models/forum-post.model";
import { ForumPostsService } from "../services/forum-posts.service";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { AuthorizeService } from "../../api-authorization/authorize.service";
import { ForumThreadsService } from "../services/forum-threads.service";
import { ForumThreadDetailsModel } from "../models/forum-thread-details.model";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

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
  threadId: number;
  displayDelete: boolean = false;
  threadDetails: ForumThreadDetailsModel;

  constructor(
    private formBuilder: FormBuilder,
    private forumThreadsServices: ForumThreadsService,
    private forumPostsServices: ForumPostsService,
    private activatedRoute: ActivatedRoute,
    private authorizeService: AuthorizeService,
    private router: Router
  ) {
    this.editThreadForm = formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(120)]],
      userName: ''
    });
  }

  get title() { return this.editThreadForm.get('title'); }
  ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.activatedRoute.params.subscribe(params => this.threadId = +params['id']);
    this.authorizeService.getUser().subscribe(user => user ? this.userName = user.name : null);

    this.forumPostsServices.GetAllPostsOfThread(this.threadId).subscribe({
      next:(posts) => {
        this.postsInThread = posts;
      },
      error:(response) =>{
        console.log(response);
        this.isError = true;
        this.isLoading = false;
      }
    });

    this.forumThreadsServices.GetThreadDetails(this.threadId).subscribe({
      next:(thread) =>{
        this.threadDetails = thread;
        this.isLoading = false;
      },
      error:(response) =>{
        this.isError = true;
        this.isLoading = false;
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
    this.display = !this.display;
    this.editThreadForm.patchValue({ title: this.threadDetails.title })
  }
  deleteToggle(){
    this.displayDelete = this.displayDelete != true;
  }
  permaDeleteCurrentThread(){
    this.forumThreadsServices.PermaDeleteCurrentThread(this.threadId).subscribe(
      () => this.router.navigate(['/']),
      error => {
        console.error(error);
        this.isError = true;
      }
    )
  }
  softDeleteCurrentThread(){
    this.forumThreadsServices.SoftDeleteCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => {
        console.error(error);
        this.isError = true;
      }
    )
  }
  unSoftDeleteCurrentThread(){
    this.forumThreadsServices.UnSoftDeleteCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => {
        console.error(error);
        this.isError = true;
      }
    )
  }

  pinCurrentThread(){
    this.forumThreadsServices.pinCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => {
        console.error(error);
        this.isError = true;
      }
    )
  }
  unpinCurrentThread(){
    this.forumThreadsServices.unpinCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => {
        console.error(error);
        this.isError = true;
      }
    )
  }

  lockCurrentThread(){
    this.forumThreadsServices.lockCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => {
        console.error(error);
        this.isError = true;
      }
    )
  }
  unlockCurrentThread(){
    this.forumThreadsServices.unlockCurrentThread(this.threadId).subscribe(
      () => location.reload(),
      error => {
        console.error(error);
        this.isError = true;
      }
    )
  }
}
