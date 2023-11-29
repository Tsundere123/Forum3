import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthorizeService } from "../../../api-authorization/authorize.service";
import { ForumThreadsService } from "../../services/forum-threads.service";

@Component({
  selector: 'app-new-forum-thread-component',
  templateUrl: './new-forum-thread.component.html'
})
export class NewForumThreadComponent implements OnInit{
  newThreadForm: FormGroup;
  categoryId: number = 0;
  userName?: string

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private authorizeService: AuthorizeService,
    private threadService: ForumThreadsService
  ) {
    this.newThreadForm = formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(120)]],
      content: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(5000)]],
      userName: ''
    });
  }

  get title() { return this.newThreadForm.get('title'); }
  get content() { return this.newThreadForm.get('content'); }

  ngOnInit() {
    this.authorizeService.getUser().subscribe(user => this.userName = user.name)
    this.route.params.subscribe(params => this.categoryId = +params['id']);
  }

  onSubmit() {
    this.newThreadForm.patchValue({ userName: this.userName })
    this.threadService.CreateThread(this.categoryId, this.newThreadForm.value).subscribe(
      () => this.router.navigate(['/categories', this.categoryId], { relativeTo: this.route }),
      error => console.error(error)
    );
  }

  onCancel() {
    this.router.navigate(['/categories', this.categoryId], { relativeTo: this.route });
  }
}
