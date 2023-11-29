import { Component, OnInit } from '@angular/core';
import { Input } from "@angular/core";
import { ForumPost } from "../../models/forum-post.model";
import { ForumPostsService } from "../../services/forumPosts.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { AuthorizeService } from "../../../api-authorization/authorize.service";
import { Observable } from "rxjs";

@Component({
  selector: 'app-forum-post-card',
  templateUrl: './forum-post-card.component.html'
})
export class ForumPostCardComponent implements OnInit{
  @Input() currentPost: ForumPost;
  editPostForm: FormGroup;
  newContent: string;
  userName?: string;
  postId: number;
  display:boolean;
  displayDelete:boolean;
  oldContent: string;

  isAuthenticated?: Observable<boolean>;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private authorizeService: AuthorizeService,
    private forumPostsService:ForumPostsService,)
  {
    this.editPostForm = formBuilder.group({
      content: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(5000)]],
      userName: ''
    });
  }

  get content() { return this.editPostForm.get('content'); }

  ngOnInit():void{
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.display = true;
    // get username
    this.authorizeService.getUser().subscribe(user => this.userName = user.name)
    this.route.params.subscribe(params => this.postId = +params['id']);
  }
  onClickEdit(){
    // Toggles display of items
    if(this.display == true)this.display = false;
    else this.display = true;
    this.editPostForm.patchValue({ content: this.currentPost.content })
  }
  deleteToggle(){
    if(this.displayDelete == true)this.displayDelete = false;
    else this.displayDelete = true;

  }
  editCurrentPost(){
    this.editPostForm.patchValue({ userName: this.userName });
    this.forumPostsService.EditCurrentPost(this.currentPost.id, this.editPostForm.value).subscribe(
      () => location.reload(),
      error => console.error(error)
    );
  }

  deleteCurrentPost(){
    this.forumPostsService.PermaDeleteCurrentPost(this.currentPost.id).subscribe(
      () => location.reload(),
      error => console.error(error)
    );
  }
  softDeleteCurrentPost(){
    this.forumPostsService.SoftDeleteCurrentPost(this.currentPost.id).subscribe(
      () => location.reload(),
      error => console.error(error)
    );
  }
  unDeletePost(){
    this.forumPostsService.UnSoftDeleteCurrentPost(this.currentPost.id).subscribe(
      () => location.reload(),
      error => console.error(error)
    )
  }
}