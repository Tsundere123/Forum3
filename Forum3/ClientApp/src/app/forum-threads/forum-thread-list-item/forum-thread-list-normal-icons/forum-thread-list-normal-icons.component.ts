import {Component, Input} from '@angular/core';
import {ForumThread} from "../../../models/forum-thread/forum-thread.model";

@Component({
  selector: 'app-forum-thread-list-normal-icons',
  templateUrl: './forum-thread-list-normal-icons.component.html'
})

export class ForumThreadListNormalIconsComponent {
  @Input() currentThread: ForumThread;
}
