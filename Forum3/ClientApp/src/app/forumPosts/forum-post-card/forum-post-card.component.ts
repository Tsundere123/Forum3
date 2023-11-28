import {Component, OnInit} from '@angular/core';
import { Input } from "@angular/core";
import { ForumPost } from "../../models/forumPost.model";
import {ForumPostsComponent} from "../forumPosts.component";
import {ForumPostsService} from "../../services/forumPosts.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthorizeService} from "../../../api-authorization/authorize.service";

@Component({
  selector: 'app-forum-post-card',
  templateUrl: './forum-post-card.component.html',
  styleUrls: ['./forum-post-card.component.css']
})

export class ForumPostCardComponent implements OnInit{
  @Input() currentPost: ForumPost;
  editPostForm: FormGroup;
  newContent: string;
  userName?: string;
  postId: number;
  display:boolean;
  oldContent: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private authorizeService: AuthorizeService,
    private forumPostsService:ForumPostsService,)
  {
    this.editPostForm = formBuilder.group({
      content: ['', Validators.required],
      userName: ''
    });
  }

  ngOnInit():void{
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
  editCurrentPost(){
    this.editPostForm.patchValue({ userName: this.userName })
    this.forumPostsService.EditCurrentPost(this.currentPost.id, this.editPostForm.value).subscribe(
      () => location.reload(),
      error => console.error(error)
    );
  }
}
