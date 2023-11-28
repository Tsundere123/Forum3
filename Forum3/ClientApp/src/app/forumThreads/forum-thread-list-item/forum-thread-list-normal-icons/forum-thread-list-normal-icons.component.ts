import {Component, Input} from '@angular/core';
import {ForumThread} from "../../../models/forumThread/forumThread.model";

@Component({
  selector: 'app-forum-thread-list-normal-icons',
  templateUrl: './forum-thread-list-normal-icons.component.html'
})

export class ForumThreadListNormalIconsComponent {
  @Input() currentThread: ForumThread;
}
