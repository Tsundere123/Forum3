import { Component, Input } from '@angular/core';
import { ForumThread } from "../../models/forum-thread/forum-thread.model";

@Component({
  selector: 'app-forum-thread-list-item',
  templateUrl: './forum-thread-list-item.component.html'
})
export class ForumThreadListItemComponent {
  @Input() currentThread: ForumThread;
}
