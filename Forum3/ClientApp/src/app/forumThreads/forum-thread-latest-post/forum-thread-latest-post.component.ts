import {Component, Input} from '@angular/core';
import {ForumThread} from "../../models/forumThread/forumThread.model";
import {ForumPost} from "../../models/forumPost.model";
import {ForumPostsService} from "../../services/forumPosts.service";

@Component({
  selector: 'app-forum-thread-latest-post',
  templateUrl: './forum-thread-latest-post.component.html',
  styleUrls: ['./forum-thread-latest-post.component.css']
})
export class ForumThreadLatestPostComponent {
  latestPost: ForumPost;
  @Input() currentThread: ForumThread;
  constructor(private forumPostsServices: ForumPostsService) {  }
  ngOnInit(): void {
    this.forumPostsServices.getAllPostsOfThread(this.currentThread.id).subscribe({
      next:(posts) => {
        console.log(posts);
        let latestPost;
        posts.forEach(function(post){
          latestPost = post;
          if(post.createdAt > latestPost.createdAt){
            latestPost = post;
          }
        });
        this.latestPost = latestPost;
      },
      error:(response) =>{
        console.log(response);
      }
    })
  }
}
