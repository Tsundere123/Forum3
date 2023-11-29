import { Component, Input } from '@angular/core';
import { ForumThread } from "../../../models/forum-thread/forum-thread.model";

@Component({
  selector: 'app-forum-thread-list-newest-soft-deleted',
  templateUrl: './forum-thread-list-newest-soft-deleted.component.html'
})
export class ForumThreadListNewestSoftDeletedComponent {
  @Input() currentThread: ForumThread;
}
