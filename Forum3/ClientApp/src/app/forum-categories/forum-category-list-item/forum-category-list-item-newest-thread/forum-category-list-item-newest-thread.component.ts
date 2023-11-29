import { Component, Input } from '@angular/core';
import { LookupThread } from "../../../models/lookup/lookup-thread.model";

@Component({
  selector: 'app-forum-category-list-item-newest-thread',
  templateUrl: './forum-category-list-item-newest-thread.component.html'
})
export class ForumCategoryListItemNewestThreadComponent {
  @Input() latestThread: LookupThread;
}
