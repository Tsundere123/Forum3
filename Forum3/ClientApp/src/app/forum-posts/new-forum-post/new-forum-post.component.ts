import {Component, Input, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuthorizeService } from "../../../api-authorization/authorize.service";
import { ForumPostsService } from "../../services/forum-posts.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-new-forum-post',
  templateUrl: './new-forum-post.component.html'
})
export class NewForumPostComponent implements OnInit{
  @Input() threadId: number;
  newPostForm: FormGroup;
  userName?: string
  isAuthenticated?: Observable<boolean>;
  isError: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private authorizeService: AuthorizeService,
    private postService: ForumPostsService
  ) {
    this.newPostForm = formBuilder.group({
      content: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(5000)]],
      userName: ''
    });
  }

  get content() { return this.newPostForm.get('content'); }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    if(this.isAuthenticated){
      this.authorizeService.getUser().subscribe(user => this.userName = user.name)
    }
  }

  onSubmit() {
    this.newPostForm.patchValue({ userName: this.userName })
    this.postService.CreatePost(this.threadId, this.newPostForm.value).subscribe(
      () => location.reload(),
      error => {
        console.error(error);
        this.isError = true;
      }
    );
  }
}
