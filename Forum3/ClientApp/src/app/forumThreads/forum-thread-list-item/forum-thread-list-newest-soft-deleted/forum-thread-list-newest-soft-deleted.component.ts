import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ForumThread} from "../../../models/forumThread/forumThread.model";
import {AppModule} from "../../../app.module";

@Component({
  selector: 'app-forum-thread-list-newest-soft-deleted',
  templateUrl: './forum-thread-list-newest-soft-deleted.component.html',
  styleUrl: './forum-thread-list-newest-soft-deleted.component.css'
})
export class ForumThreadListNewestSoftDeletedComponent {
  @Input() currentThread: ForumThread;
}
