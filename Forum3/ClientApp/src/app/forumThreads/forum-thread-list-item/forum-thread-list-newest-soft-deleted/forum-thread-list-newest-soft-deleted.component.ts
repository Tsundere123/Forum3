import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ForumThread} from "../../../models/forumThread/forumThread.model";

@Component({
  selector: 'app-forum-thread-list-newest-soft-deleted',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './forum-thread-list-newest-soft-deleted.component.html',
  styleUrl: './forum-thread-list-newest-soft-deleted.component.css'
})
export class ForumThreadListNewestSoftDeletedComponent {
  @Input() currentThread: ForumThread;
}
