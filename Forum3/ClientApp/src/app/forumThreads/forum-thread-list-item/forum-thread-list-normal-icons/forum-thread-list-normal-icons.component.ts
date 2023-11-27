import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ForumThread} from "../../../models/forumThread/forumThread.model";

@Component({
  selector: 'app-forum-thread-list-normal-icons',
  templateUrl: './forum-thread-list-normal-icons.component.html',
  styleUrl: './forum-thread-list-normal-icons.component.css'
})
export class ForumThreadListNormalIconsComponent {
  @Input() currentThread: ForumThread;
}
