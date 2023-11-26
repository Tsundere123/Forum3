import {Component, Input} from '@angular/core';
import {ForumThreadViewModel} from "../../models/forumThreadView.model";
import {ForumThread} from "../../models/forumThread/forumThread.model";
import {ForumPost} from "../../models/forumPost.model";
import {ForumPostsService} from "../../services/forumPosts.service";

@Component({
  selector: 'app-forum-thread-list-item',
  templateUrl: './forum-thread-list-item.component.html',
  styleUrls: ['./forum-thread-list-item.component.css']
})

export class ForumThreadListItemComponent {
  @Input() viewModel: ForumThreadViewModel;
  @Input() currentThread: ForumThread;
  latestPost: ForumPost;
  numberOfPosts: number;

  constructor(private forumPostsServices: ForumPostsService) {  }

  ngOnInit(): void {
    this.forumPostsServices.getAllPostsOfThread(this.currentThread.id).subscribe({
      next:(posts) => {
        console.log(posts);
        let latestPost = null;
        posts.forEach(function(post){
          latestPost = post;
          if(post.createdAt > latestPost.createdAt){
            latestPost = post;
          }
        });
        this.latestPost = latestPost;
        this.numberOfPosts = posts.length;
        if(!posts.length) this.numberOfPosts = 0;
      },
      error:(response) =>{
        console.log(response);
      }
    })
  }
}
