import { Component, Input } from '@angular/core';
import { ForumThread } from "../../models/forumThread/forumThread.model";

@Component({
  selector: 'app-forum-thread-list-item',
  templateUrl: './forum-thread-list-item.component.html'
})

export class ForumThreadListItemComponent {
  @Input() currentThread: ForumThread;
}
