import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthorizeService } from "../../../api-authorization/authorize.service";
import { ForumPostsService } from "../../services/forum-posts.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-new-forum-post',
  templateUrl: './new-forum-post.component.html'
})
export class NewForumPostComponent implements OnInit{
  newPostForm: FormGroup;
  threadId: number;
  userName?: string
  isAuthenticated?: Observable<boolean>;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private authorizeService: AuthorizeService,
    private postService: ForumPostsService
  ) {
    this.newPostForm = formBuilder.group({
      content: ['', Validators.required],
      userName: ''
    });
  }
  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    if(this.isAuthenticated){
      this.authorizeService.getUser().subscribe(user => this.userName = user.name)
    }
    this.route.params.subscribe(params => this.threadId = +params['id']);
  }

  onSubmit() {
    this.newPostForm.patchValue({ userName: this.userName })
    this.postService.CreatePost(this.threadId, this.newPostForm.value).subscribe(
      () => this.router.navigate(['/threads', this.threadId], { relativeTo: this.route }),
      error => console.error(error)
    );
  }

  onCancel() {
    this.router.navigate(['/threads', this.threadId], { relativeTo: this.route });
  }
}
