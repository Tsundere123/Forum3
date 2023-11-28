import { Component, Input } from '@angular/core';
import { ForumThread } from "../../../models/forumThread/forumThread.model";

@Component({
  selector: 'app-forum-thread-list-newest-soft-deleted',
  templateUrl: './forum-thread-list-newest-soft-deleted.component.html'
})

export class ForumThreadListNewestSoftDeletedComponent {
  @Input() currentThread: ForumThread;
}
