import { Component, OnInit } from '@angular/core';
import { Input } from "@angular/core";
import { ForumPost } from "../../models/forum-post.model";
import { ForumPostsService } from "../../services/forum-posts.service";
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
    this.authorizeService.getUser().subscribe(user => user ? this.userName = user.name : null);
    this.route.params.subscribe(params => this.postId = +params['id']);
    this.display = true;
  }
  onClickEdit(){
    this.display = this.display != true;
    this.editPostForm.patchValue({ content: this.currentPost.content })
  }
  deleteToggle(){
    this.displayDelete = this.displayDelete != true;

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
