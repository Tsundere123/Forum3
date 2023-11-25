import {Component, Input} from '@angular/core';
import {ForumThreadViewModel} from "../../models/forumThreadView.model";
import {ForumThread} from "../../models/forumThread/forumThread.model";

@Component({
  selector: 'app-forum-thread-list-item',
  templateUrl: './forum-thread-list-item.component.html',
  styleUrls: ['./forum-thread-list-item.component.css']
})

export class ForumThreadListItemComponent {
  @Input() viewModel: ForumThreadViewModel;
  @Input() currentThread: ForumThread;
}
